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
        EventManager.Instance.Raise(new BlacksmithOpenEvent());
        isShopOpened = false;
        
        
        //Trigger l'event pour bloquer ou non (cam/déplacement) le player
        // EventManager.Instance.Raise(new TriggeringMenu() { menuState = isShopOpened });
    }
    
    private void Update()
    {
        // //Fait l'update que si le menu est actuellement ouvert
        if (isShopOpened)
        {
            //Ferme le shop si press "E"
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Ferme le menu
                // EventManager.Instance.Raise(new TriggeringMenu() { menuState = false });
                EventManager.Instance.Raise(new BlacksmithCloseEvent());
                isShopOpened = false;
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        //Trigger le menu
        isShopOpened = !isShopOpened;

        if (isShopOpened == false)
        {
            EventManager.Instance.Raise(new BlacksmithCloseEvent());
        }
        else
        {
            EventManager.Instance.Raise(new BlacksmithOpenEvent());
        }

        return true;
    }
}