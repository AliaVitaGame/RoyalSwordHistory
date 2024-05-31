using UnityEngine;
using UnityEngine.UI;

public class NamePlayer : MonoBehaviour
{
    [SerializeField] private InputField inputField;
    [SerializeField] private Text[] namePlayerText;

    private void OnEnable()
    {
        DataPlayer.GetDataEvent += LoadName;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= LoadName;
    }

    private void Start()
    {
        if (DataPlayer.SDKEnabled)
            LoadName();
    }

    public void SetName(string name)
    {
        if (name.ToLower() == "nkdron") name = "???";

        var data = DataPlayer.GetData();
        data.NamePlayer = name;

        SaveName();
    }

    public void SaveName()
    {
        DataPlayer.SaveData();
    }

    private void LoadName()
    {
        if (namePlayerText == null) return;
        if (namePlayerText.Length <= 0) return;

        var data = DataPlayer.GetData();

        if (data.NamePlayer == null || data.NamePlayer == "")
        {
            string newName;
            int randomID = GetRandomValue();

            if (data.language == "en") newName = "Player";
            else if (data.language == "ru") newName = "Игрок";
            else if (data.language == "tr") newName = "oyuncu";
            else newName = "???";

            data.NamePlayer = $"{newName}{randomID}";

            SaveName();
        }

        RefreshUI();
    }

    private void RefreshUI()
    {
        var data = DataPlayer.GetData();

        for (int i = 0; i < namePlayerText.Length; i++)
        {
            if (namePlayerText[i])
                namePlayerText[i].text = data.NamePlayer;
        }

        if (inputField)
            inputField.text = data.NamePlayer;
    }

    private int GetRandomValue() => Random.Range(0, 1001);
}
