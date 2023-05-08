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
        Debug.Log("start funciton");
        float distanceToEnter = Vector3.Distance(interactor.transform.position, enter.position);
        float distanceToOuter = Vector3.Distance(interactor.transform.position, outer.position);

        if (distanceToEnter < distanceToOuter)
        {
            Debug.Log("teleport to outer");
            interactor.transform.position = outer.position;
        }
        else
        {
            Debug.Log("teleport to enter");
            interactor.transform.position = enter.position;
        }
        return true;
    }
}