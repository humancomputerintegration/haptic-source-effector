using UnityEngine;

public class RotationDifference : MonoBehaviour
{
    private Quaternion initialRotation;
    private Quaternion finalRotation;

    void Update()
    {
        // Store the initial rotation
        if (Input.GetKeyDown(KeyCode.A))
        {
            initialRotation = transform.rotation;
            Debug.Log("Initial rotation registered: " + initialRotation.eulerAngles);
        }

        // Store the final rotation and calculate the difference
        if (Input.GetKeyDown(KeyCode.D))
        {
            finalRotation = transform.rotation;
            Debug.Log("Final rotation registered: " + finalRotation.eulerAngles);
            
            float angleDifference = Quaternion.Angle(initialRotation, finalRotation);
            Debug.Log("Angle difference: " + angleDifference + " degrees");
        }
    }
}