using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacha : AttackGeneral
{
    #region references
    [SerializeField]
    AtaqueMelee ataqueMelee;
    #endregion

    #region methods
    public override void AtaquePrincipal()
    {
        if (ataqueMelee.AttackCondition())
        {
            ataqueMelee.PerformAttack();
            base.AtaquePrincipal();
        }       
        //queso      
    }

    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
