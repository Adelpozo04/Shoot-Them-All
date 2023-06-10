using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzaPiñatas : AttackGeneral
{

    #region parameters

    [Tooltip("Número de balas máximas del cargador")]
    [SerializeField] private int _maxBalasInScreen;

    [Tooltip("Tiempo que debe pasar entre un disparo y otro")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Distancia a la que se spawnea la bala (para comprobar que no atraviese paredes")]
    [SerializeField]
    private float _distancia;

    [SerializeField]
    private LayerMask _floorLayer;

    [SerializeField]
    private PointsComponent _playerFather;


    [Tooltip("Velocidad que lleva la bala ")]
    [SerializeField] private float _speed;
    #endregion

    #region references
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _bulletSpawnPoint;

    #endregion

    #region properties

    private GameObject bullet;
    private int _currentBullets;
    private float _elapsedTime;
    private int _nextExplotion;

    /// <summary>
    /// Indica si se puede disparar en relacion al ENFRIAMIENTO
    /// </summary>
    private bool _canShot = true;

    private GameObject[] _shots;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {

        RaycastHit2D raycast = Physics2D.Raycast(_playerFather.transform.position, AngleToDirection(), _distancia, _floorLayer);


        Debug.DrawRay(transform.position, new Vector3(AngleToDirection().x, AngleToDirection().y,0), Color.red, 5);

        if(_canShot && raycast.collider == null)
        {
            if (_currentBullets < _maxBalasInScreen)
            {
                Debug.Log("Entro");
                base.AtaquePrincipal();

                bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<ExplotionIgnition>().SetPlayerFather(_playerFather);
                bullet.transform.rotation = transform.rotation;

                bullet.GetComponent<Rigidbody2D>().AddForce(AngleToDirection() * _speed,ForceMode2D.Impulse);
                //bullet.GetComponent<Rigidbody2D>().velocity = AngleToDirection() * _speed;

                _shots[_currentBullets % _maxBalasInScreen] = bullet;
                _currentBullets++;
                _canShot = false;
                _elapsedTime = 0;
            }
            else
            {
                if (_shots[_nextExplotion % _maxBalasInScreen] != null)
                {
                    _shots[_nextExplotion % _maxBalasInScreen].GetComponent<ExplotionIgnition>().Explote();
                }

                _nextExplotion++;

                bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
                bullet.GetComponent<ExplotionIgnition>().SetPlayerFather(_playerFather);
                bullet.transform.rotation = transform.rotation;
                bullet.GetComponent<Rigidbody2D>().AddForce(AngleToDirection() * _speed,ForceMode2D.Impulse);
                //bullet.GetComponent<Rigidbody2D>().velocity = AngleToDirection() * _speed;

                _shots[_currentBullets % _maxBalasInScreen] = bullet;
                _currentBullets++;
                _canShot = false;
                _elapsedTime = 0;
            }
        }
       
        
    }

    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();

        for(int i = 0; i < _maxBalasInScreen; i++)
        {
            if (_shots[i] != null)
            {
                _shots[i].GetComponent<ExplotionIgnition>().Explote();
            }
            
        }

        _currentBullets = 0;
        _nextExplotion = 0;

    }


    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _animatorsManager = GetComponentInParent<AnimatorsManager>();
        _currentBullets = 0;
        _nextExplotion = 0;
        _shots = new GameObject[_maxBalasInScreen];
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canShot)
        {
            if (_elapsedTime < _enfriamiento)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                _canShot = true;
            }
        }
    }
}
