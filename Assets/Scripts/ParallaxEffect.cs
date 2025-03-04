using UnityEngine;
using Cinemachine;

public class ParallaxEffect : MonoBehaviour
{
    public float parallaxFactor; // Determines how fast this layer moves relative to the camera
    public CinemachineVirtualCamera virtualCamera; // Reference to the Cinemachine Virtual Camera
    private Transform cameraTransform;
    private Vector3 lastCameraPosition;

    void Start()
    {
        if (virtualCamera != null)
        {
            cameraTransform = virtualCamera.VirtualCameraGameObject.transform; // Get the virtual camera's transform
        }
        else
        {
            cameraTransform = Camera.main.transform; // Fallback to the main camera
        }

        // Initialize the last camera position
        lastCameraPosition = cameraTransform.position;
    }

    void Update()
    {
        // Calculate the camera movement
        Vector3 cameraDelta = cameraTransform.position - lastCameraPosition;

        // Apply parallax effect by moving the background in proportion to the camera's movement
        transform.position += new Vector3(cameraDelta.x * parallaxFactor, cameraDelta.y * parallaxFactor, 0);

        // Update the last camera position
        lastCameraPosition = cameraTransform.position;
    }
}
