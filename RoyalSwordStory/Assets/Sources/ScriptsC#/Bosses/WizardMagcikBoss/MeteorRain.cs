using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorRain : MonoBehaviour
{
    public GameObject meteorPrefab; 
    public float spawnInterval = 0.5f; 
    public float spawnRadius = 10f; 

    private bool _isRaining = false;

    public void StartMeteorRain()
    {
        if (!_isRaining)
        {
            _isRaining = true;
            StartCoroutine(RainMeteors());
        }
    }

    private System.Collections.IEnumerator RainMeteors()
    {
        while (_isRaining)
        {
          
            Vector2 randomPosition = Random.insideUnitCircle.normalized * spawnRadius;
            Vector3 spawnPosition = new Vector3(randomPosition.x, transform.position.y, randomPosition.y);

        
            GameObject meteor = Instantiate(meteorPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);
    }
}
