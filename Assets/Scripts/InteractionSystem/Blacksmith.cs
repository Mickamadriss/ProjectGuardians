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
    private bool isShopOpened = true;

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
        m_PanelBlacksmith.SetActive(!m_PanelBlacksmith.activeSelf);
        isShopOpened = !isShopOpened;
        
        //Open le menu
        EventManager.Instance.Raise(new TriggeringMenu() { menuState = isShopOpened });
        
        return true;
    }
}