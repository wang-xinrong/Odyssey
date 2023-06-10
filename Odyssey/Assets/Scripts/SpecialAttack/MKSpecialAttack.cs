using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSpecialAttack : MonoBehaviour
{
    public LayerMask AoE;
    public GameObject _playerController;
    public void InitiateSpecialAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_playerController.transform.position ,5f, AoE);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Damageable>().OnHurt(30, Vector2.zero);
        }
    }
}
