using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class SPBarScript : MonoBehaviour
{
    public TMP_Text _spBarText;
    public Slider _spSlider;
    private GameObject _player;

    private void Awake()
    {
        // for later implementation of character swapping, need to consider
        // an alternative way to get the player
        _player = GameObject.Find("Player");
        MainPlayerController _playerController = _player.GetComponent<MainPlayerController>();

        if (_player == null)
        {
            Debug.Log("No player found in the scene. " +
                "If a player has indeed been included, make sure it has tag 'player'");
        }
    }

    private int getSP()
    {
        MainPlayerController _playerController = _player.GetComponent<MainPlayerController>();
        if (_playerController)
        {
            return _playerController.SP;
        }
        return 0;
    }

    private int getMaxSP()
    {
        MainPlayerController _playerController = _player.GetComponent<MainPlayerController>();
        if (_playerController)
        {
            return _playerController.MaxSP;
        }
        return 1;
    }

    // Start is called before the first frame update
    void Start()
    {
        _spSlider.value = CalculateSliderPercentage(getSP()
            , getMaxSP());
        _spBarText.text = "SP " + getSP()
            + " / " + getMaxSP();
        MainPlayerController _playerController = _player.GetComponent<MainPlayerController>();
        if (_playerController)
        {
            _playerController.SPIncremented.AddListener(OnSPUpdated);
            _playerController.SPDecremented.AddListener(OnSPUpdated);
        }
    }

    private void OnEnable()
    {
        //_playerDamageable.HealthUpdated.AddListener(OnPlayerHealthUpdated);

        // In the event where a character swap is performed, the initial health of
        // the character has to be determined again
        //_playerDamageable.HealthUpdated.Invoke(_playerDamageable.Health, _playerDamageable.MaxHealth);
    }

    private void OnDisable()
    {
        //_playerDamageable.HealthUpdated.RemoveListener(OnPlayerHealthUpdated);
    }

    // public void Swap()
    // {
    //     _player = GameObject.FindGameObjectWithTag("Player");
    //     if (_player != null)
    //     {
    //         // disable the healthBar such that the event system can be set up
    //         gameObject.SetActive(false);
    //         _playerDamageable = _player.GetComponent<Damageable>();
    //         gameObject.SetActive(true);
    //     } else
    //     {
    //         Debug.Log("The other character is dead");
    //     }
    // }

    // if the percentage is 90%, returns 0.90
    private float CalculateSliderPercentage(int currentHealth, int maxHealth)
    {
        return currentHealth * 1f / maxHealth;
    }

    private void OnSPUpdated(int newSP, int maxSP)
    {
        _spSlider.value = CalculateSliderPercentage(newSP, maxSP);
        _spBarText.text = "SP " + newSP
            + " / " + maxSP;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
