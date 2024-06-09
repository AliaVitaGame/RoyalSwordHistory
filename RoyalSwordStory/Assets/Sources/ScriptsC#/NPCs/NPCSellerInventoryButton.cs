using UnityEngine;
using UnityEngine.UI;

public class NPCSellerInventoryButton : MonoBehaviour
{
    [Header("Temproary test variables")]
    [SerializeField] private int coinsAmountTemproary;

    [Header("Items")]
    [SerializeField] private Item[] sellingItems;
    [SerializeField] private Image[] sellItemImages;

    [Header("Description panel block")]
    [SerializeField] private GameObject itemDescriptionPanel;
    [SerializeField] private Text selectedItemNameText;
    [SerializeField] private Text selectedDescriptionText;
    [SerializeField] private Image selectedItemIconImage;

    [Header("Others")]
    [SerializeField] private LayerMask layerPlayer;

    private int currentItemIndex;
    private InventoryPlayer potentialBuyerInventory;

    private void Start()
    {
        for (int i = 0; i < sellItemImages.Length; i++)
        {
            sellItemImages[i].sprite = sellingItems[i].Sprite;
        }

        itemDescriptionPanel.SetActive(false);
    }

    private void OnEnable()
    {
        NPCButtonEventTriggers.OnCloseItemDescriptionPanel += NPCButtonEventTriggers_OnCloseItemDescriptionPanel;
        NPCButtonEventTriggers.OnBuyItemButtonPressed += NPCButtonEventTriggers_OnBuyItemButtonPressed;
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed += NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }

    private void OnDisable()
    {
        NPCButtonEventTriggers.OnCloseItemDescriptionPanel -= NPCButtonEventTriggers_OnCloseItemDescriptionPanel;
        NPCButtonEventTriggers.OnBuyItemButtonPressed -= NPCButtonEventTriggers_OnBuyItemButtonPressed;
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed -= NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }
    private void NPCButtonEventTriggers_OnOpenPocketsButtonPressed(bool isPanelActive)
    {
        if (isPanelActive == true)
        {
            var potentailBuyer = Physics2D.OverlapCircle(transform.position, 3.5f, layerPlayer); // Note: 3.5f is radius for activation of shop mechanics that can be seen in NPCController script
            if (potentailBuyer.TryGetComponent(out InventoryPlayer inventoryPlayer))
            {
                potentialBuyerInventory = inventoryPlayer;
            }
        }
        else if (isPanelActive == false) 
        {
            potentialBuyerInventory = null;
        }
    }

    private void NPCButtonEventTriggers_OnBuyItemButtonPressed()
    {
        if (coinsAmountTemproary >= sellingItems[currentItemIndex].Price)
        {
            Debug.Log("Items with number (" + currentItemIndex +")is going to be bougt");

            if (potentialBuyerInventory != null)
            {
                potentialBuyerInventory.AddItem(sellingItems[currentItemIndex] , 1);
            }
        }
        else if(coinsAmountTemproary <= sellingItems[currentItemIndex].Price)
        {
            Debug.Log("You don't have enough coins to buy the next item" + currentItemIndex);
        }
    }

    private void NPCButtonEventTriggers_OnCloseItemDescriptionPanel()
    {
        itemDescriptionPanel.SetActive(false);
    }


    public void SeeCurrentItem(int itemIndex)
    {
        currentItemIndex = itemIndex;
        itemDescriptionPanel.SetActive(true);
        Debug.Log("good with next number was chosen: " + itemIndex);
        SetItemDescription(itemIndex);
    }

    public void SetItemDescription(int itemIndex)
    {
        selectedItemNameText.text = sellingItems[itemIndex].NameItemRU;
        selectedItemIconImage.sprite = sellingItems[itemIndex].Sprite;
        selectedDescriptionText.text = sellingItems[itemIndex].DescriptionRU;
    }
}
