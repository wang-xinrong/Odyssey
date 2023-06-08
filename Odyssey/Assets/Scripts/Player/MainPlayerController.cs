using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    private Vector2 _moveInput = Vector2.down;
    // changed to public such that we are able to directly
    // plug in the character prefab instead of having to
    // search by name
    public GameObject char1, char2;

    private bool isChar1 = true;

    // new, for health system bug
    private GameObject _healthBar;

    // new for direction setup after swapping bug,
    // the _lastMovement vector is a non-zero directional
    // vector (in up-down-left-right directions)
    private Vector2 _lastMovement = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar = GameObject.Find("HealthBar");
        char1.SetActive(true);
        char2.SetActive(false);
        // set up the initial direction faced by the sprite
        Directions.SpriteDirectionSetUp(char1.GetComponent<PlayerController>(), _lastMovement);
    }

    void Update()
    {
        // new, to fix the bug that after death of one character,
        // the player can still swap back and forth between the character
        // that is alive and the character that is dead
        if (!char1.GetComponent<PlayerController>().IsAlive() &&
            !char2.GetComponent<PlayerController>().IsAlive())
        {
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
        if (context.started) {
            isChar1 = !isChar1;
        }
        SwapCharacters();
    }

    private void SwapCharacters()
    {
        // the IsAlive condition is added to ensure the player can only
        // be swapped into if he is alive
        if (isChar1 && char1.GetComponent<PlayerController>().IsAlive())
        {
            char1.SetActive(true);
            char2.SetActive(false);
            Directions.SpriteDirectionSetUp(char1.GetComponent<PlayerController>(), _lastMovement);
            _healthBar.GetComponent<HealthBarScript>().Swap();
        }
        else if (!isChar1 && char2.GetComponent<PlayerController>().IsAlive())
        {
            char1.SetActive(false);
            char2.SetActive(true);
            Directions.SpriteDirectionSetUp(char2.GetComponent<PlayerController>(), _lastMovement);
            _healthBar.GetComponent<HealthBarScript>().Swap();
        }
    }
}
