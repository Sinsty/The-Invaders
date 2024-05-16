using UnityEngine;

public class UseBeaconButton : MonoBehaviour, IInteractable
{
    [SerializeField] private Beacon _beacon;
    [SerializeField] private Animator _buttonAnimator;
    [SerializeField] private GameObject _UIButtonVisual;

    public void Interact()
    {
        _buttonAnimator.SetTrigger("Use");
        _UIButtonVisual.SetActive(false);
        _beacon.Use();
    }
}
