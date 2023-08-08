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
    public Dictionary<Direction, bool> hasDoors;
    private EnemyActivation[] _enemyActivation;
    public int NoOfEnemiesAlive;

    public List<Door> doors = new List<Door>();

    public void UpdatePosition(Vector3 newPos)
    {
        transform.position = newPos;
        // for tilemap to shift to correct position
        // grid.transform.position = newPos;
        // door.transform.position += newPos;
    }
    // Start is called before the first frame update
    protected void Start()
    {
        // ensure starting in correct scene
        if (RoomController.instance == null)
        {
            return;
        }
        Door[] ds = door.GetComponentsInChildren<Door>();
        foreach (Door d in ds)
        {
            doors.Add(d);
        }
        RoomController.instance.RegisterRoom(this);
        RemoveUnconnectedDoors();
    }

    public bool Reached = false;
    public bool Cleared = false;

    private void Update()
    {
        if (!Reached) return;
        if (Cleared) return;

        if (Reached && NoOfEnemiesAlive > 0)
        {
            foreach (Door d in doors)
            {
                d.LockDoor(true);
            }
        }

        if (Reached && NoOfEnemiesAlive == 0)
        {
            foreach (Door d in doors)
            {
                d.LockDoor(false);
            }

            Cleared = true;
        }
    }

    public void RemoveUnconnectedDoors()
    {
        foreach (Door door in doors)
        {
            switch (door.doorType)
            {
                case Door.DoorType.right:
                    door.HasDoor(hasDoors[Direction.right]); //door.gameObject.SetActive(hasDoors[Direction.right]);
                    break;
                case Door.DoorType.left:
                    door.HasDoor(hasDoors[Direction.left]);//door.gameObject.SetActive(hasDoors[Direction.left]);
                    break;
                case Door.DoorType.top:
                    door.HasDoor(hasDoors[Direction.up]); //door.gameObject.SetActive(hasDoors[Direction.up]);
                    break;
                case Door.DoorType.bottom:
                    door.HasDoor(hasDoors[Direction.down]);//door.gameObject.SetActive(hasDoors[Direction.down]);
                    break;
            }
        }
    }

    private bool HasFourAdjacentRooms()
    {
        return GetRight() != null && GetLeft() != null && GetTop() != null && GetBottom() != null;
    }

    public void ConsiderAddingThirdDoor()
    {
        if (!HasFourAdjacentRooms())
        {
            return;
        }
        System.Random rand = new System.Random();
        float probability = (float)rand.NextDouble();
        if (probability < 0.5f)
        {
            return;
        }
        foreach (Direction direction in hasDoors.Keys)
        {
            if (!hasDoors[direction])
            {
                hasDoors[direction] = true;
                Room other = this;
                Direction dir = Direction.unset;
                switch (direction)
                {
                    case Direction.up:
                        other = GetTop();
                        dir = Direction.down;
                        break;

                    case Direction.down:
                        other = GetBottom();
                        dir = Direction.up;
                        break;

                    case Direction.left:
                        other = GetLeft();
                        dir = Direction.right;
                        break;

                    case Direction.right:
                        other = GetRight();
                        dir = Direction.left;
                        break;
                }
                other.hasDoors[dir] = true;
                other.RemoveUnconnectedDoors();
                break;
            }
        }
        RemoveUnconnectedDoors();
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
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            RoomController.instance.OnPlayerLeaveRoom(this);
        }
    }

    public void RegisterExistingEnemies()
    {
        _enemyActivation = GetComponentsInChildren<EnemyActivation>();
        NoOfEnemiesAlive = _enemyActivation.Length;
    }

    public void ActivateAllEnemies(bool value)
    {
        if (Reached) return;

        // no enemies
        if (_enemyActivation == null) return;

        foreach (EnemyActivation ea in _enemyActivation)
        {
            ea.Activate(value);
        }
    }

    public void SetUpEnemyReferenceToMovementPoints(List<Vector2> list)
    {
        foreach (EnemyActivation ea in _enemyActivation)
        {
            ea.SetUpMovementPoints(list);
        }
    }


    // temporary fix to the corner issue
    public void SetUpEnemyReferenceToRoomCorners(Transform bottomLeft, Transform topRight)
    {
        foreach (EnemyActivation ea in _enemyActivation)
        {
            ea.SetUpRoomCorners(bottomLeft, topRight);
        }
    }

    public void EnemyKilled()
    {
        NoOfEnemiesAlive--;
    }
}
