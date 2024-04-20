using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    enum StartWeapon
    {
        rifle,
        pistol
    }

    [SerializeField] private StartWeapon _startWeapon;
    [SerializeField] private GameObject _rifle;
    [SerializeField] private GameObject _pistol;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        switch (_startWeapon) 
        {
            case StartWeapon.rifle:
                TakeRifle();
                break;

            case StartWeapon.pistol:
                TakePistol();
                break;
        }
    }

    private void Update()
    {
        Switch();
    }

    private void Switch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            TakeRifle();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            TakePistol();
        }
    }

    private void TakeRifle()
    {
        _rifle.SetActive(true);
        _pistol.SetActive(false);
        _animator.SetTrigger("TakeRifle");
    }

    private void TakePistol()
    {
        _rifle.SetActive(false);
        _pistol.SetActive(true);
        _animator.SetTrigger("TakePistol");
    }
}