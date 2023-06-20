using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashArma : AttackGeneral
{

    #region parameters

    [Tooltip("Tiempo para poder usar la habilidad otra vez")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Fuerza con la que se hace el dash")]
    [SerializeField] private float _dashingPower;

    [Tooltip("Tiempo en el que esta haciendo dash")]
    [SerializeField] private float _dashingTime;
    #endregion


    #region properties

    private Rigidbody2D _myFatherRB;
    private ChoqueArmaDashComponent _choqueDash;
    private HorizontalComponent _horizontalComponent;
    private JumpComponent _jumpComponent;
    private EdgeComponet _edgeComponet;
    //colocar trail renderer en el centro del arma
    private TrailRenderer _trailRenderer;
    private GameObject _jugador;
    private bool _canDash = true;
    private float _elapsedTime = 0;
    private Vector2 _direction;


    #endregion

    #region methods

    //Se calcula la direcciom a la que va el dash a partir de la rotación del centro del arma 
    public override void AtaquePrincipal()
    {
        _direction = AngleToDirection();
        //Se pide al jugador que haga el dash

        if (_canDash)
        {
            base.AtaquePrincipal();
            StartCoroutine(Dash(_direction));
            _choqueDash.ChangeDamageStage(true);
        }
    }

    #endregion


    void Start()
    {
        StartMethod();
        _jugador = GetPlayer();
        _myFatherRB = _jugador.GetComponent<Rigidbody2D>();
        _trailRenderer = transform.parent.GetComponent<TrailRenderer>();
        _horizontalComponent = _jugador.GetComponent<HorizontalComponent>();
        _jumpComponent = _jugador.GetComponent<JumpComponent>();
        _edgeComponet = _jugador.GetComponent<EdgeComponet>();
        _choqueDash = GetComponent<ChoqueArmaDashComponent>();
        _choqueDash.SetDamage(_damage);
        _choqueDash.Player = _jugador;
        
    }

   

    private IEnumerator Dash(Vector2 direction)
    {

        _canDash = false;
        _horizontalComponent.enabled = false;
        _jumpComponent.enabled = false;
        _edgeComponet.enabled = false;
        _myFatherRB.velocity += direction * _dashingPower * _horizontalComponent.SpeedToAcelerate;
        

        _trailRenderer.emitting = true;
        Debug.Log(direction);

        while (_elapsedTime < _dashingTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        _horizontalComponent.enabled = true;
        _jumpComponent.enabled = true;
        _edgeComponet.enabled = true;
        _elapsedTime = 0;

        _myFatherRB.velocity = Vector2.zero;
        _trailRenderer.emitting = false;
        _choqueDash.ChangeDamageStage(false);
        yield return new WaitForSeconds(_enfriamiento);
        _canDash = true;
    }
}
