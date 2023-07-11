using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        // this line might cause transition of scenes to invoke
        // null reference exception under development / testing setting
        // as data carrier might not exist in the corresponding scenes.
        // However, in actual game setting, the data carrier would be
        // present.
        if (gameObject.scene.name != "StartMenu") DataCarrier.Instance.UpdateInventoryData();
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
