using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyStats : MonoBehaviour, IUnitHealthStats
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    public float Health
    {
        get => health;
        set => health = Mathf.Clamp(value, 0, maxHealth);
    }
    public float MaxHealth
    {
        get => maxHealth;
        set => maxHealth = Mathf.Clamp(value, 0, Mathf.Infinity);
    }
    public bool IsDead { get; set; }
    public bool IsStunned { get; set; }

    public static Action EnemyHitEvent;
    public static Action EnemyDaadEvent;

    private EnemyAnimationController _animationController;

    private void Start()
    {
        _animationController = GetComponent<EnemyAnimationController>();
    }

    public void TakeDamage(float damage, float timeStun)
    {
        if (IsDead) return;

        _animationController.HitAnimation(true);

        Health -= damage;

        EnemyHitEvent?.Invoke();

        if (Health <= 0)
            Dead();

        StartCoroutine(StunTimer(timeStun));
    }

    public IEnumerator StunTimer(float time)
    {
        IsStunned = true;
        yield return new WaitForSeconds(time);
        IsStunned = false;

        if (_animationController)
            _animationController.HitAnimation(false);
    }

    private void Dead()
    {
        IsDead = true;
        EnemyDaadEvent?.Invoke();
        Destroy(gameObject);
    }


}
