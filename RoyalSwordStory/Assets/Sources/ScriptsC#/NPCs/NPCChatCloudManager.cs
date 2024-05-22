using System.Collections;
using UnityEngine;

public class NPCChatCloudManager : MonoBehaviour
{
    [SerializeField] private GameObject chatPanel;
    [SerializeField] private GameObject interactionPanel;

    private bool isCycled = false;

    private void Start()
    {
        NPCController.OnPlayerIsClose += NPCController_OnPlayerIsClose;
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed += NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }

    private void OnDisable()
    {
        NPCController.OnPlayerIsClose -= NPCController_OnPlayerIsClose;
        NPCButtonEventTriggers.OnOpenPocketsButtonPressed -= NPCButtonEventTriggers_OnOpenPocketsButtonPressed;
    }

    private void NPCButtonEventTriggers_OnOpenPocketsButtonPressed(bool shopPanelIsActive)
    {
        if(shopPanelIsActive == true) 
        {
            chatPanel.SetActive(false);
            interactionPanel.SetActive(false);
        }
    }

    private void NPCController_OnPlayerIsClose()
    {
       
    }

    public void TurnOnChatPanel(int indexOfPanel)
    {
        switch (indexOfPanel)
        {
            case 0: chatPanel.SetActive(true); break;
            case 1: chatPanel.SetActive(false); interactionPanel.SetActive(true); break;
            case 2: chatPanel.SetActive(false); interactionPanel.SetActive(false); break;
        }
    }

}
