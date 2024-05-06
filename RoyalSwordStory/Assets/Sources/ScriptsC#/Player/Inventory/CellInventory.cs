using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CellInventory : MonoBehaviour
{
    [SerializeField] private Item itemCell;
    [SerializeField] private int countOblectCell;
    [SerializeField] private Sprite nullSprite;

    private Image _image;
    private Text _textCount;

    private void Start()
    {
        InitializationUI();
    }

    public void AddItem(Item item, int count)
    {
        itemCell = item;
        countOblectCell += count;
        RefreshUI(item.Sprite);
    }


    public void Clear()
    {
        itemCell = null;
        countOblectCell = 0;
        RefreshUI(nullSprite);
    }

    public Item GetItem()
    {
        countOblectCell--;

        var item = itemCell;

        if (countOblectCell <= 0) 
            Clear();

        return item;
    }

    public bool HasItem() => countOblectCell > 0;
    public Item.TypeItem GetItemType() => itemCell.Type;
    public int GetCountOblectCell() => countOblectCell;
    public void SetCountOblectCell(int count) => countOblectCell = count;

    private void RefreshUI(Sprite sprite)
    {
        InitializationUI();
        _image.sprite = sprite;

        string text = HasItem() ? $"{countOblectCell}" : null;
        _textCount.text = text;
    }
    private void InitializationUI()
    {
        if (_image == null) _image = transform.GetChild(0).GetComponent<Image>();
        if (_textCount == null) _textCount = transform.GetChild(1).GetComponent<Text>();
    }
}
