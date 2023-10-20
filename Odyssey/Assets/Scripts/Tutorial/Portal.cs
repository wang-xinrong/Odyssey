using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string destination;
    private Room room;
    public bool IsTutorial = false;

    private void Start()
    {
        room = GetComponentInParent<Room>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!room.Cleared) return;

        if (IsTutorial) DataCarrier.Instance.HasClearedTutorial = true;

        if (other.gameObject.tag == "Player")
        {
            DataCarrier.Instance.UpdateInventoryData();
            SceneManager.LoadScene(destination);
        }
    }
}