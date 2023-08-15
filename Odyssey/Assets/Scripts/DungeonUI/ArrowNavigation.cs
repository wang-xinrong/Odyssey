using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowNavigation : MonoBehaviour
{
    private int page = 1;
    public int NumberOfChar = 2;
    public GameObject Card1;
    public GameObject Card2;

    private float GetOffset()
    {
        return Card2.transform.position.x - Card1.transform.position.x;
        //return ScreenDimension.GetStatsPanelArrowOffset();
    }

    public void MoveLeft()
    {
        Vector3 position = transform.position;
        if (page == 1)
        {
            MoveRight();
            return;
        }

        page--;
        gameObject.transform.position = new Vector2(
            position.x + GetOffset()
            , position.y);
        

        //GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, Offset.x, 2560);
    }

    public void MoveRight()
    {
        Vector3 position = transform.position;
        if (page == NumberOfChar)
        {
            MoveLeft();
            return;
        }

        page++;
        gameObject.transform.position = new Vector2(
            position.x - GetOffset()
            , position.y);
        

        //GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, -1 * Offset.x, 2560);
    }

    public void OnEnable()
    {
        if (page == 2) MoveLeft();
    }
}
