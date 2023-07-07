using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : MonoBehaviour
{
    public string destination;
    void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(destination);
    }
}
