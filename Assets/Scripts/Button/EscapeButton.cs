using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeButton : Button
{
    [Header("Escape button parameters")]
    [SerializeField, Tooltip("The distance to move the button vertically when pressed")]
    private float pressDistance;
    private protected override void OnPress() {
        transform.position -= pressDistance * Vector3.up;
        Debug.Log("Pressed!");
    }
}
