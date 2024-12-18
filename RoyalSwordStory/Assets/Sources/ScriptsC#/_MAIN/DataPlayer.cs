using System;
using UnityEngine;
using YG;
using static YG.YandexGame;

public class DataPlayer : MonoBehaviour
{
    public static bool SDKEnabled;
    public static Action GetDataEvent;
    public static Action SaveDataEvent;

    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadDataEvent;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadDataEvent;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled)
            LoadSDKEnabled();
    }


    public static void AddCoin(int value)
    {
        YandexGame.savesData.CoinCount += value;
        SaveData();
    }

    public static void SaveData()
    {
        YandexGame.SaveProgress();
        SaveDataEvent?.Invoke();
    }

    public static void ResetData()
    {
        YandexGame.ResetSaveProgress();
        SaveData();

    }


    public static string GetLanguage() => YandexGame.EnvironmentData.language;
    public static JsonEnvironmentData GetEnvironmentData() => YandexGame.EnvironmentData;
    public static SavesYG GetData() => YandexGame.savesData;


    private void LoadDataEvent()
    {
        GetDataEvent?.Invoke();
        LoadSDKEnabled();
    }
    private void LoadSDKEnabled() => SDKEnabled = YandexGame.SDKEnabled;
}
