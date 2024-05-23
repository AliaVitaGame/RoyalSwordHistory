using UnityEngine;
using UnityEngine.UI;

public class PanelActivator : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private KeyCode activeInventoryKey = KeyCode.Tab;
    [SerializeField] private Button activeInventoryButton;
    [Space]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private KeyCode activePauseKey = KeyCode.Escape;
    [SerializeField] private Button activePauseButton;


    private void Start()
    {
        DeactivateAllPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(activeInventoryKey))
            SetActiveInventory(!inventoryPanel.activeSelf);

        if (Input.GetKeyDown(activePauseKey))
            SetActivePause(!pausePanel.activeSelf);

    }

    public void SetActiveInventory(bool active)
    {
        DeactivateAllPanel();
        inventoryPanel.SetActive(active);
        OpenUI(active);
    }

    public void SetActivePause(bool active)
    {
        DeactivateAllPanel();
        pausePanel.SetActive(active);
        OpenUI(active);
    }

    private void OpenUI(bool active)
    {
        ManagerUI.Instance.OpenUI(active);
    }

    private void DeactivateAllPanel()
    {
        inventoryPanel.SetActive(false);
        pausePanel.SetActive(false);
        OpenUI(false);
    }



}
