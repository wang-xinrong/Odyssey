using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int x;
    public int y;
    public Dictionary<Direction, bool> hasDoors = new Dictionary<Direction, bool>{
        {Direction.up, false},
        {Direction.down, false},
        {Direction.left, false},
        {Direction.right, false}
    };
    public void addDoor(Direction dir)
    {
        hasDoors[dir] = true;
    }
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public string currentChapterName = "Chapter1";

    RoomInfo currentLoadedRoomData;

    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    public List<RoomInfo> allRooms = new List<RoomInfo>();

    public List<Vector2Int> endRoomPositions = new List<Vector2Int>();

    public string[] PossibleRooms =
    {
        "Intermediate"
       , "Intermediate2"
       , "Intermediate3"
       , "Intermediate3"
       , "Intermediate4"
       , "Intermediate5"
       , "Intermediate6"
       , "HPPickup"
    };


    bool isLoadingRoom = false;
    bool spawnedEndRoom = false;
    bool updatedRooms = false;

    void Awake()
    {
        instance = this;
    }


    void Update()
    {
        UpdateRoomQueue();
    }

    public void UpdateRoomQueue()
    {
        if (isLoadingRoom)
        {
            return;
        }

        if (loadRoomQueue.Count == 0)
        {
            if (!spawnedEndRoom)
            {
                StartCoroutine(spawnEndRoom());
            } else if (!updatedRooms)
            {
                foreach(Room room in loadedRooms)
                {
                    room.ConsiderAddingThirdDoor();
                }
                updatedRooms = true;
            }
            return;
        }

        currentLoadedRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadedRoomData));
    }

    IEnumerator spawnEndRoom()
    {
        spawnedEndRoom = true;
        yield return new WaitForSeconds(0.5f);
        if (loadRoomQueue.Count == 0)
        {
            Vector2Int chosenPosition = endRoomPositions[Random.Range(0, endRoomPositions.Count)];
            Room chosenRoom = FindRoom(chosenPosition.x, chosenPosition.y);
            Direction directionForDoor = Direction.unset;
            foreach (Direction dir in chosenRoom.hasDoors.Keys)
            {
                if (chosenRoom.hasDoors[dir])
                {
                    directionForDoor = dir;
                }
            }
            Destroy(chosenRoom.gameObject);
            loadedRooms.Remove(chosenRoom);
            RoomInfo roomInfo = allRooms.Find(room => room.x == chosenPosition.x && room.y == chosenPosition.y);
            if (roomInfo != null)
            {
                allRooms.Remove(roomInfo);
            }
            LoadRoom("End", chosenPosition.x, chosenPosition.y, directionForDoor);
        }
    }

    public void LoadRoom(string roomName, int roomX, int roomY, Direction direction)
    {
        if (HasPreviousCrawlerBeenTo(roomX, roomY))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = roomName;
        newRoomData.x = roomX;
        newRoomData.y = roomY;
        if (direction != Direction.unset)
        {
            newRoomData.addDoor(direction);
        }
        loadRoomQueue.Enqueue(newRoomData);
        allRooms.Add(newRoomData);
    }

    // Coroutine for loading room
    IEnumerator LoadRoomRoutine(RoomInfo info)
    {
        string roomName = currentChapterName + info.name;
        AsyncOperation loadRoom = SceneManager.LoadSceneAsync(roomName, LoadSceneMode.Additive);

        while (!loadRoom.isDone)
        {
            yield return null;
        }
    }

    // Adds room to main scene in the right coordinates
    public void RegisterRoom(Room room)
    {
        if (DoesRoomExist(currentLoadedRoomData.x, currentLoadedRoomData.y))
        {
            Destroy(room.gameObject);
        } else {
            // set the new room's position to be x widths from start room, y heights from start room
            Vector3 pos = new Vector3(currentLoadedRoomData.x * room.width, currentLoadedRoomData.y * room.height, 0);
            room.UpdatePosition(pos);
            room.x = currentLoadedRoomData.x;
            room.y = currentLoadedRoomData.y;
            room.name = currentChapterName + "-" + currentLoadedRoomData.name + " " + room.x + ", " + room.y;
            room.hasDoors = currentLoadedRoomData.hasDoors;
            room.transform.parent = transform;
            if (loadedRooms.Count == 0)
            {
                CameraController.instance.currRoom = room;
            }
            loadedRooms.Add(room);
        }

        // finish loading and add room to list of loaded rooms
        isLoadingRoom = false;

    }

    public bool HasPreviousCrawlerBeenTo(int x, int y)
    {
        return allRooms.Find(room => room.x == x && room.y == y) != null;
    }

    public RoomInfo FindPreviouslyEncounteredRoom(int x, int y)
    {
        return allRooms.Find(room => room.x == x && room.y == y);
    }

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(room => room.x == x && room.y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(room => room.x == x && room.y == y);
    }

    public string GetRandomRoomName()
    {
        return PossibleRooms[Random.Range(0, PossibleRooms.Length)];
    }

    public void OnPlayerEnterRoom(Room room)
    {
        StartCoroutine(EnterRoomCoroutine(room));
        // CameraController.instance.currRoom = room;
        // currRoom = room;
        // room.Reached = true;
        // room.ActivateAllEnemies(true);
    }

    IEnumerator EnterRoomCoroutine(Room room)
    {
        CameraController.instance.currRoom = room;
        room.ActivateAllEnemies(true);
        yield return new WaitForSeconds(.5f);
        Debug.Log("triggered");
        currRoom = room;
        room.Reached = true;

    }

    public void OnPlayerLeaveRoom(Room room)
    {
        room.ActivateAllEnemies(false);
    }
}
