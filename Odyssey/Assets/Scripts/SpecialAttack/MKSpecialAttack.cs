using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSpecialAttack : SpecialAttack
{
    private void Start()
    {
        UpdateSpecialAttackDamage(); 
    }

    public int SpecialAttackDamage = 70;

    public override IEnumerator InitiateSpecialAttack()
    {
        GameObject Shockwave = this.transform.Find("Shockwave").gameObject;
        // Shockwave.SetActive(true);
        // Shockwave.GetComponent<Animator>().Play("Shockwave");
        //Shockwave.SetActive(true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_playerController.transform.position, 8f, AoE);
        yield return new WaitForSeconds(0.5f);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Damageable>().OnHurt(SpecialAttackDamage
                , Vector2.zero);
        }
    }

    public void UpdateSpecialAttackDamage()
    {
        SpecialAttackDamage = StatsManager.Instance.GetCharacterSA(
            StatsManager.Character.MonkeyKing);
    }
}
