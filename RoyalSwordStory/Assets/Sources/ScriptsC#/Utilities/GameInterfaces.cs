using System.Collections;
using UnityEngine;

public interface IUnitHealthStats
{
    public float Health {  get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsStunned { get; set; }
    public void TakeDamage(float damage, float timeStun);
    public IEnumerator StunTimer(float time);
}

public interface IUnitAttacking
{
    public float Damage { get; set; }
    public float AttackTime { get; set; }
    public float Repulsion { get; set; }
    public float StunTime { get; set; }
    public float RadiusDamage { get; set; }
    public bool IsAttacking {  get; set; }
    public LayerMask LayerTarget {  get; set; }
    public Vector2 DistanceDamage {  get; set; }

    public void StartAttack();
    public IEnumerator Attack();
    public IEnumerator AttackTimer(float time);
}