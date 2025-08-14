using System;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerControls : MonoBehaviour
{
    private float jumpHeight = 1.0f;
    private float _gravity = -9.81f;
    private float _normalSpeed = 3f;
    private float _sprintSpeed = 5.0f;
    private bool _isGrounded = true;
    private bool _isJumping = false;

    private Vector3 _position;
    private Vector3 _velocity;
    private Vector3 _currentMovementDirection;
    private float _currentSpeed;

    private CharacterController _controller;

    [SerializeField] private Stamina _staminaScript;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _staminaScript = GetComponent<Stamina>();

        if (_staminaScript == null)
        {
            Debug.LogError("Stamina script not found on the same GameObject!");
        }
    }

    void Update()
    {
        player_WASD();
        playerJump();
        ApplyGravity();
    }

    private void player_WASD()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 sideMovement = transform.right * horizontalInput;    // Move left/right
        Vector3 forwardMovement = transform.forward * verticalInput; // Move forward/backward
        _currentMovementDirection = sideMovement + forwardMovement;  // Combine the movement directions

        if (Input.GetKey(KeyCode.Q))
        {
            bool playerSpintlogic = _staminaScript != null && _staminaScript.PlayerSprint();
            if (playerSpintlogic && _currentMovementDirection.magnitude > 0.1f)
            {
                _currentSpeed = _sprintSpeed;
            }
            else
            {
                _currentSpeed = _normalSpeed;
            }
        }
        else
        {
            _currentSpeed = _normalSpeed;
            if (_staminaScript != null)
            {
                _staminaScript.StopSprinting();
            }
        }

        _isGrounded = _controller.isGrounded;
    }

    private void playerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            if (_staminaScript != null && _staminaScript.CurrentStamina >= 15f)
            {
                float jumpLogic = jumpHeight * -_gravity;
                _velocity.y = Mathf.Sqrt(jumpLogic * 2); // 1.0f ^ 2 * -9.81
                _isJumping = true;

                _staminaScript.DrainStaminaJump();
            }
            else if (_staminaScript != null)
            {
                Debug.Log("Not enough stamina to jump!");
            }
        }

        if (_currentMovementDirection.magnitude > 0.1f)
        {
            Vector3 horizontalMovement = _currentMovementDirection * _currentSpeed * Time.deltaTime;
            _controller.Move(horizontalMovement);
        }

        if (_isGrounded && _velocity.y < 0.0f)
        {
            _velocity.y = -2f;
            _isJumping = false;
        }
    }

    private void ApplyGravity()
    {
        _velocity.y += _gravity * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }
}
