using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rugby : AttackGeneral
{

    #region parameters
    [Tooltip("Fuerza con la que se hace el dash")]
    [SerializeField] private float _dashingPower;

    [Tooltip("Tiempo en el que esta haciendo dash")]
    [SerializeField] private float _dashingTime;

    [Tooltip("Tiempo que tarda en recargar el arma si esta se sale del mapa")]
    [SerializeField] private float _tiempoRegresoFueraLimites;

    [SerializeField] private float _force;

    [SerializeField] GameObject _bulletPrefab;

    [SerializeField] private int _maxBullets;
    #endregion


    #region properties
    private int _currentBullets;
    private ChoqueArmaDashComponent _choqueDash;
    private DisparoParabolico _parabolicoComponent;
    private DashComponent _dashComponent;

    private GameObject[] _bullets;

    private GameObject _jugador;

    private Vector2 _direction;


    #endregion

    #region methods

    //Se calcula la direcciom a la que va el dash a partir de la rotación del centro del arma 
    public override void AtaquePrincipal()
    {
        _direction = AngleToDirection();
        //Se pide al jugador que haga el dash

        if (_dashComponent.CanDash && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            StartCoroutine(_dashComponent.Dash(_direction,_dashingPower,_dashingTime));
            _choqueDash.ChangeDamageStage(true);
        }
    }
    public override void AtaqueSecundario()
    {
        if (!WeaponWallDetector() && _timerSec > _coolDownSec && _currentBullets > 0)
        {
            base.AtaqueSecundario();
            _bullets[_maxBullets - _currentBullets] =
                _parabolicoComponent.PerfomShoot(_bulletPrefab, _playerPoints,
                AngleToDirection(), transform.position, ref _timerSec, _force);
            _bullets[_maxBullets - _currentBullets].GetComponent<FollowWhoThrow>().RegisterPlayerWhoThrow(GetPlayer()); //Cambiar por padre
                                                                                                                              //Es un poco chapuza lo de +1 pero sino habria que hacer contador individual aparte
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
    #endregion

    private void Update()
    {
        if(_timerSec < _coolDownSec)
        {
            _timerSec += Time.deltaTime;
        }
    }
    void Start()
    {
        StartMethod();
        _jugador = GetPlayer();

        _dashComponent = GetComponent<DashComponent>();
        _dashComponent.Enfriamiento = _coolDownPri;

        _parabolicoComponent = GetComponent<DisparoParabolico>();

        _choqueDash = GetComponent<ChoqueArmaDashComponent>();
        _choqueDash.SetDamage(_damagePri);
        _choqueDash.Player = _jugador;
        _currentBullets = _maxBullets;
        _bullets = new GameObject[_maxBullets];
    }

}
