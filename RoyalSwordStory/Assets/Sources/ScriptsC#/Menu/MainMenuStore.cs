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
        for (int i = 0; i < coinCountText.Length; i++)
        {
            coinCountText[i].text = $"Coin: {coin}";
        }
    }
}
