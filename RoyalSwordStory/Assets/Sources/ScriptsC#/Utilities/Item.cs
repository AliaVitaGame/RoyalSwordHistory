using UnityEngine;

[CreateAssetMenu(menuName = "Item", fileName = "Item")]
public class Item : ScriptableObject
{
    [Header("Main")]
    [SerializeField] private TypeItem typeItem;
    [SerializeField] private Sprite spriteItem;
    [Space]
    [Header("Coin")]
    [SerializeField] private float coinCount;
    [Header("Protection")]
    [SerializeField] private float addProtection;
    [Header("Recovery")]
    [SerializeField] private float addHealth;
    [SerializeField] private float addMana;

    public TypeItem Type => typeItem;
    public Sprite Sprite => spriteItem;
    public float CoinCount => coinCount;
    public float AddProtection => addProtection;
    public float AddHealth => addHealth;
    public float AddMana => addMana;

    public enum TypeItem
    {
        Coin = 0,
        Recovery = 1,

        Helmet = 2,
        Armor = 3,
        Greaves = 4,
        Boots = 5,

        Ring = 6
    }
}
