using System.Collections;
using UnityEngine;

public class RestoringPlayerStatsTimer : MonoBehaviour
{
    [SerializeField] private float reloadRecovery = 10;
    [Space]
    [SerializeField] private float healthRecovery;
    [SerializeField] private float manaRecovery;
    [Space]
    [SerializeField] private ParticleSystem effectRecovery;

    private PlayerStats[] playersStats;

    private void Start()
    {
        playersStats = FindObjectsOfType<PlayerStats>();
        PlayEffect(false);
        StartCoroutine(RecoveryTimer());
    }

    private IEnumerator RecoveryTimer()
    {
        yield return new WaitForSeconds(reloadRecovery);

        PlayEffect(true);

        for (int i = 0; i < playersStats.Length; i++)
        {
            playersStats[i].AddHealth(healthRecovery);
        }

        StartCoroutine(RecoveryTimer());
    }

    private void PlayEffect(bool isPlay = true)
    {
        effectRecovery.Stop();

        if (isPlay) effectRecovery.Play();
    }
}
