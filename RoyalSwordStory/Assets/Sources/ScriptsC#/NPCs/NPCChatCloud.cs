using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NPCChatCloud : MonoBehaviour
{
    [SerializeField] private string[] prases = new string[] { };
    [SerializeField] private Text _cloudText;

    private bool canSpeak;
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

        yield return new WaitForSeconds(5);

        canSpeak = true;
    }
}
