using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpecialAttack : MonoBehaviour
{
    public LayerMask AoE;
    public GameObject _playerController;
    public MainPlayerController _mainPlayerController;
    public int specialAttackCost = 0; 
    public abstract IEnumerator InitiateSpecialAttack();
}
