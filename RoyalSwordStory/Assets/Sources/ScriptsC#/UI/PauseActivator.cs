using UnityEngine;

public class PauseActivator : MonoBehaviour
{
    [SerializeField] private GameObject panelPause;
    public static bool IsPause { get; private set; }

    private const int _timeScalePause = 0;
    private const int _timeScaleUnPause = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            PauseActive(!IsPause);
    }

    public void PauseActive(bool active)
    {
        IsPause = active;
        panelPause.SetActive(active);
        Time.timeScale = active ? _timeScalePause : _timeScaleUnPause;
    }
}
