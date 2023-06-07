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
    private bool _canDash = true;
    private TrailRenderer _myFatherTrailRenderer;
    private float _elapsedTime = 0;
    private Vector2 _direction;
    private GameObject _jugador;
    private Transform _fatherTransform;


    #endregion

    #region methods

    //Se calcula la direcciom a la que va el dash a partir de la rotación del centro del arma 
    public override void AtaquePrincipal()
    {
        _direction = AngleToDirection();
        //Se pide al jugador que haga el dash

        if (_canDash)
        {
            StartCoroutine(Dash(_direction));
        }

        GetComponent<ChoqueArmaDashComponent>().ChangeDamageStage(true);

    }

    #endregion





    void Start()
    {
        _jugador = GetFather();
        _fatherTransform = _jugador.transform;
        _myFatherRB = _jugador.GetComponent<Rigidbody2D>();
        _myFatherTrailRenderer = _jugador.GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Dash(Vector2 direction)
    {

        _canDash = false;

        _myFatherRB.velocity += direction * _dashingPower;
        

        _myFatherTrailRenderer.emitting = true;
        Debug.Log(direction);

        while (_elapsedTime < _dashingTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _elapsedTime = 0;

        _myFatherRB.velocity = Vector2.zero;
        _myFatherTrailRenderer.emitting = false;
        GetComponent<ChoqueArmaDashComponent>().ChangeDamageStage(false);
        yield return new WaitForSeconds(_enfriamiento);
        _canDash = true;
    }
}
