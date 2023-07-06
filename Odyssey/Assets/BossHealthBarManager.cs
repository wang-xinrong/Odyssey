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
    private Canvas GameCanvas;
    private Damageable bossDamageable;
    private bool activated = false;
    public GameObject BossHealthUI;

    private void Awake()
    {
        // should work as long as there is only one
        // canvas that is currently active
        GameCanvas = FindObjectOfType<Canvas>();
        ActivateHealthBars(-1);
        ActivateStamps(-1);
        BossHealthUI.SetActive(false);
    }

    
    private void OnEnable()
    {
        BossStageManager.OnEnterBossStage += SetUpBossHealthUI;
        BossStageManager.OnPassBossDamageable += StoreReferenceToBossDamageable;
    }

    private void OnDisable()
    {
        BossStageManager.OnEnterBossStage -= SetUpBossHealthUI;
        BossStageManager.OnPassBossDamageable -= StoreReferenceToBossDamageable;
    }

    private void SetUpBossHealthUI(string bossName, BossStageManager.BossStage stage)
    {
        if (stage == BossStageManager.BossStage.End)
        {
            Debug.Log("here");
            ActivateBossHealthUI(false);
            return;
        }

        BossName.text = bossName;
        ActivateBossHealthUI(true);
        SetUpBars(stage);
        SetUpStamps(stage);
    }

    private void SetUpBars(BossStageManager.BossStage stage)
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

    private void SetUpStamps(BossStageManager.BossStage stage)
    {
        if (stage == BossStageManager.BossStage.One)
        {
            ActivateStamps(0);
        }

        if (stage == BossStageManager.BossStage.Two)
        {
            ActivateStamps(1);
        }

        if (stage == BossStageManager.BossStage.Three)
        {
            ActivateStamps(2);
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

    private void ActivateStamps(int index)
    {
        foreach (Image image in Stamps)
        {
            image.enabled = false;
        }

        if (index < 0 || index >= Stamps.Length) return;
        Stamps[index].enabled = true;
    }

    private void ActivateBossHealthUI(bool value)
    {
        if (activated == value) return;
        BossHealthUI.SetActive(value);
        activated = value;
    }
}
