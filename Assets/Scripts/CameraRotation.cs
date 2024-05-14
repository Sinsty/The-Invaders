using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] private float _xSens = 2;
    [SerializeField] private float _ySens = 2;
    [SerializeField] private Transform _spine;
    [SerializeField, Range(0f, 1f)] private float _stabilizationForce;
    [SerializeField] private Transform _cameraHolder;

    private float _neededXRotationInput;
    private float _xRotation;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        Rotating();
        Stabilize();
    }

    private void Rotating()
    {
        float yRotationInput = Input.GetAxis("Mouse X") * _ySens;

        transform.Rotate(Vector3.up * yRotationInput, Space.World);

        float xRotationInput = -Input.GetAxis("Mouse Y") * _xSens;
        _neededXRotationInput -= Input.GetAxis("Mouse Y") * _xSens;
        _neededXRotationInput = Mathf.Clamp(_neededXRotationInput, -50, 50);

        _xRotation = _neededXRotationInput - xRotationInput;
        _xRotation = Mathf.Clamp(_xRotation, -50, 50);

        _spine.transform.Rotate(transform.right * _xRotation + Vector3.up * yRotationInput, Space.World);

    }

    private void Stabilize()
    {

        float StabilizedXRotation = Mathf.LerpAngle(_cameraHolder.transform.eulerAngles.x, _xRotation, _stabilizationForce);
        float StabilizedZRotation = Mathf.LerpAngle(_cameraHolder.transform.eulerAngles.z, 0, _stabilizationForce);
        _cameraHolder.transform.eulerAngles = new Vector3(StabilizedXRotation, _cameraHolder.transform.eulerAngles.y, StabilizedZRotation);
    }
}
