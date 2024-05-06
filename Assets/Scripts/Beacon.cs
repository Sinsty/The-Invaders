using UnityEngine;
using UnityEngine.Events;

public class Beacon : MonoBehaviour
{
    public static UnityEvent OnUsed = new UnityEvent();

    public bool IsUsed { get; private set; } = false;

    public void Use()
    {
        print("Beacon Used");
        IsUsed = true;
        OnUsed.Invoke();
    }
}
