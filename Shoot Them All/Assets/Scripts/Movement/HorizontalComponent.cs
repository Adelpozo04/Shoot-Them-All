using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

[RequireComponent(typeof(JumpComponent))]
public class HorizontalComponent : MonoBehaviour
{
    #region References
    Transform _myTransform;
    Rigidbody2D _rigidbody;
    AnimatorsManager _animatorsManager;
    #endregion

    #region Parameters
    [Tooltip("Tiempo que tarda el jugadro en acelerar")]
    [SerializeField]    
    private float _timeToAcelerate;

    [Tooltip("Tiempo que tarda el jugador en frenar")]
    [SerializeField]
    private float _timeToDecelerate;

    [Tooltip("Velocidad máxima que alcanza el jugador")]
    [SerializeField]
    public float _speedToAcelerate;

    [Tooltip("Tamaño de la caja para detectar muros")]
    [SerializeField]
    Vector2 _wallDetectorBox = new Vector2(1.1f, 0.5f);
    #endregion

    #region Properties
    [SerializeField]//serializado para hacer seguimiento del correcto funcionamiento
    private float _speed;
    private float _aceleration;
    private float _deceleration;
    private float _lastDirecciton = 1;
    private float _horizontalDirecction; //propiedad determinada por input 
    LayerMask _layerMask;
    RaycastHit2D _wallBox;
    #endregion

    #region UnityMethods
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Walk");
        _myTransform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _aceleration = _speedToAcelerate / _timeToAcelerate;
        _deceleration = _speedToAcelerate / _timeToDecelerate;
        _layerMask = LayerMask.GetMask("Floor");
        _animatorsManager = GetComponent<AnimatorsManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Math.Sign(_horizontalDirecction) != Math.Sign(_lastDirecciton) && _horizontalDirecction != 0)
        {
            _myTransform.localScale = new Vector2(-_myTransform.localScale.x, _myTransform.localScale.y);
        }
        //Es posible que haya que cambiar el comportamiento en función de si se esta en el aire o no
        SetSpeed();
        //Mu chulo esto pero boy a comentarlo
        //Básicamenete lo que hace es castear una cajita y comprobar la normal en x de la pared con la que se choca
        //Luego comprueba si es la misma que la dirección y deja mover si es que sí
        //Muy chulo el método le doy un salchicón sobre 7.9
        _wallBox = Physics2D.BoxCast(_myTransform.position, _wallDetectorBox, 0, Vector2.zero, 0, _layerMask);
        if (Math.Sign(_wallBox.normal.x) == Math.Sign(_lastDirecciton) || _wallBox.normal.x == 0)
        {
            _rigidbody.velocity = new Vector2(_speed, _rigidbody.velocity.y);
        }
    }
    private void OnDrawGizmos()
    {
        //Dibuja un cubo tipo debug
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(transform.position, _wallDetectorBox);
    }
    #endregion

    #region MyMethods

    /// <summary>
    /// Metodo de abstracion para ajustar el parametro _speed para realizar la frenada y la aceleracion
    /// </summary>
    private void SetSpeed()
    {
        
        //aceleración
        if(_speed < _speedToAcelerate && _speed > -_speedToAcelerate && _horizontalDirecction != 0)
        {
            _speed += Time.fixedDeltaTime * _horizontalDirecction * _aceleration;
            _lastDirecciton = _horizontalDirecction;
        }
        //deceleracion
        if(_horizontalDirecction == 0 && _speed != 0)
        {
            _speed += Time.fixedDeltaTime * -_lastDirecciton * _deceleration;
            //porros de pablo alto trocolo mi hermano
            //sheeeesh
            //Fuera coñas es una condición de terminación para que siempre acaba en 0
            if (_speed * _lastDirecciton < 0)
            {
                _speed = 0;
            }
        }
        //Mecanismos de seguridad para ajustar la velocidad debido a las operaciones con coma flotante
        _speed = Math.Clamp(_speed, -_speedToAcelerate, _speedToAcelerate);
        //Parada del personaje si se encuentra contra un muro
        if (Math.Sign(_speed) != Math.Sign(_wallBox.normal.x) && _wallBox.normal.x != 0)
        {
            _speed = 0;
        }
    }

    public void HorizontalMovement(InputAction.CallbackContext context)
    {
        _horizontalDirecction = context.ReadValue<Vector2>().x;
        _animatorsManager?.ChangeWalking(_horizontalDirecction != 0);
        //Evita que en el cambio de dirección el jugador patine
        if (Math.Sign(_horizontalDirecction) != Math.Sign(_lastDirecciton) && _horizontalDirecction != 0)
        {
             
            _speed = 0;
        }
    }

    //posible metodo para el sprint
    //WIP
    public void Sprint(InputAction.CallbackContext context)
    {
        if (context.performed && context.interaction is TapInteraction)
        {
            Debug.Log("Corre por tu vieja");
        }
    }
    #endregion

}
