using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShopActivator : MonoBehaviour
{
    public GameObject Shop;
    public bool IsNearShop = false;
    public bool IsShopOpen = false;

    private void Awake()
    {
        Shop.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearShop = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearShop = false;
    }

    public void OnOpenShop(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!IsNearShop) return;
            Shop.SetActive(!IsShopOpen);
            IsShopOpen = !IsShopOpen;
        }
    }
}
