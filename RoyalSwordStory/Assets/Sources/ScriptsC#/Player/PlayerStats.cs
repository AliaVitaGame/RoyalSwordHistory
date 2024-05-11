using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMove))]
[RequireComponent(typeof(PlayerAnimationController))]
public class PlayerStats : MonoBehaviour, IUnitHealthStats
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private EquippedItemPlayer equippedItemPlayer;

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

    public static Action PlayerHitEvent;
    public static Action PlayerDaadEvent;

    private PlayerMove _playerMove;
    private HealthBar _healthBar;
    private PlayerAnimationController _animationController;

    private void Start()
    {
        _playerMove = GetComponent<PlayerMove>();
        _animationController = GetComponent<PlayerAnimationController>();
        _healthBar = GetComponentInChildren<HealthBar>();
        _healthBar.Unpin();
    }

    public void TakeDamage(float damage, float timeStun, float repulsion)
    {
        if (IsDead) return;

        _animationController.HitAnimation(true);

        var damageTakenArmor = damage * (1 - (equippedItemPlayer.GetDamageRepaymentPercentage() / 100));
        Health -= damageTakenArmor;

        PlayerHitEvent?.Invoke();

        _healthBar.SetHealth(Health, MaxHealth);

        _playerMove.SetStopMove(true);
        _playerMove.SetVelosity(repulsion, 0);
        if (Health <= 0)
            Dead();

        if (IsStunned == false)
            StartCoroutine(StunTimer(timeStun));
    }

    public IEnumerator StunTimer(float time)
    {
        IsStunned = true;
        yield return new WaitForSeconds(time);
        IsStunned = false;

        _playerMove.SetStopMove(false);
        _animationController.HitAnimation(false);
    }

    private void Dead()
    {
        IsDead = true;
        PlayerDaadEvent?.Invoke();
        Debug.Log("Player dead");
        Destroy(gameObject);
    }


}
