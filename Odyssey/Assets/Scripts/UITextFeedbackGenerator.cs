using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UITextFeedbackGenerator : MonoBehaviour
{
    public GameObject PromptPrefab;
    private GameObject FeedbackManager;

    private void Start()
    {
        FeedbackManager = GameObject.Find("FeedbackManager");
    }

    public void GenerateTextFeedback(string text)//, Transform transform)
    {
        GameObject go = Instantiate(PromptPrefab, transform.position
            , transform.rotation, FeedbackManager.transform);

        go.GetComponent<TextMeshProUGUI>().text = text;
    }
}
