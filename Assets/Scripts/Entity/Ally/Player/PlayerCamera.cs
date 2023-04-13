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

    private void Start()
    {
        SubscribeEvents();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
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
        UnsubscribeEvents();
    }

    #region Events' subscription
    public void SubscribeEvents()
    {
        EventManager.Instance.AddListener<GameResumeEvent>(DisableMouse);
        EventManager.Instance.AddListener<GamePauseEvent>(EnableMouse);
        EventManager.Instance.AddListener<GameOverEvent>(EnableMouse);
        EventManager.Instance.AddListener<GameMenuEvent>(EnableMouse);
    }

    public void UnsubscribeEvents()
    {
        EventManager.Instance.RemoveListener<GameResumeEvent>(DisableMouse);
        EventManager.Instance.RemoveListener<GamePauseEvent>(EnableMouse);
        EventManager.Instance.RemoveListener<GameOverEvent>(EnableMouse);
        EventManager.Instance.RemoveListener<GameMenuEvent>(EnableMouse);
    }

    #endregion

    #region Event callback

    private void DisableMouse(GameResumeEvent e)
    {
        Debug.Log("souris desactivated");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void EnableMouse(SDD.Events.Event e)
    {
        Debug.Log("souris activated");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    #endregion

    public void DoFov(float endValue)
    {
        GetComponent<Camera>().DOFieldOfView(endValue, 0.25f);
    }
}
