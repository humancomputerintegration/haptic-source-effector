using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittableTest : MonoBehaviour, IHittable
{
    public void OnHit(Vector3 position) {
        gameObject.SetActive(false);
    }

    public Vector3 GetPosition() {
        return transform.position;
    }

}
