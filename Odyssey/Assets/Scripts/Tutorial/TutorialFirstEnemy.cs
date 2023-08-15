using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialFirstEnemy : MonoBehaviour
{
    public GameObject SecondEnemy;
    // Start is called before the first frame update
    void Start()
    {
        SecondEnemy.SetActive(false);
    }
}
