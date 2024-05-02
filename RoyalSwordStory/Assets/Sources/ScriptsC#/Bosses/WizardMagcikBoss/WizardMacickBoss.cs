using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BossesStats))]
[RequireComponent(typeof(FireTopShoot))]
public class WizardMacickBoss : MonoBehaviour
{
    private Transform _playerTransform;
    private BossesStats _stats;
    private FireTopShoot _fireTopShoot;
    private float _shootTimer = 0f;
    private float _shootInterval = 2f;
    private float _cooldownTimer = 0f;
    private float _cooldownDuration = 30f; 
    private float _restTimer = 0f;
    private float _restDuration = 5f; 
    private bool _isUsingSuperAbility = false;
    private bool _isRunningAway = false;
    public GameObject magicProjectilePrefab; 
    public Transform projectileSpawnPoint; 

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _stats = GetComponent<BossesStats>();
        _fireTopShoot = GetComponent<FireTopShoot>();
    }

    private void Update()
    {
        if (!_isUsingSuperAbility)
        {
            if (!_isRunningAway)
            {
                if (_shootTimer >= _shootInterval)
                {
                    ShootAtPlayer();
                    _shootTimer = 0f;
                }
                else
                {
                    _shootTimer += Time.deltaTime; 
                }

                float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);
                if (distanceToPlayer < _stats.RunAwayDistance)
                {
                    RunAwayFromPlayer();
                }
            }

            _cooldownTimer += Time.deltaTime;
            if (_cooldownTimer >= _cooldownDuration)
            {
                _cooldownTimer = 0f;
                StartSuperAbility();
            }
        }
        else
        {
            _restTimer += Time.deltaTime;
            if (_restTimer >= _restDuration)
            {
                _restTimer = 0f;
                _isUsingSuperAbility = false;
            }
            else
            {
                RunAwayFromPlayer();
            }
        }
    }
    private void ShootAtPlayer()
    {
        Vector2 direction = (_playerTransform.position - transform.position).normalized;

        GameObject projectile = Instantiate(magicProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * _stats.ProjectileSpeed;
        }
    }

    private void StartSuperAbility()
    {
        _isUsingSuperAbility = true;
        _fireTopShoot.StartShooting(_playerTransform);
    }

    private void RunAwayFromPlayer()
    {
        Vector3 runDirection = (_playerTransform.position.x > transform.position.x) ? Vector3.left : Vector3.right;
        Vector3 newPosition = transform.position + runDirection * _stats.MoveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
