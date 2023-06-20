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
        if (ataqueMelee.AttackCondition())
        {
            meleeDamage.SetDamage(_damagePri);
            base.AtaquePrincipal();
            ataqueMelee.PerformAttack();
        }       
        //queso      
    }

    public override void AtaqueSecundario()
    {
        if (spinAttack.AttackCondition())
        {
            meleeDamage.SetDamage(_damageSec);
            base.AtaqueSecundario();
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
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
