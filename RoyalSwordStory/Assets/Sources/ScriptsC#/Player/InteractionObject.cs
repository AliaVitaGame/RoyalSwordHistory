using UnityEngine;

[RequireComponent(typeof(InventoryPlayer))]
public class InteractionObject : MonoBehaviour
{
    [SerializeField] private float radius = 3;
    [Space]
    [SerializeField] private AudioClip audioInteraction;
    [SerializeField] private AudioFX audioFX;

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
            var countItem = item.GetItem().IsStack ? item.CountItem : 1;
            if (_inventoryPlayer.AddItem(item.GetItem(), countItem))
            {
                PlayAudio();
                item.Destroy();
            }
            else
            {
                if(item != null)
                    item.SetCountItem(_inventoryPlayer.GetRemainderLastItem());
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
                    PlayAudio();
                    item.Destroy();
                    item.SetCountItem(_inventoryPlayer.GetRemainderLastItem());
                }
            }
            else
            {
                if (item != null)
                    item.SetCountItem(_inventoryPlayer.GetRemainderLastItem());
            }
        }
    }


    private void PlayAudio()
    {
        audioFX.PlayAudioRandomPitch(audioInteraction);
    }

    private void OnDrawGizmosSelected()
    {
        Color color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
} 
