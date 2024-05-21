using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoadingManager : MonoBehaviour
{
    public static SceneLoadingManager Instance;

    private void Awake()
    {
       Instance = this;
    }

    public void LoadSceneID(int ID)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(ID);
    }

    public void LoadActiveScene()
    {
        var activeSceneID = SceneManager.GetActiveScene().buildIndex;
        LoadSceneID(activeSceneID);
    }

    public void LoadNextScene()
    {
        var activeSceneID = SceneManager.GetActiveScene().buildIndex;
        LoadSceneID(++activeSceneID);
    }

    public void ExitGame() => Application.Quit();
}
