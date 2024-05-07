using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField, Tooltip("The maximum amount of time, in seconds, that the projectile can be in the game")]
    private float despawnTime;
    private protected new Rigidbody rigidbody;
    private protected new Collider collider;
    private void Start() {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        if(!collider.isTrigger) {
            Debug.Log("Error: Projectile collider is not a trigger! Fixing automatically but please adjust in the inspector for the future.");
            collider.isTrigger = true;
        }
        Invoke("OnCollision", despawnTime);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.TryGetComponent<IHittable>(out IHittable hittable)) {
            hittable.OnHit(transform.position);
            CancelInvoke();
            OnCollision();
        }
    }

    private protected virtual void OnCollision() {
        Destroy(gameObject);
    }
}
