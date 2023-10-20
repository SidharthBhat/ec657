using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework.Internal;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerControls ;
    private PlayerInput.PlayerActions actions;
    [SerializeField] CharacterController characterController;
    [SerializeField] float speed = 12f;
    [SerializeField] float gravity = -9.8f;
    [SerializeField] float jumpHeigth = 2f;
    [SerializeField] Transform groundCheck;
    [SerializeField] Vector3 groundCheckDimension = new Vector3(1f, 0.5f, 1f);
    [SerializeField] LayerMask groundMask;
    Vector3 velocity;
    bool isGrounded;
    [SerializeField] GameObject projectile;
    [SerializeField] float projSpeed;
    [SerializeField] float cooldown;
    private float lastShot;

    void Awake()
    {
        playerControls = new PlayerInput();
        actions = playerControls.Player;
    }
    void OnEnable()
    {
        playerControls.Enable();
    }

    void OnDisable()
    {
        playerControls.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Move();
        Shoot();
            
    }

    void Move()
        {
        Vector2 direction = actions.Movement.ReadValue<Vector2>();
        Vector3 move = transform.right * direction.x + transform.forward * direction.y;
        characterController.Move(move * speed * Time.deltaTime);

    }

    void Jump()
    {
        isGrounded = Physics.CheckBox(groundCheck.position, groundCheckDimension, Quaternion.identity, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }
        if (actions.Jump.IsPressed() && isGrounded)
        {
            velocity.y = Mathf.Sqrt( jumpHeigth * -2f * gravity);
        }

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    void Shoot() {
        if (actions.Shoot.IsPressed()) {
            if (Time.time - lastShot > cooldown ){
                GameObject currentprojectile = Instantiate(projectile, transform.position+transform.forward, Quaternion.identity);
                currentprojectile.GetComponent<Rigidbody>().AddForce(transform.forward * projSpeed, ForceMode.Impulse);
                lastShot = Time.time;
            }
        }
    }
    
}
