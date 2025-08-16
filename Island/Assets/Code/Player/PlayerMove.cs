using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _normalSpeed = 2f;
    [SerializeField] private float _runningSpeed = 4f;
    [SerializeField] private float _jumpHeight = 1f;
    [SerializeField] private Stamina _stamina;

    private bool _isGrounded;
    private bool _isMoving;
    private float _gravity = -9.81f;
    private CharacterController _player;
    private Vector3 _velocity;
    private float currentSpeed;

    void Start()
    {
        _player = GetComponent<CharacterController>();
        _stamina = GetComponent<Stamina>();
    }

    void Update()
    {
        CheckGrounded();
        WASD();
        Jump();
        Gravity();
    }

    private void CheckGrounded()
    {
        _isGrounded = _player.isGrounded;
    }

    private void WASD()
    {
        float move_Right_Left = Input.GetAxis("Horizontal");    
        float move_Forwards_Backwards = Input.GetAxis("Vertical");

        Vector3 RightLeft = transform.right * move_Right_Left;
        Vector3 ForwardBack = transform.forward * move_Forwards_Backwards;

        Vector3 move = (RightLeft + ForwardBack);

        if (Input.GetKey(KeyCode.Q) && _isMoving)
        {
            if (_stamina.PlayerSprint())
            {
                currentSpeed = _runningSpeed;
            }
            else
            {
                currentSpeed = _normalSpeed;
                _stamina.StopSprinting();
            }
        }
        else
        {
            currentSpeed = _normalSpeed;
            _stamina.StopSprinting();
        }

        _isMoving = (move.magnitude > 0.1f);
        _player.Move(move * currentSpeed * Time.deltaTime);
    }

    private void Jump()
    {
        if (_isGrounded && Input.GetButtonDown("Jump"))
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
            _stamina.DrainStaminaJump();
        }
    }

    private void Gravity()
    {
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }
        else
        {
            _velocity.y += _gravity * Time.deltaTime;
        }

        _player.Move(_velocity * Time.deltaTime);
    }
}