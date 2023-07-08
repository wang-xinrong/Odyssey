using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public Room currRoom;
    public float cameraShiftSpeed;

    // new
    public Transform Player;
    public enum Scene { NPCTown, Chapter}
    public Scene CurrentScene = Scene.NPCTown;



    void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        // new for npc town integration
        if (CurrentScene == Scene.NPCTown)
        {
            InNPCTown();
            return;
        }

        if (currRoom == null)
        {
            return;
        }

        Vector3 targetPos = GetCameraTargetPosition();

        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * cameraShiftSpeed);
    }

    Vector3 GetCameraTargetPosition()
    {
        if (currRoom == null)
        {
            return Vector3.zero;
        }

        Vector3 targetPos = currRoom.GetRoomCenter();
        targetPos.z = transform.position.z;

        return targetPos;
    }

    public bool IsSwitchingScene()
    {
        return !transform.position.Equals(GetCameraTargetPosition());
    }

    public void InNPCTown()
    {
        transform.position = new Vector3(
            Player.transform.position.x
            , Player.transform.position.y// + 2.5f
            , -10);
    }
}
