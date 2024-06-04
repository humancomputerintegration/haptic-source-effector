using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DynamicHapticEffect : HapticEffect
{
    [SerializeField, Tooltip("The minimum time difference, in seconds, between queueing and activating haptics.\n\nNote that this value will be overrided by the amount of time it takes to queue haptics if it is below that value.")]
    private protected float minStartup;
    [SerializeField, Tooltip("The maximum time difference, in seconds, between queueing and activating haptics")]
    private protected float maxStartup;

    private protected override float GetCooldown()
    {
        return Mathf.Max(manager.ActivationCooldown, manager.QueueCooldown, minStartup);
    }

    private protected override bool ValidateCooldown()
    {
        return curCooldown <= maxStartup;
    }
}
