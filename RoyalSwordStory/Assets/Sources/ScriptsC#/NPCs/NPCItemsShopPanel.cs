using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCItemsShopPanel : MonoBehaviour
{
    [SerializeField] private GameObject shopPanel;

    private void Awake()
    {
        if(shopPanel == null)
        {
            Debug.LogWarning("Ты забыл вставить панель с айтемами для продажи сюда");
            return;
        }

        shopPanel.SetActive(false);
    }

    private void OnEnable()
    {
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed += NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }

    private void OnDisable()
    {
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed -= NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }

    private void NPCButtonEventTriggers_OnOpenPocketsButtonPressed(bool panelIsActive)
    {
        if (panelIsActive == true)
            shopPanel.SetActive(true);
        else if(panelIsActive ==  false)
            shopPanel.SetActive(false);
    }
}
