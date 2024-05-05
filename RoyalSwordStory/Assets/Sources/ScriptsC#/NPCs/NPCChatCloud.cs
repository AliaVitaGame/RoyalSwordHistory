using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCChatCloud : MonoBehaviour
{
    [SerializeField] private Text _cloudText;
    [SerializeField] private Image _cloudImage;
    [SerializeField] private string[] _prases = new string[] { };


    private bool _canSpeak = true;

    private void Start()
    {
        NPCController.OnPlayerIsClose += NPCController_OnPlayerIsClose;
    }
    private void OnDisable()
    {
        NPCController.OnPlayerIsClose -= NPCController_OnPlayerIsClose;
    }
    private void NPCController_OnPlayerIsClose()
    {
        if (_canSpeak)
            StartCoroutine(WriteRandomPhraseCO());
    }

    private void SpeakRandomText()
    {
        if (_canSpeak)
        {
            _cloudText.text = _prases[Random.Range(0, _prases.Length)];
        }

    }
    private IEnumerator WriteRandomPhraseCO()
    {
        SpeakRandomText();
        _canSpeak = false;

        yield return new WaitForSeconds(5);

        _canSpeak = true;
    }
}
