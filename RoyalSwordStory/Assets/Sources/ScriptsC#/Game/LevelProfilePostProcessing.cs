using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class LevelProfilePostProcessing : MonoBehaviour
{
    [SerializeField] private PostProcessProfile _profiler;

    private void OnEnable() => DataPlayer.GetDataEvent += SetPost;

    private void OnDisable() => DataPlayer.GetDataEvent -= SetPost;

    private void Start()
    {
        if (DataPlayer.SDKEnabled)
            SetPost();
    }

    private void SetPost()
    {
        StartCoroutine(SetProfileTimer());
    }

    private IEnumerator SetProfileTimer()
    {
        yield return new WaitForSeconds(0.1f);

        var players = FindObjectsOfType<PostProcessVolume>();


        for (int i = 0; i < players.Length; i++)
        {
            players[i].profile = _profiler;
        }
    }
}

