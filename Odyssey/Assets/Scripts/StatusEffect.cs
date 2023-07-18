using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatusEffect : MonoBehaviour
{
    public string s;
    private TMP_Text timer;
    private Image icon;
    private int remainingStatusEffectDuration;
    private float prevDecrementTime;
    void Start()
    {
        timer = GetComponentInChildren<TMP_Text>();
        icon = GetComponentInChildren<Image>();
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        checkDecrementTime();
    }
    private void checkDecrementTime()
    {
        if (Time.time - prevDecrementTime >= 1.0f)
        {
            updateDuration();
        }
    }
    private void removeStatusEffect()
    {
        gameObject.SetActive(false);
        StatusEffectManager.instance.availableGUIs.Enqueue(this);
    }
    private void updateDuration()
    {
        remainingStatusEffectDuration--;
        timer.text = remainingStatusEffectDuration.ToString();
        prevDecrementTime = Time.time;
        if (remainingStatusEffectDuration == 0)
        {
            removeStatusEffect();
        }
    }
    public void DisplayStatusEffect(string status, int duration)
    {
        gameObject.SetActive(true);
        remainingStatusEffectDuration = duration + 1;
        updateDuration();
    }
}
