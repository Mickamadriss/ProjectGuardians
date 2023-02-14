using SDD.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MovableEntity, IEventHandler
{
    [SerializeField] private float m_RotationSpeed = 100f;
    private bool m_IsPlaying = false;

    private void Awake()
    {
        SubscribeEvents();
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_IsPlaying)
        {
            return;
        }
        Mouvement();
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

        Move(transform.forward * speed * vInput * Time.fixedDeltaTime);
        Move(transform.right * speed * hInput * Time.fixedDeltaTime);
        Rotate(Vector3.up * m_RotationSpeed * mouseX * Time.fixedDeltaTime);
    }
}