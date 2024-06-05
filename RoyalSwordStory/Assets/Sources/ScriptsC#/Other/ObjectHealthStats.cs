using System.Collections;
using UnityEngine;

[RequireComponent(typeof(ExplosionLimbs))]
public class ObjectHealthStats : MonoBehaviour, IUnitHealthStats
{
    [SerializeField] private AudioClip[] damageAudio;
    [SerializeField] private AudioClip[] deadAudio;
    [SerializeField] private AudioFX audioFX;

    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public bool IsDead { get; set; }
    public bool IsStunned { get; set; }

    private ExplosionLimbs _explosionLimbs;

    public void TakeDamage(float damage, float timeStun, float repulsion)
    {
        if (IsDead) return;

        if(audioFX && damageAudio != null)
        audioFX.PlayAudioRandomPitch(damageAudio[GetRandomValue(0, damageAudio.Length)]);

        Health -= damage;

        if (Health <= 0)
            Dead();

    }

    public IEnumerator StunTimer(float time)
    {
        return null;
    }

    private void Dead()
    {
        _explosionLimbs = GetComponent<ExplosionLimbs>();
        IsDead = true;
        DeadAudio();
        _explosionLimbs.PlayFX();
        Destroy(gameObject);
    }

    private void DeadAudio()
    {
        audioFX.transform.SetParent(null);
        audioFX.PlayAudioRandomPitch(deadAudio[GetRandomValue(0, deadAudio.Length)]);
        Destroy(audioFX.gameObject, 3);
    }

    private int GetRandomValue(int min, int max)
     => UnityEngine.Random.Range(min, max);
}
