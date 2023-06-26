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
    [SerializeField] private float _force;
    [SerializeField] GameObject _bulletPrefab;
    #endregion


    #region properties
    private int _currentBullets;
    private float _elapsedTime;
    private ChoqueArmaDashComponent _choqueDash;
    private DisparoParabolico _parabolicoComponent;
    private DashComponent _dashComponent;

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
        base.AtaqueSecundario();
        if (!WeaponWallDetector())
        {
            base.AtaqueSecundario();
            GameObject bullet = _parabolicoComponent.PerfomShoot(_bulletPrefab, _playerFather,
                AngleToDirection(), transform.position, ref _currentBullets, ref _elapsedTime, _force);

            bullet.GetComponent<Choque>().SetDamage(_damageSec);
        }

    }

    #endregion


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
        
    }

}
