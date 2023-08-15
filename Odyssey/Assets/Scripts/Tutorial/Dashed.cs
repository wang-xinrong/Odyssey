using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dashed : MonoBehaviour
{
    public bool HasDashed = false;

    public void RegisterDash()
    {
        HasDashed = true;
    }
}
