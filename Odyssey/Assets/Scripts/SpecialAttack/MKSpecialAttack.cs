using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSpecialAttack : MonoBehaviour
{
    public LayerMask AoE;
    public GameObject _playerController;
    public IEnumerator InitiateSpecialAttack()
    {
        GameObject Shockwave = this.transform.Find("Shockwave").gameObject;
        Shockwave.SetActive(true);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_playerController.transform.position ,5f, AoE);
        yield return new WaitForSeconds(1.0f);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Damageable>().OnHurt(30, Vector2.zero);
        }
        yield return new WaitForSeconds(1.0f);
        Shockwave.SetActive(true);
    }
}
