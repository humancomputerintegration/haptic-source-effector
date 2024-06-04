using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AmmoType {
    Projectile,
    Laser
}
public class AmmoBox : StompDetector
{
    [Header("Ammo Box Properties")]
    [SerializeField, Tooltip("The type of ammo this box restores")]
    private AmmoType ammoType;
    private protected override void OnStomp() {
        switch (ammoType) {
            case AmmoType.Projectile:
                GameManager.Instance.ReloadProjectile();
                break;
            case AmmoType.Laser:
                GameManager.Instance.ReloadLaser();
                break;
        }
        GameManager.Instance.StompSound.Play();
        Destroy(gameObject);
    }
}
