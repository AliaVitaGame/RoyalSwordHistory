using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnerEnemy : MonoBehaviour
{
    [SerializeField] private EnemyStats[] easyEnemys;
    [SerializeField] private EnemyStats[] middleEnemys;
    [SerializeField] private EnemyStats[] hardEnemys;
    [Space]
    [SerializeField] private float addingHealth;
    [SerializeField] private float addingDamage;
    [SerializeField] private float increaseAddingHealth = 5;
    [SerializeField] private float increaseAddingDamage = 5;
    [Space]
    [SerializeField] private int spawnCountEnemy = 5;
    [SerializeField] private int increaseSpawnCountEnemy = 3;
    [SerializeField] private int maxSpawnCountEnemy = 50;
    [Space]
    [SerializeField] private Text countdownToLaunchText;
    [SerializeField] private Transform[] spawnPoints;

    private int _enemyAlive;
    private float _survivalTime;

    private void OnEnable()
    {
        EnemyStats.EnemyAnyDeadEvent += EnemyDead;
    }

    private void OnDisable()
    {
        EnemyStats.EnemyAnyDeadEvent -= EnemyDead;
    }

    private void Start()
    {
        StartCoroutine(AutoStart());
    }

    private void FixedUpdate()
    {
        _survivalTime += Time.fixedDeltaTime;
    }

    private IEnumerator AutoStart()
    {
        var textObj = countdownToLaunchText.gameObject;

        textObj.SetActive(false);

        for (int i = 3; i > 0; i--)
        {
            countdownToLaunchText.text = i.ToString();
            textObj.SetActive(false);
            textObj.SetActive(true);
            yield return new WaitForSeconds(1);
        }

        StartSpawn();

        yield return new WaitForSeconds(1);
        textObj.SetActive(false);

    }

    public void StartSpawn()
    {
        _enemyAlive = spawnCountEnemy;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        for (int i = 0; i < spawnCountEnemy; i++)
        {
            yield return new WaitForSeconds(Random.Range(0f, 0.5f));
            var tempEnemy = Instantiate(GetEnemy(), spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.identity);
            tempEnemy.AddMaxHealth(addingHealth);
            tempEnemy.RestoreHealth();

            if (tempEnemy.transform.TryGetComponent(out EnemyMeleeAttacking unitAttacking))
            {
                unitAttacking.SetAggressionRadius(100);
                unitAttacking.Damage += addingDamage;
            }
        }

        addingHealth += increaseAddingHealth;
        addingDamage += increaseAddingDamage;
    }

    private void EnemyDead()
    {
        _enemyAlive--;

        if (_enemyAlive <= 0)
        {
            spawnCountEnemy = Mathf.Clamp(spawnCountEnemy + increaseSpawnCountEnemy, 1, maxSpawnCountEnemy);
            StartSpawn();
        }
    }

    private EnemyStats GetEnemy()
    {
        int randomID = Random.Range(0, easyEnemys.Length);
        return easyEnemys[randomID];
    }


}
