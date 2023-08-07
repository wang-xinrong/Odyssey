using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialDashRoom : Room
{
    private Dashed dashed;

    private new void Start()
    {
        base.Start();
        NoOfEnemiesAlive++;
        dashed = GameObject.Find("Player").GetComponent<Dashed>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Player" || !other.isActiveAndEnabled)
        {
            return;
        }
        RoomController.instance.OnPlayerEnterRoom(this);
    }


    void Update()
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

        if (dashed.HasDashed)
        {
            NoOfEnemiesAlive--;
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
}
