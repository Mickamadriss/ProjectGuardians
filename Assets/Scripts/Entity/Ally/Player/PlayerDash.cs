using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    public Player player;

    [Header("References")]
    public Transform orientation;
    public Transform playerCam;
    private Rigidbody rb;
    private PlayerMouvement pm;

    [Header("Dashing")]
    public float dashForce;
    public float dashUpwardForce;
    public float dashDuration;
    public float maxDashYSpeed;

    [Header("Settings")]
    public bool useCameraForward = true;
    public bool allowAllDirection = true;
    public bool resetVel = true;

    [Header("Cooldown")]
    public float dashCd;
    private float dashCdTimer;

    [Header("Input")]
    public KeyCode dashKey = KeyCode.A;

    [Header("CameraEffects")]
    public PlayerCamera cam;
    public float dashFov;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMouvement>();
    }

    private void Update()
    {
        if (!player.m_IsPlaying) return;
        if (Input.GetKeyDown(dashKey))
        {
            Dash();
        }

        if (dashCdTimer > 0) dashCdTimer -= Time.deltaTime;
    }

    private void Dash()
    {
        if (dashCdTimer > 0) return;
        else dashCdTimer = dashCd;
        pm.dashing = true;
        pm.maxYSpeed = maxDashYSpeed;

        cam.DoFov(dashFov);

        Transform forwardT;
        if (useCameraForward) forwardT = playerCam;
        else forwardT = orientation;

        Vector3 direction = GetDirection(forwardT);

        delayedForceToApply = direction * dashForce + orientation.up * dashUpwardForce;
        Invoke(nameof(DelayedDashForce), 0.025f);
        Invoke(nameof(dashReset), dashDuration);
    }

    private Vector3 delayedForceToApply;
    private void DelayedDashForce()
    {
        rb.velocity = Vector3.zero;
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
    }

    private void dashReset()
    {
        cam.DoFov(85f);
        pm.maxYSpeed = 0;
        pm.dashing = false;
    }

    private Vector3 GetDirection(Transform forwardT)
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3();

        if (allowAllDirection) direction = forwardT.forward * verticalInput + forwardT.right * horizontalInput;
        else direction = forwardT.forward;

        if(verticalInput == 0 && horizontalInput ==0) direction = forwardT.forward;

        return direction.normalized;
    }
}
