using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public InputSystem_Actions inputs;
    [SerializeField] private Vector2 moveInputs;
    private CharacterController controller;

    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float gravity = -9.8f;
    public float verticalVelocity = 0f;

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
        if(controller.isGrounded && verticalVelocity<0)
            verticalVelocity = -2f;
        moveDir.y = verticalVelocity;
        

        controller.Move(moveDir * Time.deltaTime);
    }
}
