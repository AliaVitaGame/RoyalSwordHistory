using UnityEngine;
using UnityEngine.UI;

public class FactionController : MonoBehaviour
{
    [SerializeField] private Image[] previewFlag;
    [SerializeField] private Button[] buttonsSelectFlag;
    [SerializeField] private FactionCollecting factionCollecting;
    [SerializeField] private int selectedFlagID;

    private Image[] imagesButton;

    private void Start()
    {
        InitilizationUI();
        RefreshUI();
    }

    public void SelectFlag(int ID)
    {
        selectedFlagID = ID;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (previewFlag == null) return;
        if (previewFlag.Length <= 0) return;
        if (buttonsSelectFlag == null) return;
        if (buttonsSelectFlag.Length <= 0) return;

        for (int i = 0; i < previewFlag.Length; i++)
            previewFlag[i].sprite = factionCollecting.Factions[selectedFlagID].FlagSprite;
    }

    private void InitilizationUI()
    {
        if (buttonsSelectFlag == null) return;
        if (buttonsSelectFlag.Length <= 0) return;

        imagesButton = new Image[buttonsSelectFlag.Length];

        for (int i = 0; i < buttonsSelectFlag.Length; i++)
        {
            int buttonID = i;
            buttonsSelectFlag[i].onClick.AddListener(() => SelectFlag(buttonID));

            imagesButton[i] = buttonsSelectFlag[i].transform.GetChild(0).GetComponent<Image>();
            imagesButton[i].sprite = factionCollecting.Factions[i].FlagSprite;
        }
    }
}
