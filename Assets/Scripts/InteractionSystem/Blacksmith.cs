using System;
using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;
using UnityEngine.Purchasing;

public class Blacksmith : MonoBehaviour, IInteractable
{
    [SerializeField] string _prompt;
    public string InteractionPrompt => _prompt;
    private bool isShopOpened = false;

    public void ExitShop()
    {
        //Hide le menu (nécéssaire de réécrire ici, si on quitte le menu avec le bouton quitter)
        EventManager.Instance.Raise(new BlacksmithCloseEvent());
        isShopOpened = false;
    }

    private void OpenShop()
    {
        EventManager.Instance.Raise(new BlacksmithOpenEvent());
        isShopOpened = true;
    }

    public bool Interact(Interactor interactor)
    {
        //Trigger le menu
        if (isShopOpened)
        {
            ExitShop();
        }
        else
        {
            OpenShop();
        }

        return true;
    }
}