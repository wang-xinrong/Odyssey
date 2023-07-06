using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BossHealthBarManager : MonoBehaviour
{
    public GameObject[] HealthBars;
    public Image[] Stamps;
    public TMP_Text BossName;
    public Canvas GameCanvas;
    private Damageable bossDamageable;

    private void Awake()
    {
        // should work as long as there is only one
        // canvas that is currently active
        GameCanvas = FindObjectOfType<Canvas>();
        ActivateHealthBars(-1);
    }

    
    private void OnEnable()
    {
        BossStageManager.OnEnterBossStage += SetUpBars;
        BossStageManager.OnEnterBossStage += SetUpStamps;
        BossStageManager.OnPassBossDamageable += StoreReferenceToBossDamageable;
    }

    private void OnDisable()
    {
        BossStageManager.OnEnterBossStage -= SetUpBars;
        BossStageManager.OnEnterBossStage -= SetUpStamps;
        BossStageManager.OnPassBossDamageable -= StoreReferenceToBossDamageable;
    }

    private void SetUpBars(string BossName, BossStageManager.BossStage stage)
    {
        if (stage == BossStageManager.BossStage.One)
        {
            ActivateHealthBars(0);
        }

        if (stage == BossStageManager.BossStage.Two)
        {
            ActivateHealthBars(1);
        }

        if (stage == BossStageManager.BossStage.Three)
        {
            ActivateHealthBars(2);
        }
    }

    private void SetUpStamps(string BossName, BossStageManager.BossStage stage)
    {
        if (stage == BossStageManager.BossStage.One)
        {

        }

        if (stage == BossStageManager.BossStage.Two)
        {

        }

        if (stage == BossStageManager.BossStage.Three)
        {

        }
    }

    private void StoreReferenceToBossDamageable(Damageable damageable)
    {
        bossDamageable = damageable;
        for (int i = 0; i < HealthBars.Length; i++)
        {
            HealthBars[i].GetComponent<EnemyHealthBarScript>()
                .enemyDamageable = bossDamageable;
        }
    }

    private void ActivateHealthBars(int index)
    {
        foreach (GameObject go in HealthBars)
        {
            go.SetActive(false);
        }

        if (index < 0 || index >= HealthBars.Length) return;
        HealthBars[index].SetActive(true);
    }
}
