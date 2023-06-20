using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

/// <summary>
/// Managea animadores que animan
/// </summary>
public class AnimatorsManager : MonoBehaviour
{
    [SerializeField]
    private Animator _bodyAnimator;
    [SerializeField]
    private Animator _weaponAnimator;

    private int Floor = Animator.StringToHash("Floor");
    private int Walking = Animator.StringToHash("Walking");
    private int JumpingBend = Animator.StringToHash("JumpingBend");
    private int NJump = Animator.StringToHash("NJump");
    private int Shoot = Animator.StringToHash("Shoot");
    private int WeaponType = Animator.StringToHash("WeaponType");
    private int Shoot1 = Animator.StringToHash("Shoot1");
    private int Shoot2 = Animator.StringToHash("Shoot2");
    private int DirX = Animator.StringToHash("DirX");
    private int DirY = Animator.StringToHash("DirY");

    #region SetParamaters

    public void ChangeFloor(bool floor)
    {
        _bodyAnimator.SetBool(Floor, floor);
    }
    public void ChangeWalking(bool walking)
    {
        _bodyAnimator.SetBool(Walking, walking);
    }
    public void ChangeJumpingBend(float bend)
    {
        _bodyAnimator.SetFloat(JumpingBend, bend);
    }
    public void ChangeNJump(int n)
    {
        _bodyAnimator.SetFloat(NJump, n);
    }
    public void TriggerShoot()
    {
        _bodyAnimator.SetTrigger(Shoot);
    }
    public void ChangeWeaponType(float value)
    {
        _weaponAnimator.SetFloat(WeaponType, value);
    }
    public void TriggerShoot1()
    {
        _weaponAnimator.SetTrigger(Shoot1);
    }
    public void TriggerShoot2()
    {
        _weaponAnimator.SetTrigger(Shoot2);
    }
    public void ChangeDirX(float value)
    {
        _bodyAnimator.SetFloat(DirX, 10 *value);
    }
    public void ChangeDirY(float value)
    {
        _bodyAnimator.SetFloat(DirY, 10 * value);
    }
    #endregion
    public void SetInflence(float weigth)
    {
        _bodyAnimator.SetLayerWeight(_bodyAnimator.GetLayerIndex("Direccion Arma"), weigth);
    }
}
