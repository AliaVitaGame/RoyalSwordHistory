using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject[] enemys;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            Instantiate(enemys[Random.Range(0, enemys.Length)], Vector2.zero, Quaternion.identity);
        }
    }
}
