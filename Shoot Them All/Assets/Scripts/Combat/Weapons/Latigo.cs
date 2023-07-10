using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Latigo : AttackGeneral
{

    #region references   
    AtaqueMelee ataqueMelee;
    MeleeDamageComponent meleeDamage;
    #endregion

    #region parameters

    #endregion


    #region properties

    private float _elapsedTime;
    #endregion

    #region methods

    public override void AtaquePrincipal()          // Ataque Básico
    {
        if (PriTimeCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            meleeDamage.Stun = false;
            _timerPri = 0;
            ataqueMelee.PerformAttack();
        }
    }

    public override void AtaqueSecundario()         // Stun
    {
        if (SecTimeCondition() && !WeaponWallDetector())
        {
            base.AtaqueSecundario();
            meleeDamage.Stun = true;
            _timerSec = 0;
            ataqueMelee.PerformAttack();
        }
    }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        ataqueMelee = GetComponent<AtaqueMelee>();
        meleeDamage = GetComponent<MeleeDamageComponent>();
        meleeDamage.SetDamage(_damagePri);
        _coolDownPri += ataqueMelee.HitTime;
        _coolDownSec += ataqueMelee.HitTime;

    }

    // Update is called once per frame
    void Update()
    {
        RunTimerPri();
        RunTimerSec();
    }
}
