using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArmySpawn : MagicManager
{
    [SerializeField] private List<GameObject> _soldiers;
    [SerializeField] private int _soldiersCount;
    public override void Activate()
    {
        Randomizer();
    }
    private void Randomizer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        for (int i = 0; i < _soldiers.Count; i++)
        {
            Vector3 randomPos = new Vector3(Random.Range(5, 9), 0, 0); 
            Vector3 spawnPosition = player.transform.position + randomPos;
            Instantiate(_soldiers[i], spawnPosition, Quaternion.identity);
        }
    }
}
