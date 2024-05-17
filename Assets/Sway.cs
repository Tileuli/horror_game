using UnityEngine;

public class Sway : MonoBehaviour
{
    public float swayAmount = 0.05f;       // Amount of sway
    public float maxSwayAmount = 0.1f;     // Maximum amount of sway
    public float swaySmoothness = 2f;      // Smoothness of the sway

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Calculate target sway position and rotation based on mouse movement
        Vector3 targetPosition = new Vector3(
            Mathf.Clamp(-mouseX * swayAmount, -maxSwayAmount, maxSwayAmount),
            Mathf.Clamp(-mouseY * swayAmount, -maxSwayAmount, maxSwayAmount),
            0
        );

        Quaternion targetRotation = Quaternion.Euler(
            Mathf.Clamp(-mouseY * swayAmount * 10f, -maxSwayAmount * 10f, maxSwayAmount * 10f),
            Mathf.Clamp(mouseX * swayAmount * 10f, -maxSwayAmount * 10f, maxSwayAmount * 10f),
            0
        );

        // Smoothly interpolate to the target position and rotation
        transform.localPosition = Vector3.Lerp(transform.localPosition, initialPosition + targetPosition, Time.deltaTime * swaySmoothness);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, initialRotation * targetRotation, Time.deltaTime * swaySmoothness);
    }
}
