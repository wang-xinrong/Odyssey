using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSpecialAttack : SpecialAttack
{
    public int SpecialAttackDamage = 70;

    public override IEnumerator InitiateSpecialAttack()
    {
        GameObject Shockwave = this.transform.Find("Shockwave").gameObject;
        Shockwave.SetActive(true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_playerController.transform.position, 8f, AoE);
        yield return new WaitForSeconds(0.5f);
        foreach (Collider2D collider in colliders)
        {
            Debug.Log(collider);
            collider.GetComponent<Damageable>().OnHurt(SpecialAttackDamage
                , Vector2.zero);
        }
        yield return new WaitForSeconds(1.0f);
        Shockwave.SetActive(false);
    }
}
