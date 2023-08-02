using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents : MonoBehaviour
{
    // character hurt and amount of damage
    public static UnityAction<GameObject, int> CharacterHurt;
    // character healed and amount healed
    public static UnityAction<GameObject, int> CharacterHeal;

    public static UnityAction<GameObject, string> GenerateFeedback;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
