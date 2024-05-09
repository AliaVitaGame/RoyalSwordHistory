using UnityEngine;
using UnityEngine.UI;

public class EquippedItemPlayer : MonoBehaviour
{
    [SerializeField] private Image itemHelmetImage;
    [SerializeField] private Image itemArmorImage;
    [SerializeField] private Image itemGlovesImage;
    [SerializeField] private Image itemGreavesImage;
    [SerializeField] private Image itemBootsImage;
    [SerializeField] private Image itemRingImage;
    [Space]
    [SerializeField] private Sprite nullSprite;

    private Item _itemHelmet;
    private Item _itemArmor;
    private Item _itemGloves;
    private Item _itemGreaves;
    private Item _itemBoots;
    private Item _itemRing;


    private void Start()
    {
        RefreshUI();
    }

    public void Equip(Item item)
    {
        if (item.Type == Item.TypeItem.Helmet) _itemHelmet = item;
        else if (item.Type == Item.TypeItem.Armor) _itemArmor = item;
        else if (item.Type == Item.TypeItem.Gloves) _itemGloves = item;
        else if (item.Type == Item.TypeItem.Greaves) _itemGreaves = item;
        else if (item.Type == Item.TypeItem.Boots) _itemBoots = item;
        else if (item.Type == Item.TypeItem.Ring) _itemRing = item;

        RefreshUI();
    }

    public float HelmetStats()
    {
        return _itemHelmet ? _itemHelmet.AddProtection : 0;
    }

    public float ArmorStats()
    {
        return _itemArmor ? _itemArmor.AddProtection : 0;
    }

    public float GlovesStats()
    {
        return _itemGloves ? _itemGloves.AddProtection : 0;
    }

    public float GreavesStats()
    {
        return _itemGreaves ? _itemGreaves.AddProtection : 0;
    }

    public float BootsStats()
    {
        return _itemBoots ? _itemBoots.AddProtection : 0;
    }

    public float RingStats()
    {
        return _itemRing ? _itemRing.AddProtection : 0;
    }

    private void RefreshUI()
    {
        itemHelmetImage.sprite = _itemHelmet ? _itemHelmet.Sprite : nullSprite;
        itemArmorImage.sprite = _itemArmor ? _itemArmor.Sprite : nullSprite;
        itemGlovesImage.sprite = _itemGloves ? _itemGloves.Sprite : nullSprite;
        itemGreavesImage.sprite = _itemGreaves ? _itemGreaves.Sprite : nullSprite;
        itemBootsImage.sprite = _itemBoots ? _itemBoots.Sprite : nullSprite;
        itemRingImage.sprite = _itemRing ? _itemRing.Sprite : nullSprite;
    }
}
