using UnityEngine;

public class BeaconRotation : MonoBehaviour
{
    public float RotationSpeed = 0.1f;

    [SerializeField] private Transform _rotateAroundObject;

    void Update()
    {
        if (PauseGame.isPaused)
            return;

        Rotator();
    }

    void Rotator()
    {
        gameObject.transform.RotateAround(_rotateAroundObject.position, new Vector3(0, 1, 0), RotationSpeed);
    }
}
