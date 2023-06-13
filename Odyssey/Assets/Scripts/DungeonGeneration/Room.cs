using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public int width;
    public int height;
    public int x;
    public int y;
    public Grid grid;
    public GameObject door;

    public Door leftDoor;
    public Door rightDoor;
    public Door topDoor;
    public Door bottomDoor;

    public List<Door> doors = new List<Door>();

    public void UpdatePosition(Vector3 newPos)
    {
        transform.position = newPos;
        grid.transform.position = newPos;
        door.transform.position += newPos;
    }
    // Start is called before the first frame update
    void Start()
    {
        // ensure starting in correct scene
        if (RoomController.instance == null)
        {
            return;
        }
        Door[] ds = door.GetComponentsInChildren<Door>();
        foreach(Door d in ds)
        {
            doors.Add(d);
            switch(d.doorType)
            {
                case Door.DoorType.right:
                rightDoor = d;
                break;

                case Door.DoorType.left:
                leftDoor = d;
                break;

                case Door.DoorType.top:
                topDoor = d;
                break;

                case Door.DoorType.bottom:
                bottomDoor = d;
                break;
            }
        }
        RoomController.instance.RegisterRoom(this);
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch(door.doorType)
            {
                case Door.DoorType.right:
                    if (GetRight() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.left:
                    if (GetLeft() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.top:
                    if (GetTop() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                break;
                case Door.DoorType.bottom:
                    if (GetBottom() == null)
                    {
                        door.gameObject.SetActive(false);
                    }
                break;
            }
        }
    }
    public Room GetRight()
    {
        return RoomController.instance.FindRoom(x + 1, y);
    }
    public Room GetLeft()
    {
        return RoomController.instance.FindRoom(x - 1, y);
    }
    public Room GetTop()
    {
        return RoomController.instance.FindRoom(x, y + 1);
    }
    public Room GetBottom()
    {
        return RoomController.instance.FindRoom(x, y - 1);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 0));
    }

    public Vector3 GetRoomCenter()
    {
        return new Vector3(x * width, y * height);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        print("occurred");
        RoomController.instance.OnPlayerEnterRoom(this);
    }
}
