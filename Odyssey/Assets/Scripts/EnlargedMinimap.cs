using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnlargedMinimap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Hide();
    }

    public void Hide()
    {
        Toggle(false);
    }

    public void Show()
    {
        Toggle(true);
    }

    private void Toggle(bool val)
    {
        gameObject.SetActive(val);
    }
}
