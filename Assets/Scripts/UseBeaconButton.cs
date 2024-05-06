using UnityEngine;

public class UseBeaconButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Beacon _beacon;

    public void Interact()
    {
        print("ButtonPressed");
        _beacon.Use();
    }
}
