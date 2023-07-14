using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static WeaponPickup;

public class KeyPressPromptPositionUpdate : MonoBehaviour
{
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public MainPlayerController mainPlayerController;
    public Vector3 DisplayOffset = new Vector3(0.051f, 0.768f, 0);

    private void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.enabled = false;
        spriteRenderer.enabled = false;
        //mainPlayerController = GetComponent<MainPlayerController>();
    }

    private void Update()
    {
        gameObject.transform.position = mainPlayerController
            .GetCharPosition() +
            DisplayOffset;
    }

    // Start is called before the first frame update
    public void OnEnable()
    {
        OnDisplayDroppedWeapon += PlayEAnimation;
        OnRemoveDisplay += StopPlayingAnimation;
        OnDisplaySwapCharacterPrompt += PlaySpaceBarAnimation;
    }

    public void OnDisable()
    {
        OnDisplayDroppedWeapon -= PlayEAnimation;
        OnRemoveDisplay -= StopPlayingAnimation;
        OnDisplaySwapCharacterPrompt -= PlaySpaceBarAnimation;
    }

    private void DisplayKeyAnimation(string keyName)
    {
        spriteRenderer.enabled = true;
        animator.enabled = true;
        animator.Play(keyName);
    }

    private void StopPlayingAnimation()
    {
        spriteRenderer.enabled = false;
        animator.enabled = false;

    }

    public void PlaySpaceBarAnimation()
    {
        DisplayKeyAnimation("SpaceBar");
    }

    public void PlayEAnimation(Weapon weapon)
    {
        DisplayKeyAnimation("E");
    }
}
