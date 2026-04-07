using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonController : MonoBehaviour
{
    public InputSystem_Actions inputs;
    [SerializeField] private Vector2 moveInputs;
    private CharacterController controller;
    public CinemachineCamera characterController;
    public int Life = 10;

    //Movement variables
    public float moveSpeed = 5f;
    public float rotationSpeed = 200f;
    public float gravity = -9.8f;
    public float verticalVelocity;
    //Dash variables
    public bool isDashing;
    public float dashForce;
    public float dashDuration;
    public float dashTimer;
    
    //Referencias
    public GameObject BombPrefab;
    public Transform BombSpawnRef;

    public float SpawnBombcounter;
    public float SpawnBombInterval;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputs = new();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
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
        PlayerDead();
        SpawnBombcounter += Time.deltaTime;
        OnMove();
    }
    public void OnMove()
    {
        Vector3 cameraForwardDir = characterController.transform.forward;
        cameraForwardDir.y = 0;
        cameraForwardDir.Normalize();

        if (moveInputs != Vector2.zero) 
        {

            Quaternion targetQuaternion = Quaternion.LookRotation(cameraForwardDir);           
            transform.rotation = Quaternion.Slerp(transform.rotation, targetQuaternion, rotationSpeed * Time.deltaTime);         
            Vector3 moveDir = (cameraForwardDir * moveInputs.y + transform.right * moveInputs.x) * moveSpeed;

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
            controller.Move(moveDir * Time.deltaTime);


        }

        
    }
    private void OnDash(InputAction.CallbackContext context)
    {
        isDashing = true;
        dashTimer = dashDuration;

    }
    private void OnSprint(InputAction.CallbackContext context)
    {
        moveInputs.y *= 2; 
    }
    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 2);

    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        
        if(SpawnBombcounter >= SpawnBombInterval)
        {            
            GameObject Obj = Instantiate(BombPrefab, BombSpawnRef.position, Quaternion.identity);            
            SpawnBombcounter = 0;
        }
        
    }
    public void RecieveDamage(int damage)
    {
        damage = 1;
        Life -=damage;

    }
    public void PlayerDead()
    {
        if(Life <= 0)
        {
            Destroy(gameObject);
        }
    }
   

}
