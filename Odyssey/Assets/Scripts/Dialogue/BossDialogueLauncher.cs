using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDialogueLauncher : MonoBehaviour
{
    public delegate void DisplayDialogue(List<Dialogue> dialogues);
    public static event DisplayDialogue OnDisplayDialogue;
    private Room room; 
    public List<Dialogue> StageOneDialogues = new List<Dialogue>();
    public List<Dialogue> StageTwoDialogues = new List<Dialogue>();
    public List<Dialogue> StageThreeDialogues = new List<Dialogue>();
    public GameObject Boss;
    public bool[] hasDisplayed = new bool[3];

    void Start()
    {
        room = GetComponent<Room>();
    }
    void Update()
    {
        // Stage 1 Dialogue
        if (!room.Reached)
        {
            return;
        }
        if (OnDisplayDialogue == null)
        {
            return;
        }
        if (!hasDisplayed[0])
        {
            OnDisplayDialogue.Invoke(StageOneDialogues);
            hasDisplayed[0] = true;
        }
        Boss = GameObject.Find("Level1Boss");
        BossStageManager.BossStage currStage = Boss.GetComponent<BossStageManager>()._currentBossStage;
        if (currStage == BossStageManager.BossStage.Two && !hasDisplayed[1])
        {
            OnDisplayDialogue.Invoke(StageTwoDialogues);
            hasDisplayed[1] = true;
        }
        if (currStage == BossStageManager.BossStage.Three && !hasDisplayed[2])
        {
            OnDisplayDialogue.Invoke(StageThreeDialogues);
            hasDisplayed[2] = true;
        }
    }
}
