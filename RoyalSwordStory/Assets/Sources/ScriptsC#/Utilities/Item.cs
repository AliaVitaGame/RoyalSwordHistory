using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class Item : ScriptableObject
{
    [Header("Main")]
    [SerializeField] private TypeItem typeItem;
    [SerializeField] private Sprite spriteItem;
    [SerializeField] private bool isStack;
    [SerializeField] private bool isEquip;
    [Space]
    [Header("Coin")]
    [SerializeField] private float coinCount;
    [Header("Protection")]
    [SerializeField] private float addProtection;
    [SerializeField] private float addSpeed;
    [Header("Recovery")]
    [SerializeField] private float addHealth;
    [SerializeField] private float addMana;
    [Space]
    [Header("Other")]
    [SerializeField] private string nameItem = "Item";
    [SerializeField, TextArea(10,10)] private string description = "No description";

    public TypeItem Type => typeItem;
    public Sprite Sprite => spriteItem;
    public bool IsStack => isStack;
    public bool IsEquip => isEquip;
    public float CoinCount => coinCount;
    public float AddProtection => addProtection;
    public float AddSpeed => addSpeed;
    public float AddHealth => addHealth;
    public float AddMana => addMana;
    public string Description => description;
    public string NameItem => nameItem;

    public enum TypeItem
    {
        Coin = 0,
        Recovery = 1,

        Helmet = 2,
        Armor = 3,
        Gloves = 4,
        Greaves = 5,
        Boots = 6,
        Ring = 7,

        Magic = 8
    }
}
