using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemHandler : MonoBehaviour, IItem
{
    [SerializeField] private Item item;
    [SerializeField, Range(1, 64)] private int countItem = 1;

    public int CountItem
    {
        get => countItem;
        set => countItem = Mathf.Min(value, 1, 64);
    }

    private void Start()
    {
        if(item == null)
        {
            Debug.LogWarning("Item null. GameObject: " + gameObject.name + ". The object has been deleted");
            Destroy(gameObject);
            return;
        }
        GetComponent<Collider2D>().isTrigger = true;
        GetComponentInChildren<SpriteRenderer>().sprite = item.Sprite;
    }

    public Item GetItem() => item;

    public void Destroy() => Destroy(gameObject);
}
