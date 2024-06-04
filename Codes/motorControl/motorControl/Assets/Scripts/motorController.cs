using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class motorController : MonoBehaviour
{
    private string serialportServo;
    SerialPort streamServo;

    private string serialportLinear;
    SerialPort streamLinear;

    [SerializeField, Range(900, 2100)] int servoValue;
    [SerializeField, Range(0, 1023)] int yLinearValue;
    [SerializeField, Range(0, 1023)] int angleLinearValue;

    [SerializeField] Vector3Int origin;
    [SerializeField] Vector3Int leftFoot;
    [SerializeField] Vector3Int rightFoot;
    [SerializeField] Vector3Int leftHand;
    [SerializeField] Vector3Int rightHand;
    [SerializeField] Vector3Int jaw;

    public OSC osc;

    public enum calibStates
    {
        Initial,
        Origin,
        LeftFoot,
        RightFoot,
        LeftHand,
        RightHand,
        Jaw
    }
    public calibStates calibState;

    public enum States
    {
        Origin,
        LeftFoot,
        RightFoot,
        LeftHand,
        RightHand,
        Jaw
    }
    public States State;

    [SerializeField] bool calibration = true;

    int prevServoValue;
    int prevYLinearValue;
    int prevAngleLinearValue;
    string currentState;
    string prevState;
    string currentCalibState;
    string prevCalibState;

    // Start is called before the first frame update
    void Start()
    {
        serialportServo ="/dev/cu.usbmodem14201"; // "\\\\.\\COM" + port;
        try
        {
            streamServo = new SerialPort(serialportServo, 115200);
            streamServo.ReadTimeout = 50;

            streamServo.Open();
            print("servo port opened");
        }
        catch (Exception e)
        {

        }

        serialportLinear ="/dev/cu.usbmodem14301"; // "\\\\.\\COM" + port;
        try
        {
            streamLinear = new SerialPort(serialportLinear, 115200);
            streamLinear.ReadTimeout = 50;

            streamLinear.Open();
            print("linear port opened");
        }
        catch (Exception e)
        {

        }

        servoValue = 1500;
        yLinearValue = 500;
        angleLinearValue = 500;

        osc.SetAddressHandler("/origin", moveToO);
        osc.SetAddressHandler("/leftFoot", moveToLF);
        osc.SetAddressHandler("/rightFoot", moveToRF);
        osc.SetAddressHandler("/leftHand", moveToLH);
        osc.SetAddressHandler("/rightHand", moveToRH);
        osc.SetAddressHandler("/jaw", moveToJ);
    }

    // Update is called once per frame
    void Update()
    {
        if (calibration){
            if (servoValue != prevServoValue) WriteToSerialServo(servoValue.ToString());
            if (yLinearValue != prevYLinearValue || angleLinearValue != prevAngleLinearValue) WriteToSerialLinear(yLinearValue.ToString()+","+angleLinearValue.ToString());

            if (getCalibState(calibState) != "") currentCalibState = getCalibState(calibState);
            if (currentCalibState != prevCalibState) {
                WriteToSerialServo(currentCalibState);
                WriteToSerialLinear(currentCalibState+","+currentCalibState);
                if (currentCalibState == "co") {origin.x = servoValue; origin.y = yLinearValue; origin.z = angleLinearValue;}
                if (currentCalibState == "clf") {leftFoot.x = servoValue; leftFoot.y = yLinearValue; leftFoot.z = angleLinearValue;}
                if (currentCalibState == "crf") {rightFoot.x = servoValue; rightFoot.y = yLinearValue; rightFoot.z = angleLinearValue;}
                if (currentCalibState == "clh") {leftHand.x = servoValue; leftHand.y = yLinearValue; leftHand.z = angleLinearValue;}
                if (currentCalibState == "crh") {rightHand.x = servoValue; rightHand.y = yLinearValue; rightHand.z = angleLinearValue;}
                if (currentCalibState == "cj") {jaw.x = servoValue; jaw.y = yLinearValue; jaw.z = angleLinearValue;}
            }
            prevServoValue = servoValue;
            prevYLinearValue = yLinearValue;
            prevAngleLinearValue = angleLinearValue;
            prevCalibState = currentCalibState;
        } else {
            if (getState(State) != "") currentState = getState(State);
            if (currentState != prevState) {
                WriteToSerialServo(currentState);
                WriteToSerialLinear(currentState+","+currentState);
            }
            prevState = currentState;
        }
    }

    public void moveToO (OscMessage message) {
        String trigger = message.ToString();
        print("received!!");
        WriteToSerialServo("o");
        WriteToSerialLinear("o,o");
    }

    public void moveToLF (OscMessage message) {
        String trigger = message.ToString();
        print("received!!");
        WriteToSerialServo("lf");
        WriteToSerialLinear("lf,lf");
    }

    public void moveToRF (OscMessage message) {
        String trigger = message.ToString();
        print("received!!");
        WriteToSerialServo("rf");
        WriteToSerialLinear("rf,rf");
    }

    public void moveToLH (OscMessage message) {
        String trigger = message.ToString();
        print("received!!");
        WriteToSerialServo("lh");
        WriteToSerialLinear("lh,lh");
    }

    public void moveToRH (OscMessage message) {
        String trigger = message.ToString();
        print("received!!");
        WriteToSerialServo("rh");
        WriteToSerialLinear("rh,rh");
    }

    public void moveToJ (OscMessage message) {
        String trigger = message.ToString();
        print("received!!");
        WriteToSerialServo("j");
        WriteToSerialLinear("j,j");
    }

    private string getState(States state)
    {
        switch(state)
        {
            case States.Origin: return "o";
            case States.LeftFoot: return "lf";
            case States.RightFoot: return "rf";
            case States.LeftHand: return "lh";
            case States.RightHand: return "rh";
            case States.Jaw: return "j";
            default: return "";
        }
    }

    private string getCalibState(calibStates calibState)
    {
        switch(calibState)
        {
            case calibStates.Origin: return "co";
            case calibStates.LeftFoot: return "clf";
            case calibStates.RightFoot: return "crf";
            case calibStates.LeftHand: return "clh";
            case calibStates.RightHand: return "crh";
            case calibStates.Jaw: return "cj";
            default: return "";
        }
    }

    void OnApplicationQuit()
    {
        CloseSerialPortServo();
        CloseSerialPortLinear();
    }

    void OnDisable()
    {
        CloseSerialPortServo();
        CloseSerialPortLinear();
    }

    public void WriteToSerialServo(string message)
    {
        if (!streamServo.IsOpen) return;
        streamServo.WriteLine(message);
        streamServo.BaseStream.Flush();
    }

    public void WriteToSerialLinear(string message)
    {
        if (!streamLinear.IsOpen) return;
        streamLinear.WriteLine(message);
        streamLinear.BaseStream.Flush();

    }

    private void CloseSerialPortServo()
    {
        try
        {
            streamServo.Close();
            print("servo port closed");
        }
        catch (System.Exception e)
        {
            print("servo close error");
        }
    }

    private void CloseSerialPortLinear()
    {
        try
        {
            streamLinear.Close();
            print("linear port closed");
        }
        catch (System.Exception e)
        {
            print("linear close error");
        }
    }
}
