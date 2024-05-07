using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using System;

public class MotorPosition : MonoBehaviour
{
    /* Make this a Singleton class */
    public static MotorPosition Instance { get; private set; }
    private void Awake() 
    {   
        if (Instance != null && Instance != this) 
        { 
            Destroy(this); 
        } 
        else 
        { 
            Instance = this; 
        } 
    }
    public enum States {
        Origin,
        LeftHand,
        RightHand,
        LeftFoot,
        RightFoot,
        Jaw
    }
    
    [Header("Internal States")]
    [SerializeField, ReadOnly, Tooltip("The current active state")]
    public States _state = States.Origin;
    [Header("Debugging Parameters")]
    [SerializeField, Tooltip("When enabled, the State will always synchronize with the debug state")]
    private bool debug;
    [SerializeField, Tooltip("The current debugging state")]
    private States debugState;
    private string path;
    public States State {
        get {
            return _state;
        }
        set {
            if(_state == value) {
                return;
            }
            _state = value;
            path = "";
            switch(value) {
                case States.Origin:
                    path = "/origin";
                    break;
                case States.LeftFoot:
                    path = "/leftFoot";
                    break;
                case States.RightFoot:
                    path = "/rightFoot";
                    break;
                case States.LeftHand:
                    path = "/leftHand";
                    break;
                case States.RightHand:
                    path = "/rightHand";
                    break;
                case States.Jaw:
                    path = "/jaw";
                    break;
            }
            SendString("Queue the haptics!");
        }
    }

    [SerializeField, Tooltip("When enabled, will print debug logs whenever a message is sent")]
    private bool debugLogs = true;
    private OSC osc;
    
    private void Start()
    {
        osc = GetComponent<OSC>();
    }

    public void SendString(string msg)
    {
        OscMessage message = new OscMessage();
        message.address = path;
        message.values.Add(msg);
        osc.Send(message);
        if (debugLogs)
        {
            Debug.Log("OSC Send: " + message.ToString());
        }
    }
    private void Update() {
        if (debug && State != debugState) {
            State = debugState;
        }
    }
}