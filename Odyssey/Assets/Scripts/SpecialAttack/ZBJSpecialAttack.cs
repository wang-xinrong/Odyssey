using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZBJSpecialAttack : MonoBehaviour
{
    public LayerMask AoE;
    public float knockBackMultiplier = 5f;
    public GameObject _playerController;
    public IEnumerator InitiateSpecialAttack()
    {
        GameObject Shockwave = this.transform.Find("Shockwave").gameObject;
        Shockwave.SetActive(true);
        Vector2 charPosition = _playerController.transform.position;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(charPosition, 5f, AoE);
        yield return new WaitForSeconds(0.5f);
        foreach (Collider2D collider in colliders)
        {
            Vector2 enemyPosition = collider.gameObject.transform.position;
            Vector2 knockBack = enemyPosition - charPosition;
            knockBack.Normalize();
            collider.GetComponent<Damageable>().OnHurt(0, knockBack * knockBackMultiplier);
        }
        yield return new WaitForSeconds(1.0f);
        Shockwave.SetActive(false);
    }
}