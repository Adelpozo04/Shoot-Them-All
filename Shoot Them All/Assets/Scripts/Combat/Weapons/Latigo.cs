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

    #endregion


    #region properties

    private float _elapsedTime;
    #endregion

    #region methods

    public override void AtaquePrincipal()          // Ataque Básico
    {
        if (ataqueMelee.AttackCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            meleeDamage.Stun = false;
            ataqueMelee.PerformAttack();
        }
    }

    public override void AtaqueSecundario()         // Stun
    {
        if (ataqueMelee.AttackCondition() && !WeaponWallDetector())
        {
            base.AtaqueSecundario();
            meleeDamage.Stun = true;
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
