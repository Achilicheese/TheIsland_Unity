using UnityEngine;

public class SamanthaHealth : TheTwinsHealth
{
    [SerializeField] private Collider _SamanthaCollider;
    private float _maxHealth = 100f;
    private float _currentHealth;

    public void Awake()
    {
        _SamanthaCollider = GetComponent<Collider>();
        
    }

    public void Update()
    {

    }
}