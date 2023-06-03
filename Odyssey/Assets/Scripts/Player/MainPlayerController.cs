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

    //[SerializeField]
    private Animator char1Animator;
    //[SerializeField]
    private Animator char2Animator;

    private bool isMK = true;


    // new, for health system bug
    private GameObject _healthBar;

    // new for direction setup after swapping bug,
    // the _lastMovement vector is a non-zero directional
    // vector (in up-down-left-right directions)
    private Vector2 _lastMovement = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {
        char1Animator = char1.GetComponent<PlayerController>().Animator;
        char2Animator = char2.GetComponent<PlayerController>().Animator;
        _healthBar = GameObject.Find("HealthBar");
        char1.SetActive(true);
        char2.SetActive(false);
        // set up the initial direction faced by the sprite
        Directions.SpriteDirectionSetUp(char1.GetComponent<PlayerController>(), _lastMovement);
    }

    // Update is called once per frame
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
            if (isMK && !char1.GetComponent<PlayerController>().IsAlive())
            {
                isMK = false;
            }
            if (!isMK && !char2.GetComponent<PlayerController>().IsAlive())
            {
                isMK = true;
            }

            SwapCharacters();
        }

        // original code
        if (isMK) {
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
            char1Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            char2Animator.SetFloat(AnimatorStrings.MoveXInput, _moveInput.x);
            char1Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            char2Animator.SetFloat(AnimatorStrings.MoveYInput, _moveInput.y);
            _lastMovement = _moveInput;
        }     
    }

    public void OnSwap(InputAction.CallbackContext context) 
    {
        if (context.started) {
            isMK = !isMK;
        }
        SwapCharacters();
    }

    private void SwapCharacters()
    {
        // the IsAlive condition is added to ensure the player can only
        // be swapped into if he is alive
        if (isMK && char1.GetComponent<PlayerController>().IsAlive())
        {
            char1.SetActive(true);
            char2.SetActive(false);
            Directions.SpriteDirectionSetUp(char1.GetComponent<PlayerController>(), _lastMovement);
            _healthBar.GetComponent<HealthBarScript>().Swap();
        }
        else if (!isMK && char2.GetComponent<PlayerController>().IsAlive())
        {
            char1.SetActive(false);
            char2.SetActive(true);
            Directions.SpriteDirectionSetUp(char2.GetComponent<PlayerController>(), _lastMovement);
            _healthBar.GetComponent<HealthBarScript>().Swap();
        }
        
    }
}
