using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Aiming : MonoBehaviour
{
    [SerializeField] private Transform _spine;
    [SerializeField] private Transform _aimTarget;
    [SerializeField] private Animator _animator;

    private void LateUpdate()
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
        else if (Input.GetKey(KeyCode.Mouse0) == false && Input.GetKey(KeyCode.Mouse1) == false)
        {
            _animator.SetBool("IsFiring", false);
        }
    }

    private void Aim()
    {
        //Ray ray = _camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        Ray ray = new Ray(_spine.transform.position, _spine.transform.forward);

        Physics.Raycast(ray, out hit);
        Vector3 rayPoint = ray.GetPoint(15);
        _aimTarget.transform.position = new Vector3(rayPoint.x, _spine.transform.position.y, rayPoint.z);
    }
}