using UnityEngine;
using UnityEngine.UI;
using YG;

public class MainMenuStore : MonoBehaviour
{
    [SerializeField] private Text[] coinCountText;

    public static MainMenuStore Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void FixedUpdate()
    {
        RefreshUI();
    }

    public bool TryBuy(int price)
    {
        if (YandexGame.savesData.CoinCount >= price)
        {
            YandexGame.savesData.CoinCount -= price;
            return true;
        }

        return false;
    }

    private void RefreshUI()
    {
        if (DataPlayer.SDKEnabled == false) return;

        var data = DataPlayer.GetData();
        var coin = data.CoinCount;
        string coinText;

        if (data.language == "ru") coinText = "Монеты";
        else if (data.language == "en") coinText = "Coins";
        else if (data.language == "en") coinText = "Paralar";
        else coinText = "$";

        for (int i = 0; i < coinCountText.Length; i++)
        {
            coinCountText[i].text = $"{coinText}: {coin}";
        }
    }
}
