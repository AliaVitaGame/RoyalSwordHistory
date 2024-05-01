using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BossesStats))]
[RequireComponent(typeof(MeteorRain))]
public class WizardMacickBoss : MonoBehaviour
{
    private Transform _playerTransform;
    private BossesStats _stats;
    private MeteorRain _meteorRain;
    private float _meteorRainCooldown = 30f;
    private float _meteorRainDuration = 5f;
    private float _meteorRainTimer = 0f;
    private bool _isCastingMeteorRain = false;
    private bool _isResting = false;
    private float _restTime = 5f;

    private float _shootTimer = 0f;
    private float _shootInterval = 2f; // �������� ����� ����������

    public GameObject magicProjectilePrefab; // ������ ����������� �������
    public Transform projectileSpawnPoint; // �����, ������ ����� ���������� �������

    private void Start()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        _stats = GetComponent<BossesStats>();
        _meteorRain = GetComponent<MeteorRain>();
    }

    private void Update()
    {
        if (!_isCastingMeteorRain && !_isResting)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, _playerTransform.position);

            if (distanceToPlayer > _stats.ShootDistance)
            {
                // ���� ������ ���������� ������� � ���������� ��������, ��������
                if (_shootTimer >= _shootInterval)
                {
                    ShootAtPlayer();
                    _shootTimer = 0f; // ���������� ������
                }
                else
                {
                    _shootTimer += Time.deltaTime; // ��������� ������
                }
            }
            else
            {
                RunAwayFromPlayer();
            }
        }

        if (!_isCastingMeteorRain && !_isResting)
        {
            _meteorRainTimer += Time.deltaTime;
            if (_meteorRainTimer >= _meteorRainCooldown)
            {
                _meteorRainTimer = 0f;
                StartCoroutine(CastMeteorRain());
            }
        }
    }

    private void ShootAtPlayer()
    {
        // ������� ���������� ������ � ���������� ��� � ������
        GameObject projectile = Instantiate(magicProjectilePrefab, projectileSpawnPoint.position, Quaternion.identity);
        MagicProjectile magicProjectile = projectile.GetComponent<MagicProjectile>();
        if (magicProjectile != null)
        {
            magicProjectile.Seek(_playerTransform);
        }
    }

    private void RunAwayFromPlayer()
    {
        Vector3 runDirection = (transform.position - _playerTransform.position).normalized;
        Vector3 newPosition = transform.position + runDirection * _stats.MoveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }

    private System.Collections.IEnumerator CastMeteorRain()
    {
        _isCastingMeteorRain = true;
        _meteorRain.StartMeteorRain();
        yield return new WaitForSeconds(_meteorRainDuration);
        _isCastingMeteorRain = false;
        StartCoroutine(RestForSeconds(_restTime));
    }

    private System.Collections.IEnumerator RestForSeconds(float seconds)
    {
        _isResting = true;
        yield return new WaitForSeconds(seconds);
        _isResting = false;
    }
}
