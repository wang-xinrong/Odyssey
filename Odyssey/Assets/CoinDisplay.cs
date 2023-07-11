using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class CoinDisplay : MonoBehaviour
{
    public TMP_Text CoinText;

    // Update is called once per frame
    void Update()
    {
        CoinText.text = InventoryManager.Instance.Money.ToString();
    }
}
