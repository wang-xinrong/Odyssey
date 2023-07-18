using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeHitBoxes : MonoBehaviour
{
    public Transform UpHitBox;
    public Transform DownHitBox;
    public Transform LeftHitBox;
    public Transform RightHitBox;
    private Transform[] _hitBoxes = new Transform[4];
    public PlayerController _playerController;

    public void SetUpHitBoxes()
    {
        _hitBoxes[0] = UpHitBox;
        _hitBoxes[1] = DownHitBox;
        _hitBoxes[2] = LeftHitBox;
        _hitBoxes[3] = RightHitBox;
    }

    public Transform GetHitBox(Vector2 directionVector)
    {
        return _hitBoxes[Directions.GetDirectionIndex(directionVector)];
    }

    private void Awake()
    {
        _playerController = gameObject.GetComponent<PlayerController>();
        SetUpHitBoxes();
    }

    public void InitiateAttack(int indexInCombo)
    {
        WeaponAttack currAttack = _playerController.weapon.combos[indexInCombo];
        GameObject hitBox = GetHitBox(GetDirectionFacing()).gameObject;
        hitBox.SetActive(true);
        hitBox.GetComponent<Attack>().AttackDamage = currAttack.damage;
    }

    public void EndAttack()
    {
        foreach (Transform _hitBox in _hitBoxes)
        {
            _hitBox.gameObject.SetActive(false);
        }
    }

    public Vector2 GetDirectionFacing()
    {
        return _playerController.GetDirectionFacing();
    }
}
