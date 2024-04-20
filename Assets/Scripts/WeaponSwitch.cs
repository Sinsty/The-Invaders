using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    enum StartWeapon
    {
        rifle,
        pistol,
        map
    }

    [SerializeField] private StartWeapon _startWeapon;
    [SerializeField] private GameObject _rifle;
    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _map;
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
            case StartWeapon.map:
                TakeMap();
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
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            TakeMap();
        }
    }

    private void TakeRifle()
    {
        _rifle.SetActive(true);
        _pistol.SetActive(false);
        _map.SetActive(false);
        _animator.SetTrigger("TakeRifle");
    }

    private void TakePistol()
    {
        _rifle.SetActive(false);
        _pistol.SetActive(true);
        _map.SetActive(false);
        _animator.SetTrigger("TakePistol");
    }

    private void TakeMap()
    {
        _rifle.SetActive(false);
        _pistol.SetActive(false);
        _map.SetActive(true);
        _animator.SetTrigger("TakeMap");
    }
}