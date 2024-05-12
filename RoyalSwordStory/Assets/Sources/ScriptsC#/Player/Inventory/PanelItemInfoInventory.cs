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
    [Space]
    [SerializeField] private EquippedItemPlayer _equippedItemPlayer;

    private ICell _cellInventory;

    private void OnEnable()
    {
        CellInventory.CellSelectEvent += ShowInfo;
        CellInventory.CellDeselectEvent += DontShowInfo;

        CellEquippedItem.CellEquippedSelectEvent += ShowInfo;
        CellEquippedItem.CellEquippedDeselectEvent += DontShowInfo;
    }

    private void OnDisable()
    {
        CellInventory.CellSelectEvent -= ShowInfo;
        CellInventory.CellDeselectEvent -= DontShowInfo;

        CellEquippedItem.CellEquippedSelectEvent -= ShowInfo;
        CellEquippedItem.CellEquippedDeselectEvent -= DontShowInfo;
    }

    private void Start()
    {
        useButton.onClick.AddListener(Use);
        DontShowInfo();
    }

    public void Use()
    {
        if (_cellInventory == null) return;
        if (_cellInventory.GetItem() == null) return;

        var item = _cellInventory.GetItem();

        if (item.IsEquip)
        {
            _equippedItemPlayer.Equip(item);
        }
        else if(item.Type == Item.TypeItem.Recovery)
        {
         
        }
        else
        {
            return;
        }

        _cellInventory.ReceiveItem();
    }

    private void ShowInfo(ICell cellInventory, bool ignoreSaveCell)
    {
        var item = cellInventory.GetItem();
        cellItemImage.sprite = item.Sprite;
        itemNameText.name = item.NameItem;
        itemDescriptionText.text = item.Description;

        if(ignoreSaveCell == false)
        _cellInventory = cellInventory;
    }

    private void DontShowInfo()
    {
        cellItemImage.sprite = nullSprite;
        itemNameText.text = "Name item";
        itemDescriptionText.text = "Click on the item to highlight it";
        _cellInventory = null;
    }
}
