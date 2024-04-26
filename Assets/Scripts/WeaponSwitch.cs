using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    enum Weapon
    {
        rifle = 1,
        pistol,
        map
    }

    [SerializeField] private Weapon _weapon;
    [SerializeField] private GameObject _rifle;
    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _map;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        GunUpdate();
    }

    private void Update()
    {
        SwitchAnimations();
    }

    private void SwitchAnimations()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _animator.SetInteger("GunNumber", 1);
            _weapon = Weapon.rifle;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animator.SetInteger("GunNumber", 2);
            _weapon = Weapon.pistol;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animator.SetInteger("GunNumber", 3);
            _weapon = Weapon.map;
        }
    }

    public void GunUpdate()
    {
        switch (_weapon)
        {
            case Weapon.rifle:
                _animator.SetInteger("GunNumber", 1);
                TakeRifle();
                break;
            case Weapon.pistol:
                _animator.SetInteger("GunNumber", 2);
                TakePistol();
                break;
            case Weapon.map:
                _animator.SetInteger("GunNumber", 3);
                TakeMap();
                break;
        }
    }

    private void TakeRifle()
    {
        _rifle.SetActive(true);
        _pistol.SetActive(false);
        _map.SetActive(false);
    }

    private void TakePistol()
    {
        _rifle.SetActive(false);
        _pistol.SetActive(true);
        _map.SetActive(false);
    }

    private void TakeMap()
    {
        _rifle.SetActive(false);
        _pistol.SetActive(false);
        _map.SetActive(true);
    }
}