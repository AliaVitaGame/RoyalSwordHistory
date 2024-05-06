using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class CellInventory : MonoBehaviour
{
    [SerializeField] private Item itemCell;
    [SerializeField] private int countOblectCell;
    [SerializeField] private Sprite nullSprite;

    private Image _image;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void AddItem(Item item, int count)
    {
        itemCell = item;
        countOblectCell += count;
        SetSprite(item.Sprite);
    }


    public void Clear()
    {
        itemCell = null;
        countOblectCell = 0;
        SetSprite(nullSprite);
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
    public void SetSprite(Sprite sprite)
    {
        if(_image == null) _image = GetComponent<Image>();
        _image.sprite = sprite;
    }
}
