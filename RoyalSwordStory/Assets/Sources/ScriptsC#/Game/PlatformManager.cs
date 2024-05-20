using System;
using UnityEngine;
using YG;

public class PlatformManager : MonoBehaviour
{
    [SerializeField] private bool testPlatform;
    [SerializeField] private bool isDesktop;

    [SerializeField] private GameObject[] mobileObjects;
    [SerializeField] private GameObject[] desktopObjects;

    public static Action<bool> IsDesctopEvent;
    public static bool IsDesktop;


    private void OnEnable()
    {
        YandexGame.GetDataEvent += LoadPlatform;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= LoadPlatform;
    }

    private void Start()
    {
        if (YandexGame.SDKEnabled) LoadPlatform();
    }

    private void LoadPlatform()
    {
        if (testPlatform == false)
            IsDesktop = YandexGame.EnvironmentData.isDesktop;
        else
            IsDesktop = isDesktop;

        for (int i = 0; i < mobileObjects.Length; i++)
            mobileObjects[i].SetActive(IsDesktop == false);

        for (int i = 0; i < desktopObjects.Length; i++)
            desktopObjects[i].SetActive(IsDesktop);

        IsDesctopEvent?.Invoke(IsDesktop);
    }
}
