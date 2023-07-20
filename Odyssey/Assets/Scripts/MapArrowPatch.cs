using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapArrowPatch : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnEnable()
    {
        WallCheck();
    }

    // Update is called once per frame
    private void WallCheck()
    {
        Collider2D collider = Physics2D.OverlapBox(transform.position, Vector2.one, 0);

        if (collider && collider.tag == "Door")
        {
            transform.parent.gameObject.SetActive(false);
        }
    }
}
