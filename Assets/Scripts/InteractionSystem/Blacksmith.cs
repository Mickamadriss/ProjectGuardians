using System;
using System.Collections;
using System.Collections.Generic;
using SDD.Events;
using UnityEngine;
using UnityEngine.Purchasing;

public class Blacksmith : MonoBehaviour, IInteractable
{
    [SerializeField] string _prompt;
    [SerializeField] GameObject m_PanelBlacksmith;
    public string InteractionPrompt => _prompt;
    private bool isShopOpened = false;

    public void ExitShop()
    {
        //Hide le menu (nécéssaire de réécrire ici, si on quitte le menu avec le bouton quitter)
        m_PanelBlacksmith.SetActive(false);
        isShopOpened = false;
        
        //Affiche curseur
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        //Trigger l'event pour bloquer ou non (cam/déplacement) le player
        EventManager.Instance.Raise(new TriggeringMenu() { menuState = isShopOpened });
    }
    public void OpenShop()
    {
        //Affiche curseur
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        //Trigger l'event pour bloquer ou non (cam/déplacement) le player
        EventManager.Instance.Raise(new TriggeringMenu() { menuState = isShopOpened });
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
                //
                // m_PanelBlacksmith.SetActive(false);
                // isShopOpened = false;
            }
        }
    }

    public bool Interact(Interactor interactor)
    {
        //Trigger le menu
        m_PanelBlacksmith.SetActive(!m_PanelBlacksmith.activeSelf);
        isShopOpened = !isShopOpened;

        if (isShopOpened == false)
        {
            ExitShop();
        }
        else
        {
            OpenShop();
        }

        return true;
    }

    public void BuyItem(IWeapon weaponToBuy)
    {
        return;
    }
}