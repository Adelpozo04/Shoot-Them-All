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

    private bool _bulletFollowing = false;
    private float _elapsedTime;
    private int _currentBullets;
    private GameObject [] _bullets;
    [SerializeField] private DisparoParabolico _disparoParabolico;
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
        
        if (_currentBullets > 0)
        {
            base.AtaqueSecundario();

            _bullets[_maxBullets - _currentBullets] = _disparoParabolico.PerfomShoot(_bulletPrefab, _playerFather, AngleToDirection(), _spawnpointBullet.position, ref _currentBullets, ref _elapsedTime, _force);
            _bullets[_maxBullets - _currentBullets].GetComponent<FollowWhoThrow>().RegisterPlayerWhoThrow(GetPlayer()); //Cambiar por padre
            
        }

    }

    public void Recargar()
    {
        //Activar animacion de recarga
        _currentBullets = _maxBullets;
        _bulletFollowing = false;
    }

    private void ReturnBullet()
    {
        for (int i = 0; i < _maxBullets - _currentBullets; i++)
        {
            if(_bullets[_currentBullets] != null)
            {
                _bullets[_currentBullets].GetComponent<FollowWhoThrow>().FollowPlayerWhoThrow();
                _bulletFollowing = true;
            }
            else
            {
                Recargar();
            }
        }
        
        
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentBullets = _maxBullets;
        _bullets = new GameObject[_maxBullets];
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentBullets == 0 && !_bulletFollowing) 
        {
            if (_elapsedTime < _tiempoRegreso)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                ReturnBullet();
            }
        }
        
    }
}
