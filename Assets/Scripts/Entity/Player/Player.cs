using System;
using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableEntity, IEventHandler
{
    private bool m_IsPlaying = false;
    private WeaponBehaviour weapon;

    private void Awake()
    {
        SubscribeEvents();
        weapon = this.GetComponentInChildren<WeaponBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsPlaying)
        {
            return;
        }

        Debug.Log(weapon.getIsAttacking());
        Mouvement();
        Attack();
    }

    void onDestroy()
    {
        UnsubscribeEvents();
    }

    #region Event's subscription

    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<PlayButtonClickedEvent>(PlayButtonClicked);
    }

    #endregion

    #region Event's callback

    void PlayButtonClicked(PlayButtonClickedEvent e)
    {
        m_IsPlaying = true;
    }

    #endregion

    private void Mouvement()
    {
        float hInput, vInput, mouseX;
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X");

        Move(transform.forward * vInput);
        Move(transform.right * hInput);
        Rotate(Vector3.up * mouseX);
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (weapon.canAttack())
            {
                Debug.Log("attackiiing");
                weapon.Attack();
            }
        }
    }
}