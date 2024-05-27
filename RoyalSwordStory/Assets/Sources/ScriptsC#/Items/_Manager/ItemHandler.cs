using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemHandler : MonoBehaviour, IItem
{
    [SerializeField] private float speedFlyUp = 15;
    [SerializeField] private float radiusFlyUp = 5;
    [SerializeField] private LayerMask tagetLayer;
    [Space]
    [SerializeField] private Item item;
    [SerializeField] private Text countItemText;
    [SerializeField] private GameObject destroyEffectPrefab;
    [SerializeField, Range(1, 64)] private int countItem = 1;

    private Collider2D _target;

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

    private void FixedUpdate()
    {
        if(_target == null)
            _target = Physics2D.OverlapCircle(transform.position, radiusFlyUp, tagetLayer);
        else
        {
            speedFlyUp += Time.fixedDeltaTime;
            transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, speedFlyUp * Time.fixedDeltaTime);
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, radiusFlyUp);
    }
}
