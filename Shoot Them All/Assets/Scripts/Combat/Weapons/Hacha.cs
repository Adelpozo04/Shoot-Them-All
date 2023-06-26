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
        if (ataqueMelee.AttackCondition() && !spinAttack.IsAttacking())
        {
            base.AtaquePrincipal();
            meleeDamage.SetDamage(_damagePri);
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
        StartMethod();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
