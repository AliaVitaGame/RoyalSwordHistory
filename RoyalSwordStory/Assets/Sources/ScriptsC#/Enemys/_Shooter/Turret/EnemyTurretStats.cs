using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ExplosionLimbs))]
[RequireComponent(typeof(EnemyAnimationController))]
public class EnemyTurretStats : MonoBehaviour, IUnitHealthStats, ISwitchColorHit
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private ParticleSystem bloodFX;
    [SerializeField] private Color hitColor = Color.red;
    [Space]
    [SerializeField] private AudioClip[] damageAudio;
    [SerializeField] private AudioClip[] bloodAudio;
    [SerializeField] private AudioClip[] deadAudio;
    [SerializeField] private AudioFX audioFX;
    [Space]
    [SerializeField] private bool deadCollisionGround;
    [SerializeField] private LayerMask layerGround;

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
    private ExplosionLimbs _explosionLimbs;
    private EnemyAnimationController _animationController;

    private void Start()
    {
        RefreshUI();
        _healthBar.Unpin();
        _animationController = GetComponent<EnemyAnimationController>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartColor = SpriteRenderer.color;
        _explosionLimbs = GetComponentInChildren<ExplosionLimbs>();
    }

    public void TakeDamage(float damage, float timeStun, float repulsion)
    {
        if (IsDead) return;

        EnemyStanEvent?.Invoke(true);

        StopAllCoroutines();
        StartCoroutine(StunTimer(timeStun));

        _animationController.HitAnimation(true);

        DamageAudio();

        Health -= damage;

        bloodFX.Play();

        EnemyAnyHitEvent?.Invoke();

        RefreshUI();

        if (Health <= 0)
            Dead();

    }

    public IEnumerator StunTimer(float time)
    {
        IsStunned = true;

        SetColorSprite(HitColor);

        yield return new WaitForSeconds(time);

        SetColorSprite(StartColor);

        IsStunned = false;

        EnemyStanEvent?.Invoke(false);

        if (_animationController)
            _animationController.HitAnimation(false);

    }

    public void AddMaxHealth(float value)
    {
        MaxHealth += value;
        RefreshUI();
    }

    public void RestoreHealth()
    {
        Health = MaxHealth;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (_healthBar == null) _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.SetHealth(Health, MaxHealth);
    }

    private void Dead()
    {
        IsDead = true;
        DeadAudio();
        _explosionLimbs.PlayFX();
        EnemyAnyDeadEvent?.Invoke();
        Destroy(gameObject);
    }

    private void DamageAudio()
    {
        audioFX.PlayAudioRandomPitch(damageAudio[GetRandomValue(0, damageAudio.Length)]);
        audioFX.PlayAudioRandomPitch(bloodAudio[GetRandomValue(0, bloodAudio.Length)]);
    }

    private void DeadAudio()
    {
        audioFX.transform.SetParent(null);
        audioFX.PlayAudioRandomPitch(deadAudio[GetRandomValue(0, deadAudio.Length)]);
        Destroy(audioFX.gameObject, 3);
    }

    public void SetColorSprite(Color color)
    {
        SpriteRenderer.color = color;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (deadCollisionGround == false) return;

        if (layerGround == (1 << collision.gameObject.layer))
        {
            DamageAudio();
            Dead();
        }
    }


    private int GetRandomValue(int min, int max)
        => UnityEngine.Random.Range(min, max);
}