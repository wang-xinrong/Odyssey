using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int count = 0;
    private void OnDisable()
    {
        if (gameObject.scene.name != "IntegrationScene") return;
        //StatsManager.Instance.CurrentDifficulty =
          //  StatsManager.Difficulty.Normal;
    }
}
