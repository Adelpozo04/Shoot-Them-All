using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
[RequireComponent(typeof(HorizontalComponent))]
public class JumpComponent : MonoBehaviour
{
    #region References
    [SerializeField]
    Transform _foot;
    Rigidbody2D _rigidBody;
    HorizontalComponent _horizontalComponent;
    AnimatorsManager _animatorsManager;
    EdgeComponet _edgeComponet;
    #endregion

    #region Parameters
    [Tooltip("Altura maxima del salto con una sola pulsacion")]
    [SerializeField]
    float _minimumMaxHeith = 1;

    [Tooltip("Distancia horizontal a la que se encuetra el punto medio de la parabola")]
    [SerializeField]
    float _middleJump = 1;

    float _horizontalSpeedInAir;

    [Tooltip("Tiempo que se permite mantener el boton impulsando al jugador")]
    [SerializeField]
    float _maxAdditionalSpeedTime;

    [Tooltip("Proporcion de la gravedad que se añade al salto para ampliarlo")]
    [Range(0.0f, 1)]
    [SerializeField]
    float _additionalJumpProportion;
    [Tooltip("Numero de saltos adicionales")]
    [SerializeField]
    int _additionalJumps;
    #endregion

    #region Properties
    float _initialSpeed = 4;
    LayerMask _layerMask;
    float _additionalSpeedTime;
    bool _salto;
    bool _floor;
    public bool Floor
    {
        get { return _floor; }
    }
    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        //empleamos nuestra propia gravedad
        //que borracho que sos pero dale
        _rigidBody.gravityScale = 0;
        _layerMask = LayerMask.GetMask("Floor");
        _foot.localPosition = Vector2.up * -0.1f;
        _animatorsManager = GetComponent<AnimatorsManager>();
        _horizontalComponent = GetComponent<HorizontalComponent>();
        _edgeComponet = GetComponent<EdgeComponet>();
        _horizontalSpeedInAir = _horizontalComponent.SpeedToAcelerate;
    }

    void FixedUpdate()
    {
        //calculo de los parametros del salto
        //mas adelante mover al start cuando se hayan fijado los parametros
        _initialSpeed = (2 * _minimumMaxHeith * _horizontalSpeedInAir) / _middleJump;
        Physics2D.gravity = Vector2.up * -_initialSpeed * (_horizontalSpeedInAir / _middleJump);

        //Aplicacion de las formulas de aceleracion
        if (!_floor)
        {
            //_rigidBody.position += _rigidBody.velocity * Time.fixedDeltaTime + 0.5f * Physics2D.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            _rigidBody.velocity += Vector2.up * Physics2D.gravity * Time.fixedDeltaTime;
        }

        //Ajuste de Salto
        if (_salto && !_floor && _additionalSpeedTime < _maxAdditionalSpeedTime && _additionalJumps == 1)
        {
            Debug.Log("Mas salto");
            _rigidBody.velocity += Vector2.down * Physics2D.gravity * Time.fixedDeltaTime * _additionalJumpProportion;
            _additionalSpeedTime += Time.fixedDeltaTime;
        }
        _floor = Physics2D.Raycast(_foot.position, Vector2.down, 0.5f, _layerMask);
        _animatorsManager?.ChangeFloor(_floor);
        if (!_floor)
        {
            _animatorsManager?.ChangeJumpingBend(_rigidBody.velocity.y);
        }
        else
        {
            _animatorsManager?.ChangeJumpingBend(0);
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Si esta en el suelo reinicia los saltos
        if(_floor)
        {
            _additionalJumps = 1;
            _animatorsManager?.ChangeNJump(0);
        }
    }
    #endregion

    #region ProperMethods
    /// <summary>
    /// Al pulsar el boton el personaje recibe un impulso inicial y acciona los 
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && _additionalJumps > 0 && !_edgeComponet.OnEdge)
        {
            _rigidBody.velocity = Vector2.up * _initialSpeed;
            _floor = false;
            _salto = true;
            _additionalJumps--;
            if (_additionalJumps == 0)
            {
                _animatorsManager?.ChangeNJump(1);
            }

        }
        if (context.canceled)
        {
            _additionalSpeedTime = 0;
            _salto = false;
        }
    }
    #endregion
}
