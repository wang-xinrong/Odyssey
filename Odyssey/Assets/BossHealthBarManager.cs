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

    private void Awake()
    {
        // should work as long as there is only one
        // canvas that is currently active
        GameCanvas = FindObjectOfType<Canvas>();
    }

    /*
    private void OnEnable()
    {
        BossStageManager.EnterStage += SetUpBars;
        BossStageManager.EnterStage += SetUpStamps;
    }

    private void OnDisable()
    {
        BossStageManager.EnterStage -= SetUpBars;
        BossStageManager.EnterStage -= SetUpStamps;
    }

    private void SetUpBars(BossStageManager.BossStage stage)
    {

    }

    private void SetUpStamps(BossStageManager.BossStage stage)
    {

    }

    */

}
