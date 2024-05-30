using System.Collections;
using UnityEngine;

public class SupportUnitStats : MonoBehaviour, IUnitHealthStats, ISwitchColorHit
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

    private HealthBar _healthBar;
    private FriendAnimationController _animationController;

    private void Start()
    {
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.Unpin();
        _animationController = GetComponent<FriendAnimationController>();
        SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        StartColor = SpriteRenderer.color;
    }

    public void TakeDamage(float damage, float timeStun, float repulsion)
    {
        if (IsDead) return;

        _animationController.HitAnimation(true);

        audioFX.PlayAudioRandomPitch(damageAudio[GetRandomValue(0, damageAudio.Length)]);
        audioFX.PlayAudioRandomPitch(bloodAudio[GetRandomValue(0, bloodAudio.Length)]);

        Health -= damage;

        bloodFX.Play();

        _healthBar.SetHealth(Health, MaxHealth);


        if (Health <= 0)
            Dead();

        StartCoroutine(StunTimer(timeStun));
    }

    public IEnumerator StunTimer(float time)
    {
        IsStunned = true;

        SetColorSprite(HitColor);

        yield return new WaitForSeconds(time);

        SetColorSprite(StartColor);

        IsStunned = false;

        if (_animationController)
            _animationController.HitAnimation(false);
    }

    private void Dead()
    {
        IsDead = true;
        DeadAudio();
        Destroy(gameObject);
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

    private int GetRandomValue(int min, int max)
        => UnityEngine.Random.Range(min, max);
}

