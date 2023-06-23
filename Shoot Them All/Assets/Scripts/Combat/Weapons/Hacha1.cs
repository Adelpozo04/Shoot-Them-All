using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    AtaqueMelee ataqueMelee;

    [SerializeField] private Transform _spawnpointBullet;

    #endregion

    #region properties

    private bool _bulletFollowing = false;
    private float _elapsedTime;
    private int _currentBullets;
    private GameObject [] _bullets;
    private DisparoParabolico _disparoParabolico;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _maxBullets;

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
        
        if (_currentBullets > 0 && !WeaponWallDetector())
        {
            base.AtaqueSecundario();

            
            _bullets[_maxBullets - _currentBullets] = 
                _disparoParabolico.PerfomShoot(_bulletPrefab, _playerFather, 
                AngleToDirection(), _spawnpointBullet.position, ref _currentBullets, ref _elapsedTime, _force);
            _bullets[_maxBullets - (_currentBullets + 1)].GetComponent<FollowWhoThrow>().RegisterPlayerWhoThrow(GetPlayer()); //Cambiar por padre
                                                                                                                              //Es un poco chapuza lo de +1 pero sino habria que hacer contador individual aparte
            _bullets[_maxBullets - (_currentBullets + 1)].GetComponent<ChoqueArrojadiza>().SetDamage(_damageSec);
        }

    }

    public void Recargar()
    {
        //Activar animacion de recarga
        _currentBullets++;
        _bulletFollowing = false;
    }

    private void RecargaBalaFueraLimites()
    {
        for (int i = 0; i < _maxBullets - _currentBullets; i++)
        {
            if (_bullets[i] == null)
            {
                Recargar();
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
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentBullets == 0 && !_bulletFollowing) 
        {
            if (_elapsedTime < _tiempoRegresoFueraLimites)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                RecargaBalaFueraLimites();
            }
        }
    }
}
