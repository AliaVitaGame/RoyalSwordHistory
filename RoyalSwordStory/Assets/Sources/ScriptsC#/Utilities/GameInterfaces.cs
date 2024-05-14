using System.Collections;
using UnityEngine;

public interface IUnitHealthStats
{
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsStunned { get; set; }
    public void TakeDamage(float damage, float timeStun, float repulsion);
    public IEnumerator StunTimer(float time);
}

public interface IUnitAttacking
{
    public float Damage { get; set; }
    public float AttackTime { get; set; }
    public float Repulsion { get; set; }
    public float StunTime { get; set; }
    public float RadiusDamage { get; set; }
    public bool IsAttacking { get; set; }
    public LayerMask LayerTarget { get; set; }
    public Vector2 DistanceDamage { get; set; }

    public void StartAttack(bool attackDown = false);
    public IEnumerator Attack();
    public IEnumerator AttackTimer(float time);
}

public interface IItem
{
    public int CountItem { get; set; }
    public Item GetItem();
    public void Destroy();
}

public interface ISwitchColorHit
{
    public Color StartColor { get; set; }
    public Color HitColor { get; set; }
    public SpriteRenderer SpriteRenderer { get; set; }
    public void SetColorSprite(Color color);
}

public interface ICell
{
    public Item GetItem();
    public Item ReceiveItem();
}