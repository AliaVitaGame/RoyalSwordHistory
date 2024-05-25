using UnityEngine;
using UnityEngine.UI;

public class NPCSellerInventoryButton : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] private Item[] sellingItems;
    [SerializeField] private Image[] sellItemImages;

    [Header("Description panel block")]
    [SerializeField] private GameObject itemDescriptionPanel;
    [SerializeField] private Text selectedItemNameText;
    [SerializeField] private Text selectedDescriptionText;
    [SerializeField] private Image selectedItemIconImage;


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
    }
    private void OnDisable()
    {
        NPCButtonEventTriggers.OnCloseItemDescriptionPanel -= NPCButtonEventTriggers_OnCloseItemDescriptionPanel;
    }

    private void NPCButtonEventTriggers_OnCloseItemDescriptionPanel()
    {
        itemDescriptionPanel.SetActive(false);
    }


    public void SeeCurrentItem(int itemIndex)
    {
        itemDescriptionPanel.SetActive(true);
        Debug.Log("good with next number was chosen: " + itemIndex);
        SetItemDescription(itemIndex);
    }

    public void SetItemDescription(int itemIndex)
    {
        selectedItemNameText.text = sellingItems[itemIndex].NameItem;
        selectedItemIconImage.sprite = sellingItems[itemIndex].Sprite;
        selectedDescriptionText.text = sellingItems[itemIndex].Description;
    }
}
