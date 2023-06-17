using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzaPiñatas : AttackGeneral
{

    #region references
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private DisparoParabolico _disparoParabolico;
    #endregion

    #region parameters

    [Tooltip("Número de balas máximas del cargador")]
    [SerializeField] private int _maxBalasInScreen;

    [Tooltip("Tiempo que debe pasar entre un disparo y otro")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Incia si el arma tiene municion infinita")]
    [SerializeField]
    private bool _infiniteAmo;

    //[SerializeField]
    //private PointsComponent _playerFather;

    [Tooltip("Fuerza con la que sale la bala ")]
    [SerializeField] private float _force;
    #endregion


    #region properties

    private GameObject bullet;
    private int _currentBullets;
    private float _elapsedTime;
    private Queue<ExplotionIgnition> _bullets;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        if(ShootCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            if (_bullets.Count >= _maxBalasInScreen)
            {
                _bullets.Dequeue().Explote();
            }
            GameObject proyectile = _disparoParabolico.PerfomShoot(_bulletPrefab, _playerFather, _raycastDir,
                _bulletSpawnPoint.position, ref _currentBullets, ref _elapsedTime, _force);
            _bullets.Enqueue(proyectile.GetComponent<ExplotionIgnition>());
        }
        //if(_canShot && WeaponWallDetector())
        //{
        //    if (_currentBullets < _maxBalasInScreen)
        //    {
        //        Debug.Log("Entro");
        //        base.AtaquePrincipal();

        //        bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        //        bullet.GetComponent<ExplotionIgnition>().SetPlayerFather(_playerFather);
        //        bullet.transform.rotation = transform.rotation;

        //        bullet.GetComponent<Rigidbody2D>().AddForce(AngleToDirection() * _speed,ForceMode2D.Impulse);
        //        //bullet.GetComponent<Rigidbody2D>().velocity = AngleToDirection() * _speed;

        //        _shots[_currentBullets % _maxBalasInScreen] = bullet;
        //        _currentBullets++;
        //        _canShot = false;
        //        _elapsedTime = 0;
        //    }
        //    else
        //    {
        //        if (_shots[_nextExplotion % _maxBalasInScreen] != null)
        //        {
        //            _shots[_nextExplotion % _maxBalasInScreen].GetComponent<ExplotionIgnition>().Explote();
        //        }

        //        _nextExplotion++;

        //        bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        //        bullet.GetComponent<ExplotionIgnition>().SetPlayerFather(_playerFather);
        //        bullet.transform.rotation = transform.rotation;
        //        bullet.GetComponent<Rigidbody2D>().AddForce(AngleToDirection() * _speed,ForceMode2D.Impulse);

        //        _shots[_currentBullets % _maxBalasInScreen] = bullet;
        //        _currentBullets++;
        //        _canShot = false;
        //        _elapsedTime = 0;
        //    }
        //}
        
    }

    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();
        while(_bullets.Count > 0)
        {
            _bullets.Dequeue().Explote();
        }
        Debug.Log(_bullets.Count + " Piñatas");
    }


    #endregion
    public bool ShootCondition()
    {
        return _elapsedTime > _enfriamiento && _infiniteAmo;
    }
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _bullets = new Queue<ExplotionIgnition>();
        //_bullets = new GameObject[_maxBalasInScreen];
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
