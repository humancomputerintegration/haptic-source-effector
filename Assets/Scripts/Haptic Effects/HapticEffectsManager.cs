using System.Collections;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using UnityEngine;

public enum HapticEffectType {
    LeftHandWeak,
    LeftHandStrong,
    RightHandWeak,
    RightHandStrong,
    LeftFootWeak,
    LeftFootStrong,
    RightFootWeak,
    RightFootStrong,
    Jaw
}
[System.Serializable]
public struct HapticEffectInfo {
    [SerializeField, ReadOnly, Tooltip("The type of haptic effect that this controls")]
    public HapticEffectType effectType;
    [SerializeField, Tooltip("The intensity of this haptic effect")]
    public int intensity; 
    public HapticEffectInfo(HapticEffectType effectType, int intensity) {
        this.effectType = effectType;
        this.intensity = intensity;
    }
}
[System.Serializable]
public class HapticEffectsInfo {
    [SerializeField, ReadOnly, Tooltip("All of the haptic effects")]
    public List<HapticEffectInfo> effects;
    public HapticEffectsInfo() {
        effects = new List<HapticEffectInfo>();
        foreach (HapticEffectType type in System.Enum.GetValues(typeof(HapticEffectType)).Cast<HapticEffectType>()) {
            effects.Add(new HapticEffectInfo(type, 0));
        }
    }
    
}
public class HapticEffectsManager : MonoBehaviour
{
    /* Make this a Singleton class */
    public static HapticEffectsManager Instance { get; private set; }
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
    [Header("Global Settings")]
    [SerializeField, Tooltip("The amount of time after an activation before it is safe to activate again")]
    private float activationCooldown;
    public float MaxActivationCooldown {
        get {
            return activationCooldown;
        }
    }
    [SerializeField, Tooltip("The amound of time after a queue before an activation can occur")]
    private float queueCooldown;
    public float MaxQueueCooldown {
        get {
            return queueCooldown;
        }
    }

    /* Tracks the amount of time remaining before you can activate haptics after a previous activation*/
    private float _activationTime = 0;
    public float ActivationCooldown {
        get { 
            return Mathf.Max(activationCooldown + _activationTime - Time.time, 0); 
        }
        set {
            _activationTime = Time.time;
        }
    }

    /* Tracks the amount of time remaining before you can activate haptics after a queue*/
    private float _queueTime = 0;
    public float QueueCooldown {
        get { 
            return  Mathf.Max(queueCooldown + _queueTime - Time.time, 0); 
        }
        set {
            _queueTime = Time.time;
        }
    }
    
    [SerializeField, ReadOnly, Tooltip("The current haptic effect that is queued")]
    private HapticEffect curEffect = null;
    [SerializeField, Tooltip("When clicked, the current haptic effect will be dequeued")]
    private bool dequeue;

    
    
    [Header("Effect Parameters")]
    [SerializeField, Tooltip("The intensities of each haptic effect type")]
    private HapticEffectsInfo effectsInfo;

    public MotorPosition.States GetMotorState(HapticEffectType effectType) {
        switch(effectType) {
            case HapticEffectType.LeftHandWeak:
                return MotorPosition.States.LeftHand;
            case HapticEffectType.LeftHandStrong:
                return MotorPosition.States.LeftHand;
            case HapticEffectType.RightHandWeak:
                return MotorPosition.States.RightHand;
            case HapticEffectType.RightHandStrong:
                return MotorPosition.States.RightHand;
            case HapticEffectType.LeftFootWeak:
                return MotorPosition.States.LeftFoot;
            case HapticEffectType.LeftFootStrong:
                return MotorPosition.States.LeftFoot;
            case HapticEffectType.RightFootWeak:
                return MotorPosition.States.RightFoot;
            case HapticEffectType.RightFootStrong:
                return MotorPosition.States.RightFoot;
            case HapticEffectType.Jaw:
                return MotorPosition.States.Jaw;
            default:
                return MotorPosition.States.Origin; // This is impossible but necessary for compilation
        }
    }

    /*
     * Queues the inputted haptic effect
    */
    public void QueueEffect(HapticEffectType effectType, HapticEffect effect) {
        curEffect = effect;
        QueueCooldown = Time.time;
        TMSInterface.Instance.Intensity = effectsInfo.effects[(int) effectType].intensity;
        MotorPosition.Instance.State = GetMotorState(effectType);
    }

    /* 
     * Dequeues the current haptic effect
    */
    public void DequeueEffect() {
        curEffect = null;
    }

    /*
     * Determines whether or not it is currently possible to queue an effect
     * - It is possible to queue an effect when there are no effects currently queued
     *
     * Returns:
     * - True if it is possible to queue an effect
     * - False if it is not possible to queue an effect
    */

    public bool CanQueueEffect() {
        return curEffect == null;
    }

    /*
     * Determines whether the inputted effect is the current queued effect
     *
     * Returns:
     * - True if the inputted effect is the current effect
     * - False if the inputted effect is not the current effect
    */
    public bool IsActiveEffect(HapticEffect effect) {
        return effect == curEffect;
    }

    /*
     * Activates haptics and resets the queue if there is an effect queued
     *
     * Returns:
     * - True if haptics qere successfully activated
     * - False if the queue was empty
    */
    public bool ActivateHaptics() {
        if (curEffect == null) {
            Debug.Log("Error: Haptics triggered when no effects were queued!");
            return false;
        }
        TMSInterface.Instance.QueueStimulation();
        ActivationCooldown = Time.time;
        DequeueEffect();
        return true;
    }

    /* Set initial activation and queue parameters so you don't have to wait to use haptics on startup */
    private void Start() {
        _activationTime = -activationCooldown;
        _queueTime = -queueCooldown;
    }

    /* Handle the clicking of the dequeue button */
    private void Update() {
        if (dequeue) {
            dequeue = false;
            DequeueEffect();
        }
    }
}
