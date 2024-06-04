using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(StompDetector))]
public class MoveStompToTarget : MonoBehaviour
{
    
    [SerializeField, Tooltip("The acceleration, in m/s^2, of the stomp detector")]
    private float acceleration;
    [SerializeField, Tooltip("The maximum speed, in m/s, that the stomp detector can move")]
    private float terminalSpeed;
    [SerializeField, Tooltip("The Transform the stomp detector needs to move to")]
    private Transform target;
    private float curSpeed = 0;
    private bool queued = false;
    private bool arrived = false;

    /* Sets the reference to the target Transform of the stomp detector */
    public void Setup(Transform target) {
        this.target = target;
        // Set the startup time to the amount of time it takes to fall
        // d = 0.5 a t^2 => t = sqrt(2d / a)
        GetComponent<StompDetector>().ChangeStartup(Mathf.Sqrt(2 * (target.position - transform.position).magnitude / acceleration));


    }

    /* Move the stomp detector to the target and then queue the detection script */
    void Update()
    {
        if (arrived) {
            return;
        }
        if (!queued) {
            queued = GetComponent<StompDetector>().CanQueue();
            if(queued) {
                GetComponent<StompDetector>().StartStompDetection();
            }
            return;
        }
        if (transform.position == target.position) {
            arrived = true;
        }
        else {
            curSpeed = Mathf.MoveTowards(curSpeed, terminalSpeed, acceleration * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, target.position, curSpeed * Time.deltaTime);
        }
    }
}
