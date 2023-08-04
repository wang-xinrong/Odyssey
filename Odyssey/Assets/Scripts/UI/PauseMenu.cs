using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu Instance;
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;
    public void OnPause(InputAction.CallbackContext context)
    {
        if (GameStatus.Instance.IsGamePaused && !isPaused) return;

        if (context.started)
        {
            Pause();
            pauseMenu.SetActive(isPaused);
        }
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        // newly added to fix the appearance of the pause
        // menu when the game is started
        pauseMenu.SetActive(isPaused);
    }

    public void Pause()
    {
        isPaused = !isPaused;
        //Debug.Log("Game status is " + isPaused);
    }
}
