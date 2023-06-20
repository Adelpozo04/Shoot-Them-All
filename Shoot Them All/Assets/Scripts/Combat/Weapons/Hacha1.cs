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
    private int _currentBullets;
    private GameObject _bullet;
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
        
        if (_currentBullets == 1)
        {
            base.AtaqueSecundario();

            GameObject _bullet = _disparoParabolico.PerfomShoot(_bulletPrefab, _playerFather, AngleToDirection(), _spawnpointBullet.position, ref _currentBullets, ref _elapsedTime, _force);
            _bullet.GetComponent<FollowWhoThrow>().RegisterPlayerWhoThrow(_playerFather.gameObject);
        }

    }

    public void Recargar()
    {
        //Activar animacion de recarga
        _currentBullets = _maxBullets;
    }

    private void ReturnBullet()
    {
        _bullet.GetComponent<FollowWhoThrow>().FollowPlayerWhoThrow();
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentBullets = _maxBullets;
    }

    // Update is called once per frame
    void Update()
    {
        if(_currentBullets == 0) 
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
