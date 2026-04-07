 using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputSystem_Actions inputs;
    [SerializeField] private Vector2 moveInputs;
    private CharacterController controller;
    //Movement variables
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float gravity = -9.8f;
    public float verticalVelocity ;
    //Dash variables
    public bool isDashing;
    public float dashForce;
    public float dashDuration;
    public float dashTimer;
    //Dash cooldown variables
    public float DashCooldown;


    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputs = new();
    }
    private void OnEnable()
    {
        inputs.Enable();
        inputs.Player.Move.performed += ctx => moveInputs = ctx.ReadValue<Vector2>();
        inputs.Player.Move.canceled += ctx => moveInputs = Vector2.zero;

        inputs.Player.Sprint.performed += OnSprint;
        inputs.Player.Dash.performed += OnDash;


        inputs.Player.Attack.performed += OnAttack;

       
    }

    

    void Start()
    {

    }

    void Update()
    {
        OnMove();
    }
    public void OnMove()
    {
        transform.Rotate(Vector3.up * moveInputs.x * rotationSpeed * Time.deltaTime);
        Vector3 moveDir = transform.forward * moveSpeed * moveInputs.y;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if (controller.isGrounded && verticalVelocity < 0)

        verticalVelocity = -2f;
        moveDir.y = verticalVelocity;


        
        if (isDashing)
        {
            moveDir = transform.forward * dashForce * (dashTimer / dashDuration);
            dashTimer -= Time.deltaTime;

            if (dashTimer <= 0)
            {
                dashTimer = 0;
                isDashing = false;

            }
        }

        if(!isDashing && DashCooldown > 0)
        {
            DashCooldown -= Time.deltaTime;
        }


        controller.Move(moveDir * Time.deltaTime);
    }
    private void OnDash(InputAction.CallbackContext context)
    {
            isDashing = true;
            dashTimer = dashDuration;

    }
    private void OnSprint(InputAction.CallbackContext context)
    {
        moveInputs.y *= 2; // Double the forward movement speed when sprinting       
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);

    }
    private void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("Attack");
    }
}