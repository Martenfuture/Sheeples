using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    //input fields
    //private ThirdPersonActionsAsset playerActionsAsset;
    private InputActionAsset inputAsset;
    private InputActionMap player;
    private InputAction move;
    private InputAction run;

    //movement fields
    private Rigidbody rb;
    [SerializeField]
    private float movementForce = 1f;
    private float maxForce = 50f;
    [SerializeField]
    private float jumpForce = 5f;
    private float maxSpeed = 10f;
    [SerializeField] float maxWalkSpeed;
    [SerializeField] float maxSprintSpeed;
    private Vector3 forceDirection = Vector3.zero;

    public bool isRunning;

    [SerializeField]
    private Camera playerCamera;
    private Animator animator;

    public Vector3 RespawnPosition;

    void Start()
    {
        RespawnPosition = transform.position;
    }



    private void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
        animator = this.GetComponent<Animator>();

        //playerActionsAsset = new ThirdPersonActionsAsset();
        inputAsset = this.GetComponent<PlayerInput>().actions;
        player = inputAsset.FindActionMap("Player");
        
    }

    private void OnEnable()
    {
        //playerActionsAsset.Player.Jump.started += DoJump;
        //playerActionsAsset.Player.Attack.started += DoAttack;
        //move = playerActionsAsset.Player.Move;
        //playerActionsAsset.Player.Enable();
        player.FindAction("Jump").started += DoJump;
        player.FindAction("Attack").started += DoAttack;
        move = player.FindAction("Move");
        player.FindAction("Run").started += DoRun;
        player.FindAction("Run").canceled += StopRun;
        player.Enable();
    }

    private void OnDisable()
    {
        //playerActionsAsset.Player.Jump.started -= DoJump;
        //playerActionsAsset.Player.Attack.started -= DoAttack;
        //playerActionsAsset.Player.Disable();
        player.FindAction("Jump").started -= DoJump;
        player.FindAction("Attack").started -= DoAttack;
        player.FindAction("Run").started -= DoRun;
        player.FindAction("Run").canceled -= StopRun;
        player.Disable();
    }

    private void Update()
    {
        gameObject.GetComponent<Animator>().SetFloat("movementSpeed", gameObject.GetComponent<Rigidbody>().velocity.magnitude);
    }

    private void FixedUpdate()
    {
        forceDirection += move.ReadValue<Vector2>().x * GetCameraRight(playerCamera) * movementForce;
        forceDirection += move.ReadValue<Vector2>().y * GetCameraForward(playerCamera) * movementForce;

        rb.AddForce(forceDirection, ForceMode.Impulse);
        forceDirection = Vector3.zero;

        if (rb.velocity.y < 0f)
            rb.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;

        Vector3 horizontalVelocity = rb.velocity;
        horizontalVelocity.y = 0;
        if (horizontalVelocity.sqrMagnitude > maxSpeed * maxSpeed)
            rb.velocity = horizontalVelocity.normalized * maxSpeed + Vector3.up * rb.velocity.y;


        LookAt();

        
    }
    private void DoRun(InputAction.CallbackContext obj)
    {
        maxSpeed = maxSprintSpeed;
    }
    private void StopRun(InputAction.CallbackContext obj)
    {
        maxSpeed = maxWalkSpeed;
    }

    private void LookAt()
    {
        Vector3 direction = rb.velocity;
        direction.y = 0f;

        if (move.ReadValue<Vector2>().sqrMagnitude > 0.1f && direction.sqrMagnitude > 0.1f)
            this.rb.rotation = Quaternion.LookRotation(direction, Vector3.up);
        else
            rb.angularVelocity = Vector3.zero;
    }

    private Vector3 GetCameraForward(Camera playerCamera)
    {
        Vector3 forward = playerCamera.transform.forward;
        forward.y = 0;
        return forward.normalized;
    }

    private Vector3 GetCameraRight(Camera playerCamera)
    {
        Vector3 right = playerCamera.transform.right;
        right.y = 0;
        return right.normalized;
    }

    private void DoJump(InputAction.CallbackContext obj)
    {
        if (IsGrounded() && jumpForce != 0)
        {
            forceDirection += Vector3.up * jumpForce;
            gameObject.GetComponent<Animator>().SetTrigger("jumping");
        }
    }

    private bool IsGrounded()
    {
        Ray ray = new Ray(this.transform.position + Vector3.up * 0.25f, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 0.5f))
            return true;
        else
            return false;
    }

    private void DoAttack(InputAction.CallbackContext obj)
    {
        animator.SetTrigger("Attack");
    }



}