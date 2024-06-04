using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyShooter : HitScanShooter
{
    

    private protected override void OnStartCharge() {
        Debug.Log("Charge started!");
    }

    private protected override void OnCancelCharge() {
        Debug.Log("Charging canceled!");
    }

    private protected override void OnShoot() {
        Debug.Log("Laser Shot!");
    }
}
