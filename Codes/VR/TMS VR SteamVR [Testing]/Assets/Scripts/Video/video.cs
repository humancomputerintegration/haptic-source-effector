using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class video : MonoBehaviour
{
    [SerializeField] TMSInterface tmsInterface;
    [SerializeField] OSC osc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("a")) StartCoroutine(videoPlot());
    }

    IEnumerator videoPlot(){
        yield return new WaitForSeconds(5f);
        SendString("/leftHand");
        yield return new WaitForSeconds(2f);
        tmsInterface.StimQueued = true;
        //tmsInterface.stimulate = false;
        yield return new WaitForSeconds(4f);

        SendString("/rightFoot");
        yield return new WaitForSeconds(2f);
        //tmsInterface.stimulate = true;
        tmsInterface.StimQueued = true;
        //tmsInterface.stimulate = false;
        yield return new WaitForSeconds(5f);

        SendString("/rightHand");
        yield return new WaitForSeconds(2f);
        //tmsInterface.stimulate = true;
        tmsInterface.StimQueued = true;
        //tmsInterface.stimulate = false;
        yield return new WaitForSeconds(5f);

        SendString("/leftFoot");
        yield return new WaitForSeconds(2f);
        //tmsInterface.stimulate = true;
        tmsInterface.StimQueued = true;
        //tmsInterface.stimulate = false;
        yield return new WaitForSeconds(4f);

        SendString("/rightHand");
        yield return new WaitForSeconds(2f);
        //tmsInterface.stimulate = true;
        tmsInterface.StimQueued = true;
        //tmsInterface.stimulate = false;
        yield return new WaitForSeconds(5);
        SendString("/origin");
    }

    public void SendString(string msg)
    {
        OscMessage message = new OscMessage();
        message.address = msg;
        message.values.Add(msg);
        osc.Send(message);
        Debug.Log("OSC Send: " + message.ToString());
    }
}
