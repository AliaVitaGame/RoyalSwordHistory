using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemHandler : MonoBehaviour, IItem
{
    [SerializeField] private Item item;
    [SerializeField] private Text countItemText;
    [SerializeField] private GameObject destroyEffectPrefab;
    [SerializeField, Range(1, 64)] private int countItem = 1;

    public int CountItem
    {
        get => countItem;
        set => countItem = Mathf.Min(value, 1, 64);
    }

    private void Start()
    {
        if (item == null)
        {
            Debug.LogWarning("Item null. GameObject: " + gameObject.name + ". The object has been deleted");
            Destroy(gameObject);
            return;
        }
        GetComponent<Collider2D>().isTrigger = true;
        GetComponentInChildren<SpriteRenderer>().sprite = item.Sprite;

      if (item.IsEquip) countItemText.gameObject.SetActive(false);
        RefreshUI();
    }

    public Item GetItem() => item;

    public void Destroy()
    {
        var tempFX = Instantiate(destroyEffectPrefab, transform.position, Quaternion.identity);
        Destroy(tempFX, 0.6f);
        Destroy(gameObject);
    }

    public void SetCountItem(int count)
    {
        countItem = count;
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (countItemText)
        {
            if(countItemText.gameObject.activeSelf)
                countItemText.text = $"{countItem}"; 
        }
    }
}
