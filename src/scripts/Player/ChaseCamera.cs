using UnityEngine;

public class ChaseCamera : MonoBehaviour
{
public float sensX;
public float sensY;

public Transform orientation;
public Transform playerBody;
public Transform cameraTransform;

float xRotation;
float yRotation;

private void Start()
{
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;
}

private void Update()
{
    //get mouse input
    float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
    float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

    yRotation += mouseX;

    xRotation -= mouseY;
    xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    //apply rotations
    orientation.eulerAngles = new Vector3(xRotation, yRotation);
    cameraTransform.eulerAngles = new Vector3(xRotation, yRotation + 90);
}
}