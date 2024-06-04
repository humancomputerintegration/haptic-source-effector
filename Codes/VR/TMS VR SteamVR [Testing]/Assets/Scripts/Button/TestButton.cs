using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : Button
{
    private protected override void OnPress() {
        gameObject.SetActive(false);
        Debug.Log("Pressed!");
    }
}
