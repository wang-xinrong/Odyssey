using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSoundEffects : MonoBehaviour
{
    public AudioClip PurchaseSound;
    public AudioClip CoinPickup;
    public AudioClip WeaponPickup;
    public AudioClip PotionPickup;
    public AudioClip PotionConsumption;
    //public AudioClip HPIncrease;
    //public AudioClip SPIncrease;
    //public AudioClip SpeedIncrease;
    public AudioClip DashSound;
    public AudioClip FoodConsumption;
    public AudioClip LiquidFoodConsumption;

    public static AudioClip SuccessfulPurchaseSound;
    public static AudioClip CoinPickupSound;
    public static AudioClip WeaponPickupSound;
    public static AudioClip PotionPickupSound;
    public static AudioClip PotionConsumptionSound;
    //public static AudioClip HPIncreaseSound;
    //public static AudioClip SPIncreaseSound;
    //public static AudioClip SpeedIncreaseSound;
    public static AudioClip DashSoundEffect;
    public static AudioClip FoodConsumptionSound;
    public static AudioClip LiquidFoodConsumptionSound;

    private void Awake()
    {
        SuccessfulPurchaseSound = PurchaseSound;
        CoinPickupSound = CoinPickup;
        WeaponPickupSound = WeaponPickup;
        PotionPickupSound = PotionPickup;
        PotionConsumptionSound = PotionConsumption;
        //HPIncreaseSound = HPIncrease;
        //SPIncreaseSound = SPIncrease;
        //SpeedIncreaseSound = SpeedIncrease;
        DashSoundEffect = DashSound;
        FoodConsumptionSound = FoodConsumption;
        LiquidFoodConsumptionSound = LiquidFoodConsumption;
    }

    public static void PlayPurchaseSound()
    {
        AudioManager.Instance.PlaySound(SuccessfulPurchaseSound);
    }

    public static void PlayCoinPickup()
    {
        AudioManager.Instance.PlaySound(CoinPickupSound);
    }

    public static void PlayWeaponPickup()
    {
        AudioManager.Instance.PlaySound(WeaponPickupSound);
    }

    public static void PlayPotionPickup()
    {
        AudioManager.Instance.PlaySound(PotionPickupSound);
    }

    public static void PlayPotionConsumption()
    {
        AudioManager.Instance.PlaySound(PotionConsumptionSound);
    }

    /*
    public static void PlayHPIncrease()
    {
        AudioManager.Instance.PlaySound(HPIncreaseSound);
    }

    public static void PlaySPIncrease()
    {
        AudioManager.Instance.PlaySound(SPIncreaseSound);
    }

    public static void PlaySpeedIncrease()
    {
        AudioManager.Instance.PlaySound(SpeedIncreaseSound);
    }
    */

    public static void PlayDashSound()
    {
        AudioManager.Instance.PlaySound(DashSoundEffect);
    }

    public static void PlayFoodConsumption()
    {
        AudioManager.Instance.PlaySound(FoodConsumptionSound);
    }

    public static void PlayLiquidFoodConsumption()
    {
        AudioManager.Instance.PlaySound(LiquidFoodConsumptionSound);
    }
}
