using UnityEngine;
using UnityEngine.UI;

public class FactionController : MonoBehaviour
{
    [SerializeField] private Image[] previewFlag;
    [SerializeField] private FactionCell[] factionCells;
    [SerializeField] private FactionCollecting factionCollecting;
    [Space]
    [SerializeField] private AudioFX audioFX;
    [SerializeField] private AudioClip selectAudio;
    [SerializeField] private AudioClip noMoneyAudio;

    private void OnEnable()
    {
        DataPlayer.GetDataEvent += RefreshUI;
        DataPlayer.GetDataEvent += InitilizationUI;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= RefreshUI;
        DataPlayer.GetDataEvent -= InitilizationUI;
    }

    private void Start()
    {
        if (DataPlayer.SDKEnabled)
        {
            RefreshUI();
            InitilizationUI();
        }

    }

    public void SelectFlag(int ID)
    {
        var data = DataPlayer.GetData();

        if (data.IsBuyFlag[ID])
        {
            data.SelectedFlagID = ID;
            DataPlayer.SaveData();
            audioFX.PlayAudioRandomPitch(selectAudio);
        }
        else if (MainMenuStore.Instance.TryBuy(factionCells[ID].GetPrice()))
        {
            data.SelectedFlagID = ID;
            DataPlayer.SaveData();
            audioFX.PlayAudioRandomPitch(selectAudio);
        }
        else
        {
            audioFX.PlayAudioRandomPitch(noMoneyAudio);
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        for (int i = 0; i < previewFlag.Length; i++)
            previewFlag[i].sprite = factionCollecting.Factions[DataPlayer.GetData().SelectedFlagID].FlagSprite;
    }

    private void InitilizationUI()
    {
        if (factionCells == null) return;
        if (factionCells.Length <= 0) return;

        var data = DataPlayer.GetData();

        for (int i = 0; i < factionCells.Length; i++)
        {
            int buttonID = i;
            factionCells[i].GetButton().onClick.AddListener(() => SelectFlag(buttonID));

            factionCells[i].SetFlagSprite(factionCollecting.Factions[i].FlagSprite);

            if (data.IsBuyFlag[i])
                factionCells[i].SetActiveText(false);
        }
    }
}
