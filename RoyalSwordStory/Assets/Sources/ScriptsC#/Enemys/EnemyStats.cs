using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyStats : MonoBehaviour, IUnitHealthStats, ISwitchColorHit
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private Color hitColor = Color.red;

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
    public Color StartColor { get; set; }
    public Color HitColor
    {
        get => hitColor;
        set => hitColor = value;
    }
    public SpriteRenderer SpriteRenderer { get; set; }

    public Action<bool> EnemyStanEvent;
    public static Action EnemyAnyHitEvent;
    public static Action EnemyAnyDeadEvent;

    private HealthBar _healthBar;
    private EnemyAnimationController _animationController; 

    private void Start()
    {
        _animationController = GetComponent<EnemyAnimationController>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartColor = SpriteRenderer.color;
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.Unpin();
    }

    public void TakeDamage(float damage, float timeStun)
    {
        if (IsDead) return;

        _animationController.HitAnimation(true);

        Health -= damage;

        EnemyStanEvent?.Invoke(true);
        EnemyAnyHitEvent?.Invoke();

        _healthBar.SetHealth(Health, MaxHealth);

        StartCoroutine(SwitchColorHit());

        if (Health <= 0)
            Dead();

        StartCoroutine(StunTimer(timeStun));
    }

    public IEnumerator StunTimer(float time)
    {
        IsStunned = true;
        yield return new WaitForSeconds(time);
        IsStunned = false;

        EnemyStanEvent?.Invoke(false);

        if (_animationController)
            _animationController.HitAnimation(false);
    }

    private void Dead()
    {
        IsDead = true;
        EnemyAnyDeadEvent?.Invoke();
        Destroy(gameObject);
    }

    public IEnumerator SwitchColorHit()
    {
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SpriteRenderer.color = Color.Lerp(HitColor, StartColor, Time.deltaTime);
        }

        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            SpriteRenderer.color = Color.Lerp(StartColor, HitColor, Time.deltaTime);
        }

    }
}
