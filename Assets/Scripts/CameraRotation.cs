using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _xSens;
    [SerializeField] private float _ySens;

    private float _xRotation;
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _xRotation = _camera.transform.localEulerAngles.x;
        _yRotation = transform.localEulerAngles.y;
    }

    private void Update()
    {
        Rotate();
    }

    private void Rotate()
    {
        _xRotation -= Input.GetAxis("Mouse Y") * _xSens;
        _yRotation += Input.GetAxis("Mouse X") * _ySens;

        _xRotation = Mathf.Clamp(_xRotation, -90, 90);

        _camera.transform.localEulerAngles = Vector3.right * _xRotation;
        transform.localEulerAngles = Vector3.up * _yRotation;
    }
}
