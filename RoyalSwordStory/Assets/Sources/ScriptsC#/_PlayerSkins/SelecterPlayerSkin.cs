using UnityEngine;
using UnityEngine.UI;

public class SelecterPlayerSkin : MonoBehaviour
{
    [SerializeField] private PlayerSkinsCollecting skinsCollecting;
    [Space]
    [SerializeField] private Image imageSkin;
    [SerializeField] private Button nextSkin;
    [SerializeField] private Button backSkin;

    private int _skinID;

    private void OnEnable()
    {
        DataPlayer.GetDataEvent += RefreshUI;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= RefreshUI;
    }

    private void Start()
    {
        nextSkin.onClick.AddListener(NextSkin);
        backSkin.onClick.AddListener(BackSkin);
       
        if(DataPlayer.SDKEnabled)
            RefreshUI(); 
    }

    public void NextSkin()
    {
        AddValueSkinID(1);
    }

    public void BackSkin()
    {
        AddValueSkinID(-1);
    }

    private void AddValueSkinID(int value)
    {
        var data = DataPlayer.GetData();
        _skinID = Mathf.Clamp(value + _skinID, 0, skinsCollecting.Skins.Length);
        data.SelectedPlayerSkinID = _skinID;
        RefreshUI();
    }

    private void RefreshUI()
    {
        int skinID = _skinID;

        imageSkin.sprite = skinsCollecting.Skins[skinID].PreviewSprite;

        backSkin.interactable = skinID != 0;
        nextSkin.interactable = skinID != skinsCollecting.Skins.Length;
    }



}
