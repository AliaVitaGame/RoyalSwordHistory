using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmySpawn : MagicManager
{
    [SerializeField] private List<GameObject> _soldiers;
    [SerializeField] private ParticleSystem _spawnEffect;
    [Space(20)]
    [SerializeField] private AudioClip equipAudio;
    public override void Activate()
    {
        Randomizer();
    }
    public override void PlayEquipAudio(AudioFX audioFX)
    {
        if (audioFX != null && equipAudio != null)
        {
            audioFX.PlayAudio(equipAudio);
        }
    }
    private void Randomizer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < _soldiers.Count; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(5, 9), 0, 0);
            Vector3 spawnPosition = player.transform.position + randomPos;
            _spawnEffect.transform.position = spawnPosition;
            Instantiate(_soldiers[i], spawnPosition, Quaternion.identity);

            var minus = new Vector3(spawnPosition.x, -1.8f, spawnPosition.z);
            ParticleSystem spawnEffectInstance = Instantiate(_spawnEffect, spawnPosition + minus, Quaternion.Euler(-90f, 0, 0));
            spawnEffectInstance.Play();
            //перепишу код как только появится союзник игрока чтобы как дочерний объект добавить партиклы
            //таким образом я буду активировать с ног союзника так как при Instantiate спавнится с тела
        }
    }
}
