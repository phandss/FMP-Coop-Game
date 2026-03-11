using UnityEngine;
using System;

public class HumanHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100f;
    private float _currentHealth;

    public float CurrentHealth => _currentHealth;
    public bool IsDead => _currentHealth <= 0;

    public event Action OnDeath;
    public event Action<float> OnHealthChanged;

    private void Awake()
    {
        _currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead)
        { 
            return;
        }

        _currentHealth = Mathf.Max(_currentHealth - amount, 0);
        OnHealthChanged?.Invoke(_currentHealth);

        Debug.Log($"Human took {amount} damage, current health: {_currentHealth}");

        if (IsDead)
        {
            OnDeath?.Invoke();
        }
    }

    public void Heal(float amount)
    {
            if (IsDead)
            {
                return;
            }
    
            _currentHealth = Mathf.Min(_currentHealth + amount, maxHealth);
            OnHealthChanged?.Invoke(_currentHealth);
    }
}
