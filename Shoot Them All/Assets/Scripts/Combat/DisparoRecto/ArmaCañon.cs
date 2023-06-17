using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//cambiar el nombre de la clase por ca�on
public class ArmaCa�on : AttackGeneral
{
    [SerializeField]
    private DisparoRectoBeheaviour _disparoRectoBehaviour;

    #region parameters

    //mas adelante estos parametros deberian ser cambiados por codigo igual hasta hacer un struct para estos ? ya no hace falta creo
    [Tooltip("N�mero de balas m�ximas del cargador")]
    [SerializeField] private int _maxBalas;

    [Tooltip("Tiempo que debe pasar entre un disparo y otro")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Velocidad que lleva la bala ")]
    [SerializeField] private float _speed;

    //[SerializeField]
    //private PointsComponent _playerFather;
    #endregion

    #region references
    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private Transform _weaponSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    #endregion

    #region properties

    private int _currentBullets;
    private float _elapsedTime;

    [SerializeField]
    private bool _infiniteAmo;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        Debug.Log("ataque 1");

        Debug.Log("condition" + ShootCondition());
        if(ShootCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            _disparoRectoBehaviour.PerfomShoot(_bulletPrefab, _playerFather, _raycastDir,
                _bulletSpawnPoint.position,ref _currentBullets,ref _elapsedTime,_speed);
        }
    }

    //TODO
    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();
        _disparoRectoBehaviour.Reload(ref _currentBullets,_maxBalas);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StatMethod();

        _disparoRectoBehaviour = GetComponent<DisparoRectoBeheaviour>();
        _elapsedTime = 0;
        _currentBullets = _maxBalas;
    }


    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime < _enfriamiento)
        {
            _elapsedTime += Time.deltaTime;
        }
    }

    /// <summary>
    /// Metodo para las condiciones de disparo normales del disparo recto
    /// </summary>
    /// <returns></returns>
    public bool ShootCondition()
    {
        return _elapsedTime > _enfriamiento && (_infiniteAmo || _currentBullets > 0);
    }
}
