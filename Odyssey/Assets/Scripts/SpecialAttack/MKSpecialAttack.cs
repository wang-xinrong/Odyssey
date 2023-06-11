using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKSpecialAttack : MonoBehaviour
{
    public LayerMask AoE;
    public GameObject _playerController;
    public MainPlayerController _mainPlayerController;
    public IEnumerator InitiateSpecialAttack()
    {
        GameObject Shockwave = this.transform.Find("Shockwave").gameObject;
        Shockwave.SetActive(true);
        _mainPlayerController.decrementSPBy(50);
        Collider2D[] colliders = Physics2D.OverlapCircleAll(_playerController.transform.position ,5f, AoE);
        yield return new WaitForSeconds(0.5f);
        foreach (Collider2D collider in colliders)
        {
            collider.GetComponent<Damageable>().OnHurt(30, Vector2.zero);
        }
        yield return new WaitForSeconds(1.0f);
        Shockwave.SetActive(false);
    }
}
