using System;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    [SerializeField] private int maxHealth = 100;

    public int Health { get; private set; }
    public bool IsAlive => Health > 0;

    public event Action<int> Damaged;
    public event Action Died;

    private void Awake()
    {
        Health = maxHealth;
    }

    public void Damage(int amount)
    {
        if (!IsAlive || amount <= 0)
            return;

        Health = Mathf.Max(Health - amount, 0);

        Damaged?.Invoke(amount);

        if (!IsAlive)
            Died?.Invoke();
    }

    public void Heal(int amount)
    {
        if (!IsAlive || amount <= 0)
            return;

        Health = Mathf.Min(Health + amount, maxHealth);
    }
}