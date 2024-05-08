using UnityEngine;
using UnityEngine.UI;

public class PanelItemInfoInventory : MonoBehaviour
{

    [SerializeField] private Image cellItemImage;
    [SerializeField] private Text itemNameText;
    [SerializeField] private Text itemDescriptionText;
    [Space]
    [SerializeField] private Button useButton;
    [SerializeField] private Button sellButton;
    [SerializeField] private Button dropButton;
    [Space]
    [SerializeField] private Sprite nullSprite;

    private void OnEnable()
    {
        CellInventory.CellSelectEvent += ShowInfo;
        CellInventory.CellDeselectEvent += DontShowInfo;
    }

    private void OnDisable()
    {
        CellInventory.CellSelectEvent -= ShowInfo;
        CellInventory.CellDeselectEvent -= DontShowInfo;
    }

    private void Start()
    {
        DontShowInfo();
    }

    private void ShowInfo(CellInventory cellInventory)
    {
        var item = cellInventory.GetItem();
        cellItemImage.sprite = item.Sprite;
        itemNameText.name = item.NameItem;
        itemDescriptionText.text = item.Description;
    }

    private void DontShowInfo()
    {
        cellItemImage.sprite = nullSprite;
        itemNameText.text = "Name item";
        itemDescriptionText.text = "Click on the item to highlight it";
    }
}
