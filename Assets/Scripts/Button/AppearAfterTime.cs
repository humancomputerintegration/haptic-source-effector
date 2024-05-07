using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearAfterTime : MonoBehaviour
{
    [SerializeField, Tooltip("The time, in seconds, it takes before all child objects are enabled")]
    private float appearTime;
    // Start is called before the first frame update
    private void Awake() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        Invoke("EnableAll", appearTime);
    }

    private void EnableAll() {
        foreach(Transform child in transform) {
            child.gameObject.SetActive(true);
        }
    }
}
