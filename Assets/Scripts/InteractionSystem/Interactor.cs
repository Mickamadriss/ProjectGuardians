using SDD.Events;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius;
    [SerializeField] private LayerMask _interactionMask;

    private readonly Collider[] _colliders = new Collider[3];
    [SerializeField] private int _numFound;
    bool isPromptDraw = false;

    private void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactionMask);
        Debug.Log(_numFound);
        if(_numFound > 0)
        {
            var interactableCollider = _colliders[0].GetComponentInParent<IInteractable>();
            Debug.Log(interactableCollider);
            if (interactableCollider != null)
            {
                EventManager.Instance.Raise(new DrawInteractionHud() { prompt = interactableCollider.InteractionPrompt });
                isPromptDraw = true;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactableCollider.Interact(this);
                }
            }
        }
        else if (isPromptDraw)
        {
            isPromptDraw = false;
            EventManager.Instance.Raise(new EraseInteractionHud() { });
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}
