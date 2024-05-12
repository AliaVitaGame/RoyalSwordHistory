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
