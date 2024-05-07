using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/* Static Haptic Effects are Dynamic Haptic Effects with constant startup */
public abstract class StaticHapticEffect : HapticEffect
{
    [SerializeField, Tooltip("The startup time, in seconds, of the haptics")]
    private protected float startupTime;
    private protected override float GetCooldown()
    {
        return startupTime;
    }
    private protected override bool ValidateCooldown() {
        return curCooldown >= Mathf.Max(manager.ActivationCooldown, manager.QueueCooldown);
    }
    public void ChangeStartup(float newStartup) {
        startupTime = newStartup;
    }
}
