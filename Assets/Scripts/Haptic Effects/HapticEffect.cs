using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HapticEffect : MonoBehaviour
{
    [Header("TMS Settings")]
    [SerializeField, Tooltip("The type of haptic effect to render")]
    private protected HapticEffectType effectType;

    [Header("Haptics Activation Properties")]
    [SerializeField, Tooltip("When enabled, this effect will activate after it has been queued")]
    private protected bool autoActivate = true;
    private protected float curCooldown = 0;
    private float _queueTime;
    private protected float StartUp {
        get {
            return Mathf.Max(_queueTime + curCooldown - Time.time, 0);
        }
        set {
            _queueTime = Time.time;
        }
    }
    private protected HapticEffectsManager manager;

    /* Sets references */
    private protected virtual void Start() {
        manager = HapticEffectsManager.Instance;
    }

    /* 
     * Checks whether you can queue this effect
     *
     * Returns:
     * - True if you can queue the effect
     * - False otherwise
    */
    public bool CanQueue() {
        if (!manager.CanQueueEffect()) {
            return false;
        }
        curCooldown = GetCooldown();
        return ValidateCooldown();
    }

    /* 
     * Returns whether this effect is currently queued
    */
    public bool IsQueued() {
        return manager.IsActiveEffect(this);
    }
    
    /* 
     * Determines the current cooldown 
     */
    private protected virtual float GetCooldown() {
        return 0;
    }

    /* 
     * Determines whether the current cooldown is valid
    */
    private protected virtual bool ValidateCooldown() {
        return true;
    }

    
    /*
     * Queues this effect if possible
     *
     * Returns:
     * - True if the effect was successfully queued
     * - False if the effect was not queued
    */
    public bool Queue() {
        if(!CanQueue()) {
            return false;
        }
        OnQueue();
        manager.QueueEffect(effectType, this);
        StartUp = Time.time;
        if(autoActivate) {
            Invoke("Activate", curCooldown + 3f / 60f);
        }
        
        return true;
    }

    /*
     * A callback function that is called whenever an effect is queued
    */
    private protected virtual void OnQueue() {

    } 

    /*
     * Dequeues this effect if it is active
     *
     * Returns:
     * - True if this effect was successfully dequeued
     * - False if this effect was not active and could not be dequeued
    */
    public bool Dequeue() {
        if(!IsQueued()) {
            return false;
        }
        manager.DequeueEffect();
        OnDequeue();
        return true;
    }

    /*
     * A callback function that is called whenever this effect is dequeued
    */
    private protected virtual void OnDequeue() {

    }

    /*
     * Determines whether this effect can be activated
     *
     * Returns:
     * - True if the effect can be activated
     * - False if the effect cannot be activated
    */
    public bool CanActivate() {
        return manager.IsActiveEffect(this) && StartUp == 0 && manager.ActivationCooldown == 0 && manager.QueueCooldown == 0;
    }
    
    /*
     * Activates this effect if possible
     *
     * Returns:
     * - True if the effect was successfully activated
     * - False if it was not possible to activate the effect
    */
    public bool Activate() {
        if(!CanActivate()) {
            return false;
        }
        manager.ActivateHaptics();
        OnActivate();
        return true;
    }

    /*
     * A callback function that is called whenever this effect is activated
    */
    private protected virtual void OnActivate() {
        
    }
}
