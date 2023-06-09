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
        if (PriTimeCondition() && !WeaponWallDetector() && _currentBullets > 0)
        {
            base.AtaquePrincipal();
            _timerPri = 0;
            ataqueMelee.PerformAttack();
        }          
    }

    public override void AtaqueSecundario()
    {
        
        if (SecTimeCondition() && !WeaponWallDetector() && !ataqueMelee.IsAttacking && _currentBullets > 0)
        {
            base.AtaqueSecundario();
            _bullets[_maxBullets - _currentBullets] = 
                _disparoParabolico.PerfomShoot(_bulletPrefab, _playerPoints, 
                AngleToDirection(), _spawnpointBullet.position, ref _timerSec, _force);
            _bullets[_maxBullets - _currentBullets].GetComponent<FollowWhoThrow>().RegisterPlayerWhoThrow(GetPlayer());
            _bullets[_maxBullets - _currentBullets].GetComponent<Choque>().SetDamage(_damageSec);
            _currentBullets--;
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
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _currentBullets = _maxBullets;
        _bullets = new GameObject[_maxBullets];
        _disparoParabolico = GetComponent<DisparoParabolico>();
        _returnTime = 0;
        ataqueMelee = GetComponent<AtaqueMelee>();
        _coolDownPri += ataqueMelee.HitTime;
        GetComponent<Choque>().SetDamage(_damagePri);
        _spawnpointBullet = transform;
    }

    // Update is called once per frame
    void Update()
    {
        RunTimerPri();
        RunTimerSec();


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
