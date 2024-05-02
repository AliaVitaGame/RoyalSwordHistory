using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTopShoot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public float fallSpeed = 10f; 

    private Transform _playerTransform;
    private bool _isShooting = false;
    private bool _isResting = false;

    public bool IsShooting { get { return _isShooting; } }
    public bool IsResting { get { return _isResting; } }

    public void StartShooting(Transform playerTransform)
    {
        if (!_isShooting && !_isResting)
        {
            _playerTransform = playerTransform;
            _isShooting = true;
            // Создаем пулю и направляем ее к игроку
            GameObject bullet = Instantiate(bulletPrefab, GetSpawnPosition(), Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            if (bulletRb != null)
            {
                bulletRb.velocity = Vector2.down * fallSpeed;
            }


            Invoke(nameof(StartResting), 30f);
        }
    }

    public void StopShooting()
    {
        CancelInvoke(nameof(StartResting));
        _isShooting = false;
        _isResting = true;
        Invoke(nameof(StopResting), 3f);
    }

    private Vector3 GetSpawnPosition()
    {
        if (_playerTransform != null)
        {
            Vector3 playerPosition = _playerTransform.position;
            return new Vector3(playerPosition.x, playerPosition.y + 1f, playerPosition.z);
        }
        else
        {
            return transform.position;
        }
    }

    private void StartResting()
    {
        _isShooting = false;
        _isResting = true;
        Invoke(nameof(StopResting), 3f);
    }

    private void StopResting()
    {
        _isResting = false;
    }
}
