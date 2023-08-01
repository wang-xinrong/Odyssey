using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNavigation : MonoBehaviour
{
    public enum OS { Windows, MacOS}
    public OS CurrOS;
    private int windowsOffset = 1900;
    private int MacOSOffset = 975; 
    private int page = 1;
    public int NumberOfChar = 2;

    private int GetOffset()
    {
        if (CurrOS == OS.Windows) return windowsOffset;
        if (CurrOS == OS.MacOS) return MacOSOffset;
        return -1;
    }

    public void MoveLeft()
    {
        Vector3 position = transform.position;
        if (page == 1) return;

        page--;
        gameObject.transform.position = new Vector2(
            position.x + GetOffset()
            , position.y);
        

        //GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, Offset.x, 2560);
    }

    public void MoveRight()
    {
        Vector3 position = transform.position;
        if (page == NumberOfChar) return;

        page++;
        gameObject.transform.position = new Vector2(
            position.x - GetOffset()
            , position.y);
        

        //GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -1 * Offset.x, 2560);
    }
}
