using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(BoxCollider2D))]
public class Door : MonoBehaviour
{
    public GameObject Arrow;

    public enum DoorType
    {
        left, right, top, bottom
    }
    public DoorType doorType;
    private BoxCollider2D boxCollider2D;
    private TilemapRenderer tilemapRenderer;

    private bool _exist = true;
    private bool _isLocked = false;

    private void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        tilemapRenderer = GetComponentInChildren<TilemapRenderer>();
    }

    public void HasDoor(bool value)
    {
        // if the door doesnt exist, it would be permanently locked
        LockDoor(!value);
        _exist = value;
    }

    public void LockDoor(bool value)
    {

        if (!_exist) return;
        // the line below is affecting the behaviour of arrow
        // activation code, thus commented it out.
        //if (value == _isLocked) return;

        if (value == false)
        {
            boxCollider2D.enabled = false;
            tilemapRenderer.enabled = false;
            Arrow.SetActive(true);
            _isLocked = false;
        }

        if (value == true)
        {
            boxCollider2D.enabled = true;
            tilemapRenderer.enabled = true;
            Arrow.SetActive(false);
            _isLocked = true;
        }
    }
}
