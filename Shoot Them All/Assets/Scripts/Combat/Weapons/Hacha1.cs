using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacha1 : AttackGeneral
{
    #region parameters

    [Tooltip("Fuerza con la que sale disparado el arma")]
    [SerializeField] private float _force;

    #endregion

    #region references
    [SerializeField]
    AtaqueMelee ataqueMelee;

    [SerializeField] private Transform _spawnpointBullet;

    #endregion

    #region properties

    [SerializeField] private float _tiempoRegreso;

    private float _elapsedTime;
    private bool _haveWeapon;
    private GameObject _bullet;
    [SerializeField] private DisparoParabolico _disparoParabolico;
    [SerializeField] private GameObject _bulletPrefab;

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
        

        if (_haveWeapon)
        {
            base.AtaqueSecundario();

            //GameObject _bullet = _disparoParabolico.PerfomShoot(_bulletPrefab, , AngleToDirection(), _spawnpointBullet, ,_elapsedTime, _force)
        }

    }

    private void ReturnProyectile()
    {

    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _haveWeapon = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_elapsedTime > _tiempoRegreso)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
}
