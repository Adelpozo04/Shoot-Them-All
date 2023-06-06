using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoRecto : AttackGeneral
{

    #region parameters

    [Tooltip("Número de balas máximas del cargador")]
    [SerializeField] private int _maxBalas;

    [Tooltip("Tiempo que debe pasar entre un disparo y otro")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Distancia a la que se spawnea la bala (para comprobar que no atraviese paredes")]
    [SerializeField]
    private float _distancia;

    [SerializeField]
    private LayerMask _layer;

    [SerializeField]
    private PointsComponent _playerFather;
    #endregion

    #region references
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _bulletSpawnPoint;
    #endregion

    #region properties
    private GameObject bullet;
    private int _currentBullets;
    private float _elapsedTime;

    /// <summary>
    /// Indica si se puede disparar en relacion al ENFRIAMIENTO
    /// </summary>
    private bool _canShot = true;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        RaycastHit2D raycast = Physics2D.Raycast(Vector2.zero, AngleToDirection(), _distancia, _layer);
        Debug.DrawRay(transform.position, new Vector3(AngleToDirection().x, AngleToDirection().y,0), Color.red, 5);
        if (_currentBullets > 0 && _canShot && raycast.collider == null)
        {
            base.AtaquePrincipal();
            bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            bullet.GetComponent<ChoqueBalaComponent>().SetPlayerFather(_playerFather);
            bullet.transform.rotation = transform.rotation;
            _currentBullets--;
            _canShot = false;
            _elapsedTime = 0;
        }
        
    }

    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();
        Recargar();
    }

    private void Recargar()
    {
        _currentBullets = _maxBalas;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _animatorsManager = GetComponentInParent<AnimatorsManager>();
        _currentBullets = _maxBalas;
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
