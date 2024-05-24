using UnityEngine;
using UnityEngine.UI;

public class NPCSellerInventoryButton : MonoBehaviour
{

    [SerializeField] private Item[] sellingItems;

    [SerializeField] private Image[] sellItemImages;

    private void Start()
    {
        for (int i = 0; i < sellItemImages.Length; i++)
        {
            sellItemImages[i].sprite = sellingItems[i].Sprite;
        }
    }

    public void BuyCurrentItem(int itemIndex)
    {
        Debug.Log("good with next number was bought: " + itemIndex);



    }


}
