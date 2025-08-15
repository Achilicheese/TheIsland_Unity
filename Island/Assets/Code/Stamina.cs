using UnityEngine;
using UnityEngine.UI;

public class Stamina : MonoBehaviour
{
    [SerializeField] private float _maxStamina = 100f;
    [SerializeField] private float _staminaRegenRate = 10f;
    [SerializeField] private float _staminaDrainJump = 15f;
    [SerializeField] private float _staminaDrainSprint = 20f;

    public Image staminaBar;

    private float _currentStamina;
    private bool _isSprinting = false;

    public float CurrentStamina => _currentStamina;
    public float MaxStamina => _maxStamina;
    public bool IsSprinting => _isSprinting;

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

    public void DrainStaminaJump()
    {
        bool canJump = _currentStamina >= _staminaDrainJump;
        if (canJump)
        {
            _currentStamina -= _staminaDrainJump;
            _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);
        }
    }

    public void StopSprinting()
    {
        _isSprinting = false;
    }

    public bool PlayerSprint()
    {
        bool canSprint = _currentStamina >= _staminaDrainSprint;

        if (canSprint)
        {
            _isSprinting = true;
            _currentStamina -= _staminaDrainSprint * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);

            if (_currentStamina <= 0.0f)
            {
                _isSprinting = false;
            }
        }
        else
        {
            _isSprinting = false;
        }

        return _isSprinting;
    }

    private void RegenerateStamina()
    {
        if (_currentStamina < _maxStamina)
        {
            _currentStamina += _staminaRegenRate * Time.deltaTime;
            _currentStamina = Mathf.Clamp(_currentStamina, 0.0f, _maxStamina);
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
