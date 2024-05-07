using System;
using System.Collections;
using UnityEngine;

public class TMSInterface : MonoBehaviour
{
    /* Make this a Singleton class */
    public static TMSInterface Instance { get; private set; }
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
    public enum StimType {
        SinglePulse,
        Constant,
        MultiStim
    }
    [Header("Stimulation Parameters")]
    [SerializeField, Tooltip("When enabled, stimulation will not occur")]
    private bool disableStimulation;
    [SerializeField, Tooltip("The current type of stimulation")] 
    private StimType type;
    private StimType _type;
    public StimType Type {
        get {
            return _type;
        }
        set {
            if(!stimReady || value == _type) {
                return;
            }
            QueueDisarm();
            if(debug) {
                Debug.Log("Internal stimulation type updated to: " + value);
            }
            _type = value;
            type = value;
        }
    }

    [SerializeField, Range(0, 100), Tooltip("The intensity of the stimulation")] 
    private int intensity = 20;
    private int _intensity;
    public int Intensity {
        get {
            return _intensity;
        }
        set {
            if(!stimReady || value == _intensity || value == 0) {
                return;
            }
            QueueDisarm();
            if(debug) {
                Debug.Log("Internal intensity updated to: " + value);
            }
            _intensity = value;
            intensity = value;
        }
    }

    [SerializeField, Range(0, 20), Tooltip("The frequency, in 1/s, of the stimulation")]
    private int frequency = 20;
    private int _frequency;
    public int Frequency {
        get {
            return _frequency;
        }
        set {
            if(!stimReady || _type != StimType.MultiStim || value == _frequency) {
                return;
            }
            QueueDisarm();
            if(debug) {
                Debug.Log("Internal frequency updated to: " + value);
            }
            _frequency = value;
            frequency = value;
        }
    }
    [SerializeField, Range(1, 10), Tooltip("The number of stimulations to do when in MultiStim mode")] 
    private int numMultiStims = 3;
    private bool stimActive = false;

    [Header("Internal Stimulation Parameters")]
    [SerializeField, ReadOnly, Tooltip("Whether the coil is currently able to stimulate")]
    private bool stimReady = false;
    [SerializeField, ReadOnly, Tooltip("Whether the coil is currently preparing a stimulation")]
    private bool preparingStim = false;
    /* When true, if stimulation is possible, stimulation will activate */
    private bool _stimQueued = false;
    public bool StimQueued {
        get {
            return _stimQueued;
        }
        set {
            if (!stimReady || value == _stimQueued) {
                return;
            }
            _stimQueued = value;
        }
    }
    private bool disarmQueued = false;
    public enum InputSource {
        Keyboard,
        Inspector
    }
    [SerializeField, ReadOnly, Tooltip("The source of input used for constant stimulation")]
    private InputSource inputSource = InputSource.Keyboard;

    [Header("Manual Control")]
    [SerializeField, Tooltip("Press this button to activate the emergency stop")]
    private bool emergencyStop;
    [SerializeField, ReadOnly, Tooltip("Press this button to activate stimulation. Note: This functionality is currently disabled since it will cause the coil to endlessly stimulate in constant stimulation mode!")]
    public bool stimulate;
    [SerializeField, Tooltip("The button to press to activate the emergency stop")]
    private KeyCode emergencyStopBinding = KeyCode.Escape;
    [SerializeField, Tooltip("The button to hit to activate stimulation")]
    private KeyCode stimBinding = KeyCode.Space;

    [Header("Safety Parameters")]
    [SerializeField, Tooltip("The amount of time you need to wait after stimulating to ensure stimulations are safe")]
    private float waitTime = 5;
    public float WaitTime {
        get {
            return waitTime;
        }
    }
    [SerializeField, ReadOnly, Tooltip("The amount of time remaining before it is safe to stimulate")]
    private float curWaitTime = 0;
    [Header("Object References")]
    [SerializeField, Tooltip("The OSC communications script for sending messages to TMS")]
    private OSC osc;
    [Header("Debugging")]
    [SerializeField, Tooltip("When enabled, this script will print debug logs whenever something happens")]
    private bool debug;

