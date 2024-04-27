using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Aiming : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Animator _animator;

    private void Update()
    {
        Fire();
        Aim();
    }

    private void Fire()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.Mouse1))
        {
            _animator.SetBool("IsFiring", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            _animator.SetBool("IsFiring", false);
        }
    }

    private void Aim()
    {
        Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        Physics.Raycast(ray, out hit);
        Vector3 rayPoint = ray.GetPoint(15);
        _aimTarget.transform.position = new Vector3(rayPoint.x, _camera.transform.position.y, rayPoint.z);
    }
}