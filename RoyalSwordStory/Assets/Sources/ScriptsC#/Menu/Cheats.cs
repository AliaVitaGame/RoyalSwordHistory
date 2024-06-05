using UnityEngine;
using UnityEngine.UI;

public class Cheats : MonoBehaviour
{
    [SerializeField] private CheatCode[] cheatCodes;
    [Space]
    [SerializeField] private InputField inputCheat;
    [SerializeField] private Button activeCheat;
    [Space]
    [SerializeField] private AudioFX audioFX;
    [SerializeField] private AudioClip cheatActivatedAudio;

    private void OnEnable()
    {
        DataPlayer.GetDataEvent += ActivatedButton;
    }

    private void OnDisable()
    {
        DataPlayer.GetDataEvent -= ActivatedButton;
    }

    void Start()
    {
        activeCheat.onClick.AddListener(ActiveCheat);
        activeCheat.interactable = false;

        if (DataPlayer.SDKEnabled)
            ActivatedButton();
    }

    public void ActiveCheat()
    {
        if (inputCheat.text == null) return;
        if (inputCheat.text == "") return;

        for (int i = 0; i < cheatCodes.Length; i++)
        {
            if (cheatCodes[i].Code == inputCheat.text)
            {
                Debug.Log("ACTIVE CHEAT");
                audioFX.PlayAudioRandomPitch(cheatActivatedAudio);
                DataPlayer.AddCoin(cheatCodes[i].AddingCoin);
                return;
            }
        }
    }

    private void ActivatedButton() 
        => activeCheat.interactable = true;
}

[System.Serializable]
public class CheatCode
{
    public string Code;
    public int AddingCoin;
}
