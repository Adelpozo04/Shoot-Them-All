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
}