    /* Set initial states of internal variables */
    private void Start() {
        _type = type;
        _intensity = intensity;
        _frequency = frequency;
    }

    /* Resets the current wait time */
    private void SetWaitTime() {
        curWaitTime = waitTime;
    }

    /* Updates all of the internal states to match the inspector values */
    private void SynchronizeInspectorValues() {
        Type = type;
        Intensity = intensity;
        Frequency = frequency;
    }

    /* 
     * Triggers an emergency stop if an emergency stop input is pressed
     * 
     * Returns:
     * - True if an emergency stop occured
     * - False if an emergency stop did not occur
    */
    private bool ProcessEmergencyStop() {
        if(emergencyStop || Input.GetKeyDown(emergencyStopBinding)) {
            if(debug) {
                Debug.Log("Emergency Stop Activated!");
            }
            QueueDisarm();
            curWaitTime = waitTime;
            emergencyStop = false;
            return true;
        }
        return false;
    }

    /* Queues a stimulation if the input is pressed */
    private void ProcessStimActivate() {
        if(!stimReady) {
            return;
        }
        if (stimulate || Input.GetKeyDown(stimBinding)) {
            StimQueued = true;
            if(Type != StimType.Constant) {
                stimulate = false;
            }
            if (debug) {
                Debug.Log("Stimulation queued!");
            }
        }
    }

    /* Deactivates a stimulation if the stimulation button is released and the stim type can be deactivated */
    private void ProcessStimDeactivate() {
        if(!stimActive) {
            return;
        }
        switch(Type) {
            case StimType.SinglePulse:
                break;
            case StimType.Constant:
                bool releaseDetected = false;
                /* Check if you released the input that you used to activate this stimulation */
                switch(inputSource) {
                    case InputSource.Keyboard:
                        if(Input.GetKeyUp(stimBinding)) {
                            releaseDetected = true;
                        }
                        stimulate = false;
                        break;
                    case InputSource.Inspector:
                        if(!stimulate) {
                            releaseDetected = true;
                        }
                        break;
                }
                if(releaseDetected) {
                    if(debug) {
                        Debug.Log("Stimulation deactivated!");
                    }
                    QueueDisarm();
                    SetWaitTime();
                    stimActive = false;
                }
                break;
            case StimType.MultiStim:
                break;
        }
    }

    /* Readies the coil */
    private void PrepareStim() {
        if(stimReady || preparingStim || stimActive) {
            return;
        }
        StartCoroutine(Ready());
    }

    /* Queues a disarm to happen later in the frame */
    public void QueueDisarm() {
        disarmQueued = true;
    }

    /* Disarms the coil if a disarm was queued earlier in the frame */
    private void ProcessDisarm() {
        if(!disarmQueued) {
            return;
        }
        Disarm();
        disarmQueued = false;
    }

    /* Tells the coil to do a single pulse */
    private void StartSinglePulseStim() {
        PeriodicStim();
    }

    /* Tells the coil to start pulsing repeatedly */
    private void StartConstantStim() {
        Stim();
        stimActive = true;
        if(Input.GetKeyDown(stimBinding)) {
            inputSource = InputSource.Keyboard;
        }
        else {
            inputSource = InputSource.Inspector;
        }
    }

    /* Tells the coil to start a finite sequence of repeated pulses */
    private void StartMultiStim() {
        Stim();
        StartCoroutine(MultiStim());
    }

    /* Initializes the correct type of stimulation if possible */
    private void StartStim() {
        bool queueState = StimQueued;
        StimQueued = false;

        if(!queueState || curWaitTime > 0 || disableStimulation) {
            return;
        }

        switch(Type) {
            case StimType.SinglePulse:
                if(debug) {
                    Debug.Log("Single-Pulse Stimulation starting!");
                }
                StartSinglePulseStim();
                break;
            case StimType.Constant:
                if(debug) {
                    Debug.Log("Constant Stimulation starting!");
                }
                StartConstantStim();
                break;
            case StimType.MultiStim:
                if(debug) {
                    Debug.Log("Multistim starting!");
                }
                StartMultiStim();
                break;
        }
    }

