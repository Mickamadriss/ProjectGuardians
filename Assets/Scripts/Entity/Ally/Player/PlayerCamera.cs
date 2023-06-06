using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using SDD.Events;

public class PlayerCamera : MonoBehaviour, IEventHandler
{
    public float sensX;
    public float sensY;

    public Transform orientation;

    float xRotation;
    float yRotation;
    
    public bool isInMenu = false;

    #region Events' subscription
    public virtual void SubscribeEvents()
    {
        EventManager.Instance.AddListener<TriggeringMenu>(triggeringMenu);
    }

    public virtual void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<TriggeringMenu>(triggeringMenu);
    }

    #endregion
    
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        //Si un menu est ouvert : ne pas bouger la caméra
        if (isInMenu) return;
        
        //get mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        // rotate cam and orientation
        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }

    private void OnDestroy()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }
    
    #region Event callback

    private void triggeringMenu(TriggeringMenu e)
    {
        Debug.Log(e.menuState);
        isInMenu = e.menuState;
    }
    #endregion
}
