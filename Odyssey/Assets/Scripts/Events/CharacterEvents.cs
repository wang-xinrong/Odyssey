using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    // character hurt and amount of damage
    public static UnityEvent<GameObject, int> CharacterHurt;
    // character healed and amount healed
    public static UnityEvent<GameObject, int> CharacterHeal;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
