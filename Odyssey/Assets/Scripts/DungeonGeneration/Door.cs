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

    private bool _exist = true;
    private bool _isLocked = false;

    public void HasDoor(bool value)
    {
        // if the door doesnt exist, it would be permanently locked
        LockDoor(!value);
        _exist = value;
    }

    public void LockDoor(bool value)
    {
        if (!_exist) return;
        if (value == _isLocked) return;

        if (value == false)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponentInChildren<TilemapRenderer>().enabled = false;
            Arrow.SetActive(true);
            _isLocked = false;
        }

        if (value == true)
        {
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
            gameObject.GetComponentInChildren<TilemapRenderer>().enabled = true;
            Arrow.SetActive(false);
            _isLocked = true;
        }
    }
}
