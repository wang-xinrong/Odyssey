using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PausePanelPatch : MonoBehaviour
{
    public void LoadSceneWithDataUpdate(string sceneName)
    {
        // this line might cause transition of scenes to invoke
        // null reference exception under development / testing setting
        // as data carrier might not exist in the corresponding scenes.
        // However, in actual game setting, the data carrier would be
        // present.
        DataCarrier.Instance.UpdateInventoryData();
        SceneManager.LoadScene(sceneName);
    }


    public void LoadSceneWithoutDataUpdate(string sceneName)
    {
        // this line might cause transition of scenes to invoke
        // null reference exception under development / testing setting
        // as data carrier might not exist in the corresponding scenes.
        // However, in actual game setting, the data carrier would be
        // present.
        DataCarrier.Instance.ClearInventoryData();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadSceneFromStartMenu(string sceneName)
    {
        // this line might cause transition of scenes to invoke
        // null reference exception under development / testing setting
        // as data carrier might not exist in the corresponding scenes.
        // However, in actual game setting, the data carrier would be
        // present.
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
