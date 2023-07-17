using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInventoryRoom : Room
{
    public Item potion;
    public Damageable dmg;
    private bool hasRestored = false;
    private bool hasDamaged = false;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (hasDamaged)
        {
            return;
        }
        if (other.tag == "Player" && other.isActiveAndEnabled)
        {
            hasDamaged = true;
            Debug.LogWarning(other.GetComponent<Collider>());
            dmg = other.GetComponent<Damageable>();
            dmg.OnHurt(20, Vector2.zero);
            InventoryManager.Instance.OverwriteSlotItem(potion, 1, 6);
            NoOfEnemiesAlive++;
            RoomController.instance.OnPlayerEnterRoom(this);
        }
    }
    void Update()
    {
        if (hasRestored) return;
        if (!Reached) return;
        if (Cleared) return;

        if (Reached && NoOfEnemiesAlive > 0)
        {
            foreach (Door d in doors)
            {
                d.LockDoor(true);
            }
        }

        if (dmg.Health == dmg.MaxHealth)
        {
            NoOfEnemiesAlive--;
            hasRestored = true;
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
