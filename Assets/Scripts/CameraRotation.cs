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
    private float _yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void LateUpdate()
    {
        Rotating(Inputs());
        Stabilize();
    }

    private Vector2 Inputs()
    {
        if (PauseGame.isPaused)
            return Vector2.zero;

        return new Vector2(-Input.GetAxis("Mouse Y") * _xSens, Input.GetAxis("Mouse X") * _ySens);
    }

    private void Rotating(Vector2 inputs)
    {
        _yRotation += inputs.y;

        transform.Rotate(Vector3.up * inputs.y, Space.World);

        _neededXRotationInput += inputs.x;
        _neededXRotationInput = Mathf.Clamp(_neededXRotationInput, -75, 75);

        _xRotation = _neededXRotationInput - inputs.x;
        _xRotation = Mathf.Clamp(_xRotation, -50, 50);

        _spine.transform.Rotate(transform.right * _xRotation + Vector3.up * inputs.y, Space.World);

    }

    private void Stabilize()
    {

        float StabilizedXRotation = Mathf.LerpAngle(_cameraHolder.transform.eulerAngles.x, _xRotation, _stabilizationForce);
        float StabilizedYRotation = Mathf.LerpAngle(_cameraHolder.transform.eulerAngles.y, _yRotation, _stabilizationForce);
        float StabilizedZRotation = Mathf.LerpAngle(_cameraHolder.transform.eulerAngles.z, 0, _stabilizationForce);
        _cameraHolder.transform.eulerAngles = new Vector3(StabilizedXRotation, StabilizedYRotation, StabilizedZRotation);
    }
}
