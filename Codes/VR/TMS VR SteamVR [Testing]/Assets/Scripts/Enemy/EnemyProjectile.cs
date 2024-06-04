using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    [SerializeField, ReadOnly, Tooltip("The amount of time before the projectile hits its target")]
    private float timeRemaining = 0;
    [SerializeField, ReadOnly, Tooltip("The Transform the projectile is trying to hit")]
    private Transform target;
    public void Setup(float time, Transform target) {
        timeRemaining = time;
        this.target = target;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Update() {
        transform.position = Vector3.MoveTowards(transform.position, target.position, Vector3.Distance(transform.position, target.position) / timeRemaining * Time.deltaTime);
        if (transform.position == target.position) {
            Destroy(gameObject);
        }
        timeRemaining = Mathf.MoveTowards(timeRemaining, 0, Time.deltaTime);
    }

    private protected override void OnCollision() {
        return;
    }
}
