using UnityEngine;
using UnityEngine.Events;

public class Beacon : MonoBehaviour
{
    public static UnityEvent OnUsed = new UnityEvent();

    [SerializeField] private float _turningOffSpeed = 1;
    [SerializeField] private GameObject _light;

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
        _light.transform.localScale = Vector3.Lerp(_light.transform.localScale, Vector3.up * 0.01f, _turningOffSpeed * Time.deltaTime);
    }

    public void Use()
    {
        print("Beacon Used");
        IsUsed = true;
        OnUsed.Invoke();
    }
}
