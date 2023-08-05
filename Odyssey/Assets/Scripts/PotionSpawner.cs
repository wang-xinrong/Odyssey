using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionSpawner : ItemSpawner
{
    public GeneralPotionSpawner GeneralPotionSpawner;
    public bool IsBoss = false;

    public void SpawnPotion()
    {
        int maxHealth = GetComponent<Damageable>().MaxHealth;

        // the max health value is reduced by 1 to include the
        // boundary value in the lower bracket
        int potionTier = (int) ((maxHealth - 1) / 100);

        MS = GeneralPotionSpawner.PotionSpawners[potionTier];

        base.Awake();

        int numberOfSpawningRounds = IsBoss ? 3 : 1;

        for (int i = 0; i < numberOfSpawningRounds; i++)
        {

            base.Start();

            base.SpawnObject();
        }
    }
}
