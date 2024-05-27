using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using YG;

public class RandomPhrasesMenuUI : MonoBehaviour
{
    [SerializeField] private Text textPhrases;
    [Space]
    [SerializeField] private string[] ruPhrases;
    [SerializeField] private string[] enPhrases;
    [SerializeField] private string[] trPhrases;

    private float _delayWrite = 0.05f;
    private float _timeShow = 7f;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += ShowRandomPhrases;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= ShowRandomPhrases;
    }

    private void Start()
    {
        if (textPhrases == null)
        {
            if (gameObject.TryGetComponent(out Text text))
                textPhrases = text;
        }

        if (YandexGame.SDKEnabled)
            ShowRandomPhrases();
    }

    private void ShowRandomPhrases()
    {
        StartCoroutine(ShowTime());
    }

    private IEnumerator ShowTime()
    {
        SetText("");

        yield return new WaitForSeconds(0.5f);

        string[] texts = null;

        if (YandexGame.EnvironmentData.language == "ru") texts = ruPhrases;
        else if (YandexGame.EnvironmentData.language == "en") texts = enPhrases;
        else if (YandexGame.EnvironmentData.language == "tr") texts = trPhrases;

        string phrases = texts[Random.Range(0, texts.Length)];

        for (int i = 0; i < phrases.Length; i++)
        {
            AddText(phrases[i].ToString());
            yield return new WaitForSeconds(_delayWrite);
        }

        yield return new WaitForSeconds(_timeShow);

        StartCoroutine(ShowTime());
    }

    private void SetText(string text) => textPhrases.text = text;
    private void AddText(string text) => textPhrases.text += text;

}