    /* Reads inputs and controls stimulation */
    private void Update() {
        if(ProcessEmergencyStop()) {
            return;
        }
        SynchronizeInspectorValues();
        ProcessDisarm();
        PrepareStim();
        ProcessStimActivate();
        StartStim();
        ProcessStimDeactivate();

        curWaitTime = Mathf.MoveTowards(curWaitTime, 0, Time.deltaTime);
    }

    /* Use this function to trigger TMS haptics from other functions */
    public void QueueStimulation()
    {
        StimQueued = true;
        if(Type == StimType.Constant) { // Constant stim type does not make sense to automate
            Type = StimType.MultiStim;
        }
    }

    /* Communication functions */
    /* ----------------------- */
    private IEnumerator MultiStim()
    {
        stimActive = true;
        yield return new WaitForSeconds((numMultiStims - 0.5f) / frequency);
        Disarm();
        stimReady = false;
        stimActive = false;
        curWaitTime = waitTime;
        if(debug) {
            Debug.Log("MultiStim ended!");
        }
    }


    public void Set(int num1, int num2, float num3)
    {
        OscMessage message;
        message = new OscMessage
        {
            address = "/set"
        };
        message.values.Add(num1);
        message.values.Add(num2);
        message.values.Add(num3);
        osc.Send(message);
    }

    public void Arm()
    {
        if(debug) {
            Debug.Log("Coil armed!");
        }
        OscMessage message;
        message = new OscMessage
        {
            address = "/arm"
        };
        osc.Send(message);
    }

    public void Disarm()
    {
        if(debug) {
            Debug.Log("Coil disarmed!");
        }
        OscMessage message;
        message = new OscMessage
        {
            address = "/disarm"
        };
        osc.Send(message);
        stimReady = false;
        stimActive = false;
    }

    public IEnumerator Ready()
    {
        preparingStim = true;
        if(debug) {
            Debug.Log("Stimulation preparing...");
        }

        Arm();

        // Stop the coroutine until the waitTime is sufficiently low
        while (curWaitTime > 0)
        {
            yield return null;
        }

        switch(Type) {
            case StimType.SinglePulse:
                Set(Intensity, 0, 0.5f);
                if(debug) {
                    Debug.Log("Intensity set to: " + Intensity);
                    Debug.Log("Frequency set to: 0 (this is a single pulse)");
                }
                break;
            case StimType.Constant:
                Set(Intensity, Frequency, 5f);
                if(debug) {
                    Debug.Log("Intensity set to: " + Intensity);
                    Debug.Log("Frequency set to:)" + Frequency);
                }
                break;
            case StimType.MultiStim:
                Set(Intensity, Frequency, 5f);
                if(debug) {
                    Debug.Log("Intensity set to: " + Intensity);
                    Debug.Log("Frequency set to: " + Frequency);
                }
                break;
        }
        yield return new WaitForSeconds(0.1f);
        
        stimReady = true;
        preparingStim = false;
        Debug.Log("Ready!");
    }

    private void Stim()
    {
        if(debug) {
            Debug.Log("Starting stimulation...");
        }
        SendStim(1);
    }

    private void SendStim(int num) {
        OscMessage message;
        message = new OscMessage
        {
            address = "/stim",
            values = {num}
        };
        osc.Send(message);
    }

    private void PeriodicStim()
    {
        OscMessage message;
        message = new OscMessage
        {
            address = "/periodicStim",
            values = {0}
        };
        osc.Send(message);
        SetWaitTime();
    }

    private void OnApplicationQuit()
    {
        OscMessage message;
        message = new OscMessage
        {
            address = "/disconnect"
        };
        osc.Send(message);
    }
}
