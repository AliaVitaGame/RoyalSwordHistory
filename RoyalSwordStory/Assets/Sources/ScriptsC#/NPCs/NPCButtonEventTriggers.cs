using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCButtonEventTriggers : MonoBehaviour
{
    public static event Action <bool> OnOpenPocketsButtonPressed;
    public static event Action OnCloseItemDescriptionPanel;
    public static event Action OnBuyItemButtonPressed;

    public void OpenSellersPocket()
    {
        OnOpenPocketsButtonPressed?.Invoke(true);
        ManagerUI.Instance.OpenUI(true);
    }

    public void CloseSellersPocket()
    {
        OnOpenPocketsButtonPressed?.Invoke(false);
        ManagerUI.Instance.OpenUI(false);
    }

    public void CloseItemDescriptionPanel()
    {
        OnCloseItemDescriptionPanel?.Invoke();
    }

    public void BuyItemTrigger()
    {
        OnBuyItemButtonPressed?.Invoke();
    }
}
