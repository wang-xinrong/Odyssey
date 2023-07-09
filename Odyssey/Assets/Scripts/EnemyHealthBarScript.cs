using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarScript : MonoBehaviour
{
    private Slider _healthSlider;
    public Damageable enemyDamageable;

    private void Awake()
    {
        _healthSlider = GetComponent<Slider>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthSlider.value = CalculateSliderPercentage(enemyDamageable.Health
            , enemyDamageable.MaxHealth);
        enemyDamageable.HealthUpdated.AddListener(OnPlayerHealthUpdated);
    }

    
    private void OnEnable()
    {
        enemyDamageable.HealthUpdated.AddListener(OnPlayerHealthUpdated);
        _healthSlider.value = CalculateSliderPercentage(enemyDamageable.Health
            , enemyDamageable.MaxHealth);
    }

    private void OnDisable()
    {
        enemyDamageable.HealthUpdated.RemoveListener(OnPlayerHealthUpdated);
    }
    

    // if the percentage is 90%, returns 0.90
    private float CalculateSliderPercentage(int currentHealth, int maxHealth)
    {
        return currentHealth * 1f / maxHealth;
    }

    private void OnPlayerHealthUpdated(int newHealth, int maxHealth)
    {
        _healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
    }

    private Vector2 left = new Vector3(-1, 1, 1);
    private Vector2 right = new Vector3(1, 1, 1);
    
    private void Update()
    {
        if (enemyDamageable.transform.localScale.x == -1)
        {
            transform.parent.localScale = left;
        } else
        {
            transform.parent.localScale = right;
        }

        if (_healthSlider.value <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    
}
