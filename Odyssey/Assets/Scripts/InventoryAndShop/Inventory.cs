using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Inventory : MonoBehaviour
{
    public bool IsInventoryOpen = false;

    public void OnOpen(InputAction.CallbackContext context)
    {
        if (GameStatus.Instance.IsGamePaused && !IsInventoryOpen) return;

        if (context.started)
        {
            Open();
            gameObject.SetActive(IsInventoryOpen);
        }
    }

    public void Open()
    {
        IsInventoryOpen = !IsInventoryOpen;
    }
}
