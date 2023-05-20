using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public TMP_Text _healthBarText;
    public Slider _healthSlider;
    private GameObject _player;
    private Damageable _playerDamageable;

    private void Awake()
    {
        // for later implementation of character swapping, need to consider
        // an alternative way to get the player
        _player = GameObject.FindGameObjectWithTag("Player");

        if (_player == null)
        {
            Debug.Log("No player found in the scene. " +
                "If a player has indeed been included, make sure it has tag 'player'");
        }

        _playerDamageable = _player.GetComponent<Damageable>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _healthSlider.value = CalculateSliderPercentage(_playerDamageable.Health
            , _playerDamageable.MaxHealth);
        _healthBarText.text = "HP " + _playerDamageable.Health
            + " / " + _playerDamageable.MaxHealth;
    }

    private void OnEnable()
    {
        _playerDamageable.HealthUpdated.AddListener(OnPlayerHealthUpdated);
    }

    private void OnDisable()
    {
        _playerDamageable.HealthUpdated.RemoveListener(OnPlayerHealthUpdated);
    }

    // if the percentage is 90%, returns 0.90
    private float CalculateSliderPercentage(int currentHealth, int maxHealth)
    {
        return currentHealth * 1f / maxHealth;
    }

    private void OnPlayerHealthUpdated(int newHealth, int maxHealth)
    {
        _healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        _healthBarText.text = "HP " + newHealth
            + " / " + maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
