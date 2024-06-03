using UnityEngine;

[RequireComponent(typeof(DataPlayer))]
[RequireComponent(typeof(MainSpawner))]
public class MainManager : MonoBehaviour
{
    private MainSpawner _spawner;

    private void OnEnable()
    {
        DataPlayer.GetDataEvent += StartGame;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= StartGame;
    }

    private void Start()
    {
        _spawner = GetComponent<MainSpawner>();

        if (DataPlayer.SDKEnabled)
            StartGame();
    }

    private void StartGame()
    {
        _spawner.SpawnPlayer();
    }



}
