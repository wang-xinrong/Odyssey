using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CooldownElement : MonoBehaviour
{
    public GameObject timer;
    public GameObject overlay;
    private int remainingCooldownDuration;
    private float prevDecrementTime;
    void Start()
    {
        timer.SetActive(false);
        overlay.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        checkDecrementTime();
    }
    private void removeCooldownOverlay()
    {
        overlay.SetActive(false);
        timer.SetActive(false);
    }
    private void checkDecrementTime()
    {
        if (Time.time - prevDecrementTime >= 1.0f)
        {
            updateDuration();
        }
    }
    private void updateDuration()
    {
        remainingCooldownDuration--;
        TMP_Text text = timer.GetComponent<TMP_Text>();
        text.text = remainingCooldownDuration.ToString();
        prevDecrementTime = Time.time;
        if (remainingCooldownDuration == 0)
        {
            removeCooldownOverlay();
        }
    }
    public void DisplayCooldown(int duration)
    {
        overlay.SetActive(true);
        timer.SetActive(true);
        remainingCooldownDuration = duration + 1;
        updateDuration();
    }
}
