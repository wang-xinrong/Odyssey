using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    public GameObject CoinPrefab;
    private Damageable damagebale;
    private float CoinToHPRatio;

    // Start is called before the first frame update
    void Start()
    {
        CoinToHPRatio = StatsManager.Instance.GetCoinToHPRatio();
        damagebale = GetComponent<Damageable>();
    }

    // Update is called once per frame
    public void SpawnCoin()
    {
        GameObject coin = Instantiate(CoinPrefab
            , transform.position
            , Quaternion.identity
            , gameObject.transform.parent) as GameObject;

        coin.GetComponent<Coin>().SetQuantity(
            (int) (damagebale.MaxHealth * CoinToHPRatio));

        // after spawning the object, this script should
        // be deactivated to avoid a second round of spawning
        this.enabled = false;
    }
}
