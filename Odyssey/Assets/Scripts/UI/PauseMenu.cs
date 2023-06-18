using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu instance;
    [SerializeField] GameObject pauseMenu;
    public bool isPaused = false;
    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pauseMenu.SetActive(isPaused);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        // newly added to fix the appearance of the pause
        // menu when the game is started
        pauseMenu.SetActive(isPaused);
    }
}
