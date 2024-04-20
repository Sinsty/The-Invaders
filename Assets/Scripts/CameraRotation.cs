using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _xSens = 2;
    [SerializeField] private float _ySens = 2;
    [SerializeField] private Transform _spine;

    private float _xRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        Rotating();
    }

    private void Rotating()
    {
        float yRotationInput = Input.GetAxis("Mouse X") * _ySens;

        transform.Rotate(Vector3.up * yRotationInput, Space.World);

        float xRotationInput = -Input.GetAxis("Mouse Y") * _xSens;
        _xRotation -= Input.GetAxis("Mouse Y") * _xSens;
        _xRotation = Mathf.Clamp(_xRotation, -50, 50);

        float xRotation = _xRotation - xRotationInput;
        xRotation = Mathf.Clamp(xRotation, -50, 50);

        _spine.transform.Rotate(transform.right * xRotation + Vector3.up * yRotationInput, Space.World);

    }
}
