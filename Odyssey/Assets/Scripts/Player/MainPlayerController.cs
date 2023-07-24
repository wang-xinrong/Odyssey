using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class MainPlayerController : MonoBehaviour
{
    private Vector2 _moveInput = Vector2.down;
    // changed to public such that we are able to directly
    // plug in the character prefab instead of having to
    // search by name
    public GameObject char1, char2;

    public bool isChar1 = true;

    // new, for health system bug
    private GameObject _healthBar;

    // new, for SP bar
    public int SP = 0;
    public int MaxSP = 0;
    public UnityEvent<int, int> SPIncremented;
    public UnityEvent<int, int> SPDecremented;
    [SerializeField]
    private float _rechargeSPInterval;
    [SerializeField]
    private float[] charSpecialAttackCD;
    [SerializeField]
    private float[] charLastSpAttack;
    private float lastRechargeSPTime; 

    // new, for weapon pickup
    public UnityEvent<Weapon> OnDisplayCurrentWeapon;
    public UnityEvent<string, bool> OnDisplayPrimaryCharacter;
    public UnityEvent<string, bool> OnDisplaySecondaryCharacter;

    // new for direction setup after swapping bug,
    // the _lastMovement vector is a non-zero directional
    // vector (in up-down-left-right directions)
    private Vector2 _lastMovement = Vector2.down;

    private bool isGameOver = false;
    public UnityEvent OnDisplayGameOver;

    PlayerController primary;
    PlayerController secondary;
    Damageable primaryDamageable;
    Damageable secondaryDamageable;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar = GameObject.Find("HealthBar");
        primary = char1.GetComponent<PlayerController>();
        secondary = char2.GetComponent<PlayerController>();
        primaryDamageable = char1.GetComponent<Damageable>();
        secondaryDamageable = char2.GetComponent<Damageable>();
        char1.SetActive(true);
        char2.SetActive(false);
        // set up the initial direction faced by the sprite
        
        if (!primary)
        {
            return;
        }
        if (!secondary)
        {
            return;
        }
        Directions.SpriteDirectionSetUp(primary, _lastMovement);
        OnDisplayCurrentWeapon.Invoke(primary.weapon);
        OnDisplayPrimaryCharacter.Invoke(primary.charName, true);
        OnDisplaySecondaryCharacter.Invoke(secondary.charName, false);

        // fix mk healthbar not set up bug
        _healthBar.GetComponent<HealthBarScript>().Swap();
    }

    // helper method that takes a reference time and checks if the interval between the current
    // and reference time exceeds the given interval duration
    private bool HasSufficientTimePassed(float referenceTime, float intervalDuration)
    {
        return Time.time - referenceTime > intervalDuration; 
    }

    public bool specialAttackOffCD(int charNumber)
    {
        float prevAttackTime = charLastSpAttack[charNumber];
        return prevAttackTime == 0 || HasSufficientTimePassed(prevAttackTime, charSpecialAttackCD[charNumber]);
    }

    PlayerController temporaryController;

    private void checkIncrementSP()
    {
        if (isChar1)
        {
            temporaryController = char1.GetComponent<PlayerController>();
        } else
        {
            temporaryController = char2.GetComponent<PlayerController>();
        }

        _rechargeSPInterval = StatsManager.Instance
            .GetCharacterSPRegenrate(temporaryController.Character);

        // check if sufficient time has passed since last SP increment
        if (!HasSufficientTimePassed(lastRechargeSPTime, _rechargeSPInterval))
        {
            return;
        }
        if (SP >= MaxSP)
        {
            return;
        }
        SP++;
        lastRechargeSPTime = Time.time;
        // everything subscribing to SPIncremented event will be notified
        SPIncremented.Invoke(SP, MaxSP);
    }

    public void decrementSPBy(int amount, int charNumber)
    {
        if (charNumber >= 0)
        {
            charLastSpAttack[charNumber] = Time.time;
        }
        SP -= amount;
        // everything subscribing to SPIncremented event will be notified
        SPDecremented.Invoke(SP, MaxSP);
    }

    public void displaySwappedWeapon(Weapon weapon)
    {
        OnDisplayCurrentWeapon.Invoke(weapon);
    }

    public bool hasSufficientSP(int amount)
    {
        return SP >= amount;
    }

    void Update()
    {
        // see if sufficient time has elapsed since previous SP regen
        checkIncrementSP();

        // new, to fix the bug that after death of one character,
        // the player can still swap back and forth between the character
        // that is alive and the character that is dead
        if (!char1.GetComponent<PlayerController>().IsAlive() &&
            !char2.GetComponent<PlayerController>().IsAlive())
        {
            if (!isGameOver)
            {
                isGameOver = true;
                OnDisplayGameOver.Invoke();
            }
            return;
        } else
        {
            if (isChar1 && !char1.GetComponent<PlayerController>().IsAlive())
            {
                isChar1 = false;
            }
            if (!isChar1 && !char2.GetComponent<PlayerController>().IsAlive())
            {
                isChar1 = true;
            }
        }

        // original code
        if (isChar1) {
            char2.transform.position = char1.transform.position;
            char2.transform.rotation = char1.transform.rotation;
        } else {
            char1.transform.position = char2.transform.position;
            char1.transform.rotation = char2.transform.rotation;
        }

        SwapCharacters();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        _moveInput = context.ReadValue<Vector2>();

        if (_moveInput != Vector2.zero)
        {
            _lastMovement = _moveInput;
        }     
    }

    public void OnSwap(InputAction.CallbackContext context) 
    {
        if (!CanSwap) return;
        // characters not allowed to swap when performing special attack
        if (primary._currentState == PlayerController.State.Special ||
            secondary._currentState == PlayerController.State.Special) return;

        if (!primary.IsAlive()) return;
        if (!secondary.IsAlive()) return;

        if (context.started && SP >= 10) {
            // change the state of the character to be deactivated to idle
            if (isChar1)
            {
                primary.StartIdling();
                GetComponentInChildren<MKAttackSpritePatch>()
                    .DeactivateSprites();
            }

            if (!isChar1) secondary.StartIdling();

            isChar1 = !isChar1;

            SwapCharacters();
            decrementSPBy(10, -1);   
        }
    }

    private bool IsChar1Active = true;
    private bool _canSwap = true;
    public bool CanSwap
    {
        set
        {
            _canSwap = value;
        }

        get
        {
            return _canSwap;
        }
    }


    private void SwapCharacters()
    {
        if (IsChar1Active == isChar1) return;
        PlayerController primary = char1.GetComponent<PlayerController>();
        PlayerController secondary = char2.GetComponent<PlayerController>();


        // the IsAlive condition is added to ensure the player can only
        // be swapped into if he is alive
        if (isChar1 && primary.IsAlive())
        {
            char1.SetActive(true);
            char2.SetActive(false);
            Directions.SpriteDirectionSetUp(primary, _lastMovement);
            _healthBar.GetComponent<HealthBarScript>().Swap();

            IsChar1Active = true;
            OnDisplayCurrentWeapon.Invoke(primary.weapon);
            OnDisplayPrimaryCharacter.Invoke(primary.charName, true);
            if (secondary.IsAlive())
            {
                OnDisplaySecondaryCharacter.Invoke(secondary.charName, false);
            }
        }
        else if (!isChar1 && char2.GetComponent<PlayerController>().IsAlive())
        {
            char1.SetActive(false);
            char2.SetActive(true);
            Directions.SpriteDirectionSetUp(secondary, _lastMovement);
            _healthBar.GetComponent<HealthBarScript>().Swap();

            IsChar1Active = false;
            OnDisplayCurrentWeapon.Invoke(secondary.weapon);
            OnDisplaySecondaryCharacter.Invoke(secondary.charName, true);
            if (primary.IsAlive())
            {
                OnDisplayPrimaryCharacter.Invoke(primary.charName, false);
            }
        }
    }

    public bool ReplenishSP(int amount)
    {
        // cant use the item if both characters are dead
        if (!char1.GetComponent<PlayerController>().IsAlive() &&
            !char2.GetComponent<PlayerController>().IsAlive()) return false;

        // cant use the item if the SP is currently full
        if (SP == MaxSP) return false;

        SP = Mathf.Min(MaxSP, SP + amount);

        SPIncremented.Invoke(SP, MaxSP);

        return true;
    }

    public Vector3 GetCharPosition()
    {
        return char1.transform.position;
    }

    public PlayerController GetCurrentCharacterPlayerController()
    {
        return isChar1 ? primary
                       : secondary;
    }
}
