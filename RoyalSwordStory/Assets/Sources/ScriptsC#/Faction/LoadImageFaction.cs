using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class LoadImageFaction : MonoBehaviour
{
    [SerializeField] private bool autoLoad;
    [SerializeField] private FactionCollecting factionCollecting;

    public Sprite Flag {  get; private set; }

    private void OnEnable()
    {
        DataPlayer.GetDataEvent += LoadImage;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= LoadImage;
    }

    private void Start()
    {
        if (DataPlayer.SDKEnabled)
            LoadImage();
    }

    private void LoadImage()
    {
        if (autoLoad == false) return;

        var data = DataPlayer.GetData();
        Flag = factionCollecting.Factions[data.SelectedFlagID].FlagSprite;
        GetComponent<Image>().sprite = Flag;
    }
}
