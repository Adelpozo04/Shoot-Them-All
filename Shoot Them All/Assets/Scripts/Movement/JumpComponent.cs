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
    public Transform Feet { get { return _foot; } }
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

    [Tooltip("Proporcion de la gravedad que se a�ade al salto para ampliarlo")]
    [Range(0.0f, 1)]
    [SerializeField]
    float _additionalJumpProportion;
    [Tooltip("Numero de saltos adicionales")]
    [SerializeField]
    int _additionalJumps;
    [Tooltip("La mitad del ancho de detecci�n de los pies")]
    [SerializeField]
    float _lateralFootOffset;
    public float FootOffset { get { return _lateralFootOffset; } }
    #endregion

    #region Properties
    Vector2 _gravity;
    float _initialSpeed = 4;
    LayerMask _layerMask1;
    LayerMask _layerMask2;
    float _additionalSpeedTime;
    bool _salto;
    bool _floor;
    public bool Floor
    {
        get { return _floor; }
    }

    bool _atravesando;
    public bool Atravesando { set { _atravesando = value; } }
    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        //empleamos nuestra propia gravedad
        //que borracho que sos pero dale
        _rigidBody.gravityScale = 0;
        _layerMask1 = LayerMask.GetMask("Floor");
        _layerMask2 = LayerMask.GetMask("Atravesable");
        _foot.localPosition = Vector2.up * -0.1f;
        _animatorsManager = GetComponent<AnimatorsManager>();
        _horizontalComponent = GetComponent<HorizontalComponent>();
        _edgeComponet = GetComponent<EdgeComponet>();
        _horizontalSpeedInAir = _horizontalComponent._speedToAcelerate;
        _atravesando = false;
    }

    void FixedUpdate()
    {
        //calculo de los parametros del salto
        //mas adelante mover al start cuando se hayan fijado los parametros
        _initialSpeed = (2 * _minimumMaxHeith * _horizontalSpeedInAir) / _middleJump;
        _gravity = Vector2.up * -_initialSpeed * (_horizontalSpeedInAir / _middleJump);

        //Aplicacion de las formulas de aceleracion
        if (!_floor)
        {
            //_rigidBody.position += _rigidBody.velocity * Time.fixedDeltaTime + 0.5f * Physics2D.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            _rigidBody.velocity += Vector2.up * _gravity * Time.fixedDeltaTime;
        }

        //Ajuste de Salto
        if (_salto && !_floor && _additionalSpeedTime < _maxAdditionalSpeedTime && _additionalJumps == 1)
        {
            Debug.Log("Mas salto");
            _rigidBody.velocity += Vector2.down * _gravity * Time.fixedDeltaTime * _additionalJumpProportion;
            _additionalSpeedTime += Time.fixedDeltaTime;
        }
        //Varios rayos para detectar el suelo
        if (!_atravesando)
        {
            _floor = Physics2D.Raycast(_foot.position, Vector2.down, 0.5f, _layerMask1) ||
            Physics2D.Raycast(_foot.position + Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerMask1) ||
            Physics2D.Raycast(_foot.position - Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerMask1) || 
            Physics2D.Raycast(_foot.position, Vector2.down, 0.5f, _layerMask2) ||
            Physics2D.Raycast(_foot.position + Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerMask2) ||
            Physics2D.Raycast(_foot.position - Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerMask2);
        }
        else
        {
            _floor = Physics2D.Raycast(_foot.position, Vector2.down, 0.5f, _layerMask1) ||
            Physics2D.Raycast(_foot.position + Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerMask1) ||
            Physics2D.Raycast(_foot.position - Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerMask1);
        }

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
