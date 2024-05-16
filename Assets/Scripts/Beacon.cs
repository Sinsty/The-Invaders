using UnityEngine;
using UnityEngine.Events;

public class Beacon : MonoBehaviour
{
    public static UnityEvent OnUsed = new UnityEvent();

    [SerializeField] private float _turningOffSpeed = 1;
    [SerializeField] private GameObject _light;
    [SerializeField] private BeaconRotation _rotatingPanels;

    public bool IsUsed { get; private set; } = false;

    private void Update()
    {
        if (IsUsed)
        {
            TurnOff();
        }
    }

    private void TurnOff()
    {
        if (_light.transform.localScale.magnitude >= 0.05 || _rotatingPanels.RotationSpeed >= 0.05f)
        {
            _light.transform.localScale = Vector3.Lerp(_light.transform.localScale, Vector3.up * 0.01f, _turningOffSpeed * Time.deltaTime);
            _rotatingPanels.RotationSpeed = Mathf.Lerp(_rotatingPanels.RotationSpeed, 0.01f, _turningOffSpeed * Time.deltaTime);
        }
        else if (_light.activeInHierarchy)
        {
            _light.SetActive(false);
        }
        else
        {
            _rotatingPanels.RotationSpeed = 0;
        }
    }

    public void Use()
    {
        print("Beacon Used");
        IsUsed = true;
        OnUsed.Invoke();
    }
}
