using UnityEngine;

[RequireComponent(typeof(InventoryPlayer))]
public class InteractionObject : MonoBehaviour
{
    [SerializeField] private float radius = 3;

    private InventoryPlayer _inventoryPlayer;

    private void Start()
    {
        _inventoryPlayer = GetComponent<InventoryPlayer>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interaction();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IItem item))
        {
            if (_inventoryPlayer.AddItem(item.GetItem(), item.CountItem))
            {
                item.Destroy();
            }
            else
            {
                Debug.Log("There is no place in the inventory");
            }
        }
    }

    public void Interaction()
    {
        var temps = Physics2D.OverlapCircleAll(transform.position, radius);

        for (int i = 0; i < temps.Length; i++)
        {
            if (temps[i].TryGetComponent(out IItem item))
            {
                if (_inventoryPlayer.AddItem(item.GetItem(), item.CountItem))
                {
                    item.Destroy();
                }
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Color color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
} 
