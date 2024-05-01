using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SerializeField] private int _startGunNumber = 1;
    [SerializeField] private GameObject _rifle;
    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _map;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.SetInteger("GunNumber", _startGunNumber);
        GunUpdate();
    }

    private void Update()
    {
        if (_animator.GetCurrentAnimatorStateInfo(1).IsName("Check") == false)
        {
            SwitchAnimations();
        }
    }

    private void SwitchAnimations()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _animator.SetInteger("GunNumber", 1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _animator.SetInteger("GunNumber", 2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _animator.SetInteger("GunNumber", 3);
        }
    }

    public void GunUpdate()
    {
        int gunNumber = _animator.GetInteger("GunNumber");
        switch (gunNumber)
        {
            case 1:
                TakeRifle();
                break;
            case 2:
                TakePistol();
                break;
            case 3:
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