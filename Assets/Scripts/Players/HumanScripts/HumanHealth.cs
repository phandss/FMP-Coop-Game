using UnityEngine;
using System;

public class HumanHealth : MonoBehaviour
{
    [SerializeField] public float maxHealth = 100f;

    private float currentHealth; // actual health value

    public float CurrentHealth
    {
        get { return currentHealth; }
    }

    public bool IsDead
    {
        get { return currentHealth <= 0f; }
    }

    public event Action OnDeath;
    public event Action<float> OnHealthChanged;

    private void Awake()
    {
        // start full health
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        if (IsDead)
        {
            // already dead so nothing to do
            return;
        }

        // subtract damage
        currentHealth -= amount;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        
        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(currentHealth);
        }

        Debug.Log("Human took " + amount + " damage. Health now: " + currentHealth);

        // check death after damage
        if (currentHealth <= 0)
        {
            // TODO maybe add ragdoll or animation here
            if (OnDeath != null)
            {
                OnDeath.Invoke();
            }
        }
    }

    public void Heal(float amount)
    {
        if (IsDead)
        {
            return; // can't heal if dead
        }

        currentHealth += amount;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(currentHealth);
        }
    }

    public void Respawn()
    {
        currentHealth = maxHealth;

        if (OnHealthChanged != null)
        {
            OnHealthChanged.Invoke(currentHealth);
        }
    }

}
