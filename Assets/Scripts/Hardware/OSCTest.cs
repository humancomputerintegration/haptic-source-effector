using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OSCTest : MonoBehaviour
{
    [SerializeField, Tooltip("The message to send")]
    private string message;
    [SerializeField, Tooltip("The amount of time, in seconds, to wait before sending a message")]
    private float sendTime;
    private float time = 0;
    private bool sending = false;

    [SerializeField, Tooltip("The OSC path used for communication")]
    private string path = "/anchor";
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

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (time >= sendTime)
        {
            time = 0;
            SendString(message);
        }
    }
}