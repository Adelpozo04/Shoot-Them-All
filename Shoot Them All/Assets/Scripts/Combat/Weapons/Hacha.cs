using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacha : AttackGeneral
{
    #region references
    AtaqueMelee ataqueMelee;
    SpinAttack spinAttack;
    MeleeDamageComponent meleeDamage;
    #endregion

    #region methods
    public override void AtaquePrincipal()
    {
        if (PriTimeCondition() && !spinAttack.IsAttacking())
        {
            base.AtaquePrincipal();
            meleeDamage.SetDamage(_damagePri);
            _timerPri = 0;
            ataqueMelee.PerformAttack();
        }       
        //queso      
    }

    public override void AtaqueSecundario()
    {
        if (spinAttack.AttackCondition() && !ataqueMelee.IsAttacking)
        {
            base.AtaqueSecundario();
            meleeDamage.SetDamage(_damageSec);
            _timerSec = 0;
            spinAttack.StartSpin();
        }
        
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ataqueMelee = GetComponent<AtaqueMelee>();
        spinAttack = GetComponent<SpinAttack>();
        meleeDamage = GetComponent<MeleeDamageComponent>();
        _coolDownPri += ataqueMelee.HitTime;
        StartMethod();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimerPri();
    }
}
