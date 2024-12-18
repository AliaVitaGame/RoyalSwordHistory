using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Button activeInventoryButton;
    [Space]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button activePauseButton;
    [Space]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private GameObject[] additionalPanels;

    private void Start()
    {
        DeactivateAllPanel();
    }

    public void InputInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
            SetActiveInventory(!inventoryPanel.activeSelf); 
    }
    public void InputPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            SetActivePause(!pausePanel.activeSelf);
    }

    public void SetActiveInventory(bool active)
    {
        if(IsCanOpen() == false) return;

        DeactivateAllPanel();
        inventoryPanel.SetActive(active);
        OpenUI(active);
    }

    public void SetActivePause(bool active)
    {
        if (IsCanOpen() == false) return;

        DeactivateAllPanel();
        pausePanel.SetActive(active);
        OpenUI(active);
    }

    private void OpenUI(bool active)
    {
        ManagerUI.Instance.OpenUI(active);
    }

    public void DeactivateAllPanel()
    {
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        OpenUI(false);

        if(additionalPanels != null)
        {
            if(additionalPanels.Length > 0)
            {
                for (int i = 0; i < additionalPanels.Length; i++)
                {
                    if (additionalPanels[i])
                        additionalPanels[i].SetActive(false);
                }
            }
        }
    }

    private bool IsCanOpen()
    {
        if (playerStats)
        {
            if(playerStats.IsDead)
            return false;
        }

        return true;
    } 

}
