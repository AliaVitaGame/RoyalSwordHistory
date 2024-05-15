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
    [SerializeField] private Button takeOffButton;
    [Space]
    [SerializeField] private Sprite nullSprite;
    [Space]
    [SerializeField] private InventoryPlayer inventoryPlayer;
    [SerializeField] private EquippedItemPlayer _equippedItemPlayer;

    private bool _isEquippedItem;
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
        else if (item.Type == Item.TypeItem.Recovery)
        {

        }
        else
        {
            return;
        }
        RefreshUI();
        _cellInventory.ReceiveItem();
    }

    public void TakeOff()
    {
        if (_cellInventory == null) return;
        if (_cellInventory.GetItem() == null) return;


        if (inventoryPlayer.AddItem(_cellInventory.GetItem(), 1))
        {
            _isEquippedItem = false;
            _cellInventory.ReceiveItem();
        }

        RefreshUI();
    }

    private void ShowInfo(ICell cellInventory, bool equipped)
    {
        var item = cellInventory.GetItem();
        cellItemImage.sprite = item.Sprite;
        itemNameText.name = item.NameItem;
        itemDescriptionText.text = item.Description;

        _isEquippedItem = equipped;
        _cellInventory = cellInventory;
        RefreshUI();
    }

    private void DontShowInfo()
    {
        cellItemImage.sprite = nullSprite;
        itemNameText.text = "Name item";
        itemDescriptionText.text = "Click on the item to highlight it";
        _cellInventory = null;
        RefreshUI();
    }

    private void RefreshUI()
    {
        var hasItem = _cellInventory != null;
        useButton.interactable = hasItem;
        sellButton.interactable = hasItem;
        dropButton.interactable = hasItem;

        useButton.gameObject.SetActive(_isEquippedItem == false);
        sellButton.gameObject.SetActive(_isEquippedItem == false);
        dropButton.gameObject.SetActive(_isEquippedItem == false);

        takeOffButton.gameObject.SetActive(_isEquippedItem);
    }
}
