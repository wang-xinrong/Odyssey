using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// abstract class containing the fileds that all special attacks should have
public abstract class SpecialAttack : MonoBehaviour
{
    public LayerMask AoE;
    public GameObject _playerController;
    public int specialAttackCost = 0; 
    public abstract IEnumerator InitiateSpecialAttack();
}
