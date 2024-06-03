using UnityEngine;

public class MainSpawner : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [Space]
    [SerializeField] private Transform spawnPointPlayer;
    [SerializeField] private Transform spawnPointFriend;

    private Animator animatorPlayer;

    private void Start()
    {
        animatorPlayer = playerPrefab.GetComponentInChildren<PlayerAnimationController>().GetAnimator();
    }

    public void SpawnPlayer()
    {
        var tempPlayer = SpawnObject(playerPrefab, spawnPointPlayer);
    }

    private GameObject SpawnObject(GameObject obj, Transform point)
    {
        var tempPoint = point ? point : transform;
        return Instantiate(obj, tempPoint.position, Quaternion.identity);
    }
}
