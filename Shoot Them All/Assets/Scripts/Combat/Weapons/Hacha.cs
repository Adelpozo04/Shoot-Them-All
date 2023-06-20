using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacha : AttackGeneral
{
    #region references
    [SerializeField]
    AtaqueMelee ataqueMelee;
    [SerializeField]
    SpinAttack spinAttack;
    #endregion

    #region methods
    public override void AtaquePrincipal()
    {
        if (ataqueMelee.AttackCondition())
        {
            base.AtaquePrincipal();
            ataqueMelee.PerformAttack();
        }       
        //queso      
    }

    public override void AtaqueSecundario()
    {
        if (spinAttack.AttackCondition())
        {
            base.AtaqueSecundario();
            spinAttack.StartSpin();
        }
        
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        ataqueMelee = GetComponent<AtaqueMelee>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
