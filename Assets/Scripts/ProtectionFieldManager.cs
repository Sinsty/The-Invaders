using UnityEngine;

public class ProtectionFieldManager : MonoBehaviour
{
    [SerializeField] private Beacon[] _allBeacons = new Beacon[0];
    [SerializeField] private ProtectionField _protectionField;

    private void OnEnable()
    {
        Beacon.OnUsed.AddListener(UpdateFieldState);
    }

    private void UpdateFieldState()
    {
        int usedBeaconsCount = 0;
        foreach (Beacon beacon in _allBeacons)
        {
            if (beacon.IsUsed)
            {
                usedBeaconsCount++;
            }
        }

        if (usedBeaconsCount == _allBeacons.Length)
        {
            _protectionField.SwitchOff();
        }
    }

    private void OnDisable()
    {
        Beacon.OnUsed.RemoveListener(UpdateFieldState);
    }
}