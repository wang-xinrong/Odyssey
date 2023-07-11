using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string destination;
    private Room room;

    private void Start()
    {
        room = GetComponentInParent<Room>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!room.Cleared) return;

        if (other.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(destination);
        }
    }
}
