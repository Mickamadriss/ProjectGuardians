using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] string _prompt;
    [SerializeField] Transform enter;
    [SerializeField] Transform outer;
    public string InteractionPrompt => _prompt;

    public bool Interact(Interactor interactor)
    {
        if (SfxManager.Instance) SfxManager.Instance.PlaySfx3D(Constants.DOOR, gameObject.transform.position);

        float distanceToEnter = Vector3.Distance(interactor.transform.position, enter.position);
        float distanceToOuter = Vector3.Distance(interactor.transform.position, outer.position);

        if (distanceToEnter < distanceToOuter)
        {
            interactor.transform.position = outer.position;
        }
        else
        {
            interactor.transform.position = enter.position;
        }
        return true;
    }
}