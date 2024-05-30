using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FactionCell : MonoBehaviour
{
    [SerializeField] private int price;

    private Text _priceText;
    private Image _imageCellFlag;
    private Button _button;

    private void Start()
    {
        InitiliztionUI();

        if (price <= 0)
        {
            price = 0;
            SetActiveText(false);
        }
    }

    public void SetFlagSprite(Sprite sprite)
    {
        InitiliztionUI();
        _imageCellFlag.sprite = sprite;
        RefreshUI();
    }
    public Button GetButton()
    {
        InitiliztionUI();
        return _button;
    }

    public void RefreshUI()
    {
        InitiliztionUI();
        _priceText.text = $"${price}";
    }

    public void SetActiveText(bool active)
    {
        InitiliztionUI();
        _priceText.gameObject.SetActive(active);
    }

    public int GetPrice() => price;

    private void InitiliztionUI()
    {
        if (_button == null) _button = GetComponent<Button>();
        if (_imageCellFlag == null) _imageCellFlag = transform.GetChild(0).GetComponent<Image>();
        if (_priceText == null) _priceText = transform.GetChild(1).GetComponent<Text>();
    }
}
