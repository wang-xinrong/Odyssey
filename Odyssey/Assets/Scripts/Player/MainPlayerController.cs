using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MainPlayerController : MonoBehaviour
{
    [SerializeField]
    private Animator char1Animator;
    [SerializeField]
    private Animator char2Animator;
    private Vector2 _moveInput = Vector2.down;
    private GameObject char1, char2;
    private bool isMK = true;


    // new, for health system bug
    public GameObject HealthBar;
    private GameObject _healthBar;

    // new for direction setup after swapping bug,
    // the _lastMovement vector is a non-zero directional
    // vector (in up-down-left-right directions)
    private Vector2 _lastMovement = Vector2.down;

    // Start is called before the first frame update
    void Start()
    {
        char1 = GameObject.Find("mk");
        char2 = GameObject.Find("zbj");
        _healthBar = GameObject.Find("HealthBar");
        char1.SetActive(true);
        char2.SetActive(false);
        // set up the initial direction faced by the sprite
        Directions.SpriteDirectionSetUp(char1.GetComponent<PlayerController>(), _lastMovement);
    }

    // Update is called once per frame
    void Update()
    {
        if (isMK) {
            /*
            char1.SetActive(true);
            char2.SetActive(false);
            */
            char2.transform.position = char1.transform.position;
            char2.transform.rotation = char1.transform.rotation;
        } else {
            /*
            char1.SetActive(false);
            char2.SetActive(true);
            */
            char1.transform.position = char2.transform.position;
            char1.transform.rotation = char2.transform.rotation;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        //if (!_damageable.IsAlive) return;
        _moveInput = context.ReadValue<Vector2>();
        if (_moveInput != Vector2.zero)// && CanMove)
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
        if (isMK)
        {
            char1.SetActive(true);
            char2.SetActive(false);
            Directions.SpriteDirectionSetUp(char1.GetComponent<PlayerController>(), _lastMovement);
            //char2.transform.position = char1.transform.position;
            //char2.transform.rotation = char1.transform.rotation;
        }
        else
        {
            char1.SetActive(false);
            char2.SetActive(true);
            Directions.SpriteDirectionSetUp(char2.GetComponent<PlayerController>(), _lastMovement);
            //char1.transform.position = char2.transform.position;
            //char1.transform.rotation = char2.transform.rotation;
        }
        _healthBar.GetComponent<HealthBarScript>().Swap();
    }
}
