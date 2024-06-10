using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadZeroScene : MonoBehaviour
{
    [SerializeField] private int sceneID;

    private void OnEnable()
        => DataPlayer.GetDataEvent += LoadScene;

    private void OnDisable()
        => DataPlayer.GetDataEvent -= LoadScene;

    private void Start()
    {
        if (DataPlayer.SDKEnabled)
            LoadScene();
    }

    private void LoadScene()
    {
        if (DataPlayer.GetData().IsNewPlayer == true) return;

        DataPlayer.GetData().IsNewPlayer = true;
        DataPlayer.SaveData();
        SceneManager.LoadScene(sceneID);
    }
}
