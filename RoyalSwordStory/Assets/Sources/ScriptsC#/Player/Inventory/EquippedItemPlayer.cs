using UnityEngine;

public class EquippedItemPlayer : MonoBehaviour
{
    [SerializeField] private CellEquippedItem _cellHelmet;
    [SerializeField] private CellEquippedItem _cellArmor;
    [SerializeField] private CellEquippedItem _cellGloves;
    [SerializeField] private CellEquippedItem _cellGreaves;
    [SerializeField] private CellEquippedItem _cellBoots;
    [SerializeField] private CellEquippedItem _cellRing;

    public void Equip(Item itemCell)
    {
        var item = itemCell;
        if (item.Type == Item.TypeItem.Helmet) _cellHelmet.AddItem(item);
        else if (item.Type == Item.TypeItem.Armor) _cellArmor.AddItem(item);
        else if (item.Type == Item.TypeItem.Gloves) _cellGloves.AddItem(item);
        else if (item.Type == Item.TypeItem.Greaves) _cellGreaves.AddItem(item);
        else if (item.Type == Item.TypeItem.Boots) _cellBoots.AddItem(item);
        else if (item.Type == Item.TypeItem.Ring) _cellRing.AddItem(item);
    }

    public float GetDamageRepaymentPercentage()
    {
        return HelmetStats() + ArmorStats() + GlovesStats() + GreavesStats() + BootsStats();
    }

    public float HelmetStats()
    {
        return _cellHelmet ? _cellHelmet.GetItem().AddProtection : 0;
    }

    public float ArmorStats()
    {
        return _cellArmor ? _cellArmor.GetItem().AddProtection : 0;
    }

    public float GlovesStats()
    {
        return _cellGloves ? _cellGloves.GetItem().AddProtection : 0;
    }

    public float GreavesStats()
    {
        return _cellGreaves ? _cellGreaves.GetItem().AddProtection : 0;
    }

    public float BootsStats()
    {
        return _cellBoots ? _cellBoots.GetItem().AddProtection : 0;
    }

    public float RingStats()
    {
        return _cellRing ? _cellRing.GetItem().AddProtection : 0;
    }
}
