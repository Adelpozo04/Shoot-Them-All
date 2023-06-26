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

    [Tooltip("Tiempo que debe pasar entre cada ataque")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Fuerza con la que sale la bala ")]
    [SerializeField] private float _force;
    #endregion


    #region properties

    private float _elapsedTime;
    #endregion

    #region methods

    public override void AtaquePrincipal()          // Ataque Básico
    {

    }

    public override void AtaqueSecundario()         // Stun
    {
        if (ataqueMelee.AttackCondition())
        {
            base.AtaqueSecundario();
            meleeDamage.Stun = true;
            meleeDamage.SetDamage(_damagePri);
            ataqueMelee.PerformAttack();
            meleeDamage.Stun = false;
        }

        // Aumentar Porcentaje

        // Knockback del último golpe

    }


    #endregion
    public bool AttackCondition()
    {
        return _elapsedTime > _enfriamiento;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        ataqueMelee = GetComponent<AtaqueMelee>();
        meleeDamage = GetComponent<MeleeDamageComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime < _enfriamiento)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
}
