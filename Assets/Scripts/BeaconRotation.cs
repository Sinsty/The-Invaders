using UnityEngine;

public class BeaconRotation : MonoBehaviour
{
    public float RotationSpeed = 0.1f;

    [SerializeField] private Transform _rotateAroundObject;

    void Update()
    {
        Rotator();
    }

    void Rotator()
    {
        gameObject.transform.RotateAround(_rotateAroundObject.position, new Vector3(0, 1, 0), RotationSpeed);
    }
}
