using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCChatCloud : MonoBehaviour
{
    [SerializeField] private string[] prases = new string[] { };
    [SerializeField] private Text _cloudText;

    private bool canSpeak = true;

    private void Awake()
    {
        NPCController.OnPlayerIsClose += NPCController_OnPlayerIsClose;
    }

    private void OnDisable()
    {
        NPCController.OnPlayerIsClose -= NPCController_OnPlayerIsClose;
    }
    private void NPCController_OnPlayerIsClose()
    {
        SaySomething();
    }

    public void SaySomething()
    {
        if(canSpeak)
        {
            _cloudText.text = prases[Random.Range(0, prases.Length)];
            StartCoroutine(SpeechIntervalCO());
        }
    }

    

    private IEnumerator SpeechIntervalCO()
    {
        canSpeak = false;

        yield return new WaitForSeconds(3);
    
        canSpeak = true;
    }
}
