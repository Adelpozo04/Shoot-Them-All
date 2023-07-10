using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cuchillo : AttackGeneral
{
    private MeleeDamageComponent _meleeDamage;
    private AtaqueMelee _ataqueMelee;
    private DisparoRectoBeheaviour _disparoRecto;

    #region Parameters
    [Tooltip("Velocidad a la que sale la bala")]
    [SerializeField] private float _speed;
    [Tooltip("Tiempo que tarda en recargar el arma si esta se sale del mapa")]
    [SerializeField] private float _tiempoRegresoFueraLimites;
    [Tooltip("Prefab de la bala a disparar")]
    [SerializeField] private GameObject _bulletPrefab;
    [Tooltip("Máximo de balas del arma")]
    [SerializeField] private int _maxBullets;

    #endregion
    #region properties
    private int _currentBullets;
    private float _returnTime;
    private GameObject[] _bullets;
    #endregion
    #region Methods

    public override void AtaquePrincipal()
    {
        if (PriTimeCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            _timerPri = 0;
            _ataqueMelee.PerformAttack();
        }
    }
    public override void AtaqueSecundario()
    {
        if (SecTimeCondition() && !WeaponWallDetector() && !_ataqueMelee.IsAttacking && _currentBullets > 0)
        {
            base.AtaqueSecundario();
            _bullets[_maxBullets - _currentBullets] =
                _disparoRecto.PerfomShoot(_bulletPrefab, _playerPoints,
                AngleToDirection(), transform.position, ref _timerSec, _speed);
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
        _disparoRecto = GetComponent<DisparoRectoBeheaviour>();
        _returnTime = 0;
        _ataqueMelee = GetComponent<AtaqueMelee>();
        _coolDownPri += _ataqueMelee.HitTime;
        GetComponent<Choque>().SetDamage(_damagePri);
    }

    // Update is called once per frame
    void Update()
    {
        RunTimerPri();
        RunTimerSec();


        if (_currentBullets == 0)
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
