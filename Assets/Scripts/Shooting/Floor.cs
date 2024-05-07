using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour, IHittable
{
    public void OnHit(Vector3 position) {
        Debug.Log("Floor Hit!");
        return;
    }

    public Vector3 GetPosition() {
        return transform.position;
    }
}
