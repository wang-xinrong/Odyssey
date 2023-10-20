using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BuildingActivator : MonoBehaviour
{
    public GameObject BuildingPanel;
    public bool IsNearBuilding = false;
    public bool IsBuildingPanelOpen = false;
    public AudioClip EntryClip;

    private void Awake()
    {
        BuildingPanel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearBuilding = true;

        collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>()
            .PlayEAnimation(null);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        IsNearBuilding = false;

        collision.transform.parent
            .GetComponentInChildren<KeyPressPromptPositionUpdate>()
            .StopPlayingAnimation();
    }

    public void OnOpenBuildingPanel(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (GameStatus.Instance.IsGamePaused && !IsBuildingPanelOpen) return;


            //temporary fix of the issue that if the player walks
            // away from the building while it is open, the building
            // would not be able to be closed. the better solution would
            // be to pause the game.

            if (!IsNearBuilding) return;
            BuildingPanel.SetActive(!IsBuildingPanelOpen);
            IsBuildingPanelOpen = !IsBuildingPanelOpen;

            if (IsBuildingPanelOpen) AudioManager.Instance.PlaySound(EntryClip);

            /*
            if (!IsBuildingPanelOpen)
            {
                if (!IsNearBuilding) return;
                BuildingPanel.SetActive(!IsBuildingPanelOpen);
                IsBuildingPanelOpen = !IsBuildingPanelOpen;
                return;
            }

            if (IsBuildingPanelOpen)
            {
                //if (!IsNearBuilding) return;
                BuildingPanel.SetActive(!IsBuildingPanelOpen);
                IsBuildingPanelOpen = !IsBuildingPanelOpen;
                return;
            }
            */
        }
    }
}