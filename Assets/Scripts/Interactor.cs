using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private KeyCode _interactKey = KeyCode.E;
    [SerializeField] private LayerMask _interactLayerMask;
    [SerializeField] private Camera _interactionSourceCamera;
    [SerializeField] private float _interactRange = 3;

    private void Update()
    {
        Interact();
    }

    private void Interact()
    {
        if (Input.GetKeyDown(_interactKey))
        {
            Ray ray = _interactionSourceCamera.ViewportPointToRay(new Vector2(0.5f, 0.5f));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, _interactRange, _interactLayerMask))
            {
                IInteractable interactObject;
                if (hit.collider.gameObject.TryGetComponent<IInteractable>(out interactObject))
                {
                    interactObject.Interact();
                }
            }
        }
    }
}