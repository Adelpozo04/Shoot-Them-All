using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Hacha1 : AttackGeneral
{
    #region parameters

    [Tooltip("Fuerza con la que sale disparado el arma")]
    [SerializeField] private float _force;

    [Tooltip("Tiempo que tarda en recargar el arma si esta se sale del mapa")]
    [SerializeField] private float _tiempoRegresoFueraLimites;

    #endregion

    #region references
    private AtaqueMelee ataqueMelee;
    private Transform _spawnpointBullet;

    #endregion

    #region properties
    [SerializeField]
    private float _enfriamiento;
    private float _elapsedTime;
    private int _currentBullets;
    private float _returnTime;
    private GameObject [] _bullets;
    private DisparoParabolico _disparoParabolico;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _maxBullets;

    #endregion

    #region methods
    public override void AtaquePrincipal()
    {
        if (ataqueMelee.AttackCondition() && ShootCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            ataqueMelee.PerformAttack();
        }          
    }

    public override void AtaqueSecundario()
    {
        
        if (ShootCondition() && !WeaponWallDetector() && !ataqueMelee.IsAttacking)
        {
            base.AtaqueSecundario();
            _bullets[_maxBullets - _currentBullets] = 
                _disparoParabolico.PerfomShoot(_bulletPrefab, _playerPoints, 
                AngleToDirection(), _spawnpointBullet.position, ref _currentBullets, ref _elapsedTime, _force);
            _bullets[_maxBullets - (_currentBullets + 1)].GetComponent<FollowWhoThrow>().RegisterPlayerWhoThrow(GetPlayer()); //Cambiar por padre
                                                                                                                              //Es un poco chapuza lo de +1 pero sino habria que hacer contador individual aparte
            _bullets[_maxBullets - (_currentBullets + 1)].GetComponent<Choque>().SetDamage(_damageSec);
        }
    }

    public void Recargar()
    {
        if (_currentBullets < _maxBullets)
        {
            _currentBullets++;
        }
    }

    private void RecargaBalaFueraLimites()
    {
        for (int i = 0; i < _maxBullets; i++)
        {
            if (_bullets[i] == null)
            {
                Recargar();
                _returnTime = 0;
            }
        }        
    }

    private bool ShootCondition()
    {
        return _currentBullets > 0 && _elapsedTime > _enfriamiento;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _currentBullets = _maxBullets;
        _bullets = new GameObject[_maxBullets];
        _disparoParabolico = GetComponent<DisparoParabolico>();
        _elapsedTime = _enfriamiento + 1;
        _returnTime = 0;
        ataqueMelee = GetComponent<AtaqueMelee>();
        GetComponent<Choque>().SetDamage(_damagePri);
        _spawnpointBullet = transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime < _enfriamiento)
        {
            _elapsedTime += Time.deltaTime;
        }


        if(_currentBullets == 0) 
        {
            if (_returnTime < _tiempoRegresoFueraLimites)
            {
                _returnTime += Time.deltaTime;
            }
            else
            {
                RecargaBalaFueraLimites();
            }
        }
    }
}
