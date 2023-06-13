using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomInfo
{
    public string name;
    public int x;
    public int y;
}

public class RoomController : MonoBehaviour
{
    public static RoomController instance;
    public string currentChapterName = "Chapter1";

    RoomInfo currentLoadedRoomData;

    Room currRoom;

    Queue<RoomInfo> loadRoomQueue = new Queue<RoomInfo>();

    public List<Room> loadedRooms = new List<Room>();

    bool isLoadingRoom = false;
    bool hasRemovedWalls = false;

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
            if (!hasRemovedWalls)
            {
                foreach (Room room in loadedRooms)
                {
                    room.RemoveUnconnectedDoors();
                }
                hasRemovedWalls = true;
            }
            return;
        }

        currentLoadedRoomData = loadRoomQueue.Dequeue();
        isLoadingRoom = true;

        StartCoroutine(LoadRoomRoutine(currentLoadedRoomData));
    }

    public void LoadRoom(string roomName, int roomX, int roomY)
    {
        if (DoesRoomExist(roomX, roomY))
        {
            return;
        }
        RoomInfo newRoomData = new RoomInfo();
        newRoomData.name = roomName;
        newRoomData.x = roomX;
        newRoomData.y = roomY;
        loadRoomQueue.Enqueue(newRoomData);
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

    public bool DoesRoomExist(int x, int y)
    {
        return loadedRooms.Find(room => room.x == x && room.y == y) != null;
    }

    public Room FindRoom(int x, int y)
    {
        return loadedRooms.Find(room => room.x == x && room.y == y);
    }

    public void OnPlayerEnterRoom(Room room)
    {
        CameraController.instance.currRoom = room;
        currRoom = room;
    }
}
