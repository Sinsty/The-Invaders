using UnityEngine;

public class WeaponPullOutEvent : MonoBehaviour
{
    [SerializeField] private WeaponSwitch _weaponSwitchScript;

    public void PullOutWeaponEvent()
    {
        _weaponSwitchScript.GunUpdate();
    }
}
