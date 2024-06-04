using UnityEngine;

public class PositionDistance : MonoBehaviour
{
    private Vector3 initialPosition;

    void Update()
    {
        // Store the initial position
        if (Input.GetKeyDown(KeyCode.A))
        {
            initialPosition = transform.position;
            Debug.Log("Initial position registered: " + initialPosition);
        }

        // Store the final position and calculate the distance
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector3 finalPosition = transform.position;
            Debug.Log("Final position registered: " + finalPosition);
            
            float positionDistance = Vector3.Distance(initialPosition, finalPosition);
            Debug.Log("Position distance: " + positionDistance);
        }
    }
}