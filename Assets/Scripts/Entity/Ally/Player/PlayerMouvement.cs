using SDD.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouvement : MonoBehaviour
{
    public Player player;

    [Header("Mouvement")]
    private float moveSpeed;
    public float walkSpeed;
    public float runSpeed;
    public float dashSpeed;
    public float dashSpeedChangedFactor;
    public float maxYSpeed;
    public float groundDrag;

    [Header("Jump")]
    public float playerHeight;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Ground Check")]
    public LayerMask whatIsGround;
    bool grounded;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode runKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementSate state;

    public enum MovementSate
    {
        walking,
        running,
        crouching,
        dashing,
        air
    }

    public bool dashing;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        startYScale = transform.localScale.y;
    }

    private void Update()
    {
        if (!player.m_IsPlaying) return;
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);

        MyInput();
        SpeedControl();
        StateHandler();

        //handle drag
        if (grounded && state != MovementSate.dashing)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!player.m_IsPlaying) return;
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
        //when to jump
        if(Input.GetKey(jumpKey) && readyToJump && grounded)
        {
            Jump();
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if (Input.GetKeyDown(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse); // make you go on the ground instantly to avoid floating
        }

        if (Input.GetKeyUp(crouchKey))
        {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;
    private MovementSate lastState;
    private bool keepMomentum;

    private void StateHandler()
    {
        if (dashing)
        {
            state = MovementSate.dashing;
            desiredMoveSpeed = dashSpeed;
            speedChangeFactor = dashSpeedChangedFactor;
        }
        else if (Input.GetKey(crouchKey))
        {
            state = MovementSate.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        else if(grounded && Input.GetKey(runKey))
        {
            state = MovementSate.running;
            desiredMoveSpeed = runSpeed;
        }
        else if (grounded)
        {
            state = MovementSate.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else
        {
            state = MovementSate.air;

            if (desiredMoveSpeed < runSpeed) desiredMoveSpeed = walkSpeed;
            else desiredMoveSpeed = runSpeed;
        }

        bool desiredMoveSpeedChanged = desiredMoveSpeed != lastDesiredMoveSpeed;
        if (lastState == MovementSate.dashing) keepMomentum = true;

        if (desiredMoveSpeedChanged)
        {
            if (keepMomentum)
            {
                StopAllCoroutines();
                StartCoroutine(SmoothlyLerpMoveSpeed());
            }
            else
            {
                StopAllCoroutines();
                moveSpeed = desiredMoveSpeed;
            }
        }

        lastDesiredMoveSpeed = desiredMoveSpeed;
        lastState = state;
    }

    private float speedChangeFactor;

    private IEnumerator SmoothlyLerpMoveSpeed()
    {
        //Smoothly lerp movementSpeed to desired value
        float time = 0;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startValue = moveSpeed;

        float boostFactor = speedChangeFactor;

        while (time < difference)
        {
            moveSpeed = Mathf.Lerp(startValue, desiredMoveSpeed, time / difference);

            time += Time.deltaTime * boostFactor;

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
        speedChangeFactor = 1f;
        keepMomentum = false;
    }

    private void MovePlayer()
    {
        //calcule movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (grounded)
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        else
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
    }

    private void SpeedControl()
    {
        Vector3 flatvel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        if(flatvel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatvel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }

        //limit y velocity
        if (maxYSpeed != 0 && rb.velocity.y > maxYSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxYSpeed, rb.velocity.z);
        }
    }

    private void Jump()
    {
        //reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }
}
