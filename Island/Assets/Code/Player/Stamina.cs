using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaRegenRate = 10f;
    [SerializeField] private float _staminaDrainJump = 30f;
    [SerializeField] private float _staminaDrainSprint = 10f;
    [SerializeField] private float _staminaDelayTimer = 1.0f;

    public Image staminaBar;
    private float _currentStamina;
    private bool _isSprinting = false;
    private bool _canJump = true;
    
    private float _lastStaminaUseTime;
    private bool _staminaWasUsed = false;

    public float CurrentStamina => _currentStamina;
    public float MaxStamina => _maxStamina;
    public bool IsSprinting => _isSprinting;
    public bool CanJump => _canJump;

    void Start()
    {
        _currentStamina = _maxStamina;
        UpdateStaminaBar();
    }

    void Update()
    {
        if (_isSprinting)
        {
            PlayerSprint();
        }
        else
        {
            RegenerateStamina();
        }

        UpdateStaminaBar();
    }

    public bool DrainStaminaJump()
    {
         if (_currentStamina <= _staminaDrainJump)
        {
            _canJump = false;
            return false;
        }
        
        _currentStamina -= _staminaDrainJump;
        _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);
        _lastStaminaUseTime = Time.time;
        _staminaWasUsed = true;
        
        _canJump = true;
        return true;
    }

    public void StopSprinting()
    {
        _isSprinting = false;
        if (_staminaWasUsed)
        {
            _lastStaminaUseTime = Time.time;
        }
    }

    public bool PlayerSprint()
    {
        if (_currentStamina <= 0f)
        {
            StopSprinting();
            return false;
        }

        _currentStamina -= _staminaDrainSprint * Time.deltaTime;
        _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);
        _lastStaminaUseTime = Time.time;
        _staminaWasUsed = true;
        
        _isSprinting = true;
        return _isSprinting;
    }

    private void RegenerateStamina()
    {
        if (!_staminaWasUsed)
        {
            if (_currentStamina < _maxStamina)
            {
                _currentStamina += _staminaRegenRate * Time.deltaTime;
                _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);
            }
            return;
        }

        if (Time.time - _lastStaminaUseTime >= _staminaDelayTimer)
        {
            if (_currentStamina < _maxStamina)
            {
                _currentStamina += _staminaRegenRate * Time.deltaTime;
                _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);
                
                if (_currentStamina >= _maxStamina)
                {
                    _staminaWasUsed = false;
                }
            }
        }
    }

    private void UpdateStaminaBar()
    {
        if (staminaBar != null)
        {
            staminaBar.fillAmount = _currentStamina / _maxStamina;
        }
    }
}