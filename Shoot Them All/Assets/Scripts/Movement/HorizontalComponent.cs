using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class HorizontalComponent : MonoBehaviour
{
    #region References
    Transform _myTransform;
    Rigidbody2D _rigidbody;
    JumpComponent _jumpComponent;
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
    private float _speedToAcelerate;
    #endregion

    #region Properties
    [SerializeField]//serializado para hacer seguimiento del correcto funcionamiento
    private float _speed;
    private float _aceleration;
    private float _deceleration;
    private float _lastDirecciton;
    private float _horizontalDirecction; //propiedad determinada por input
    LayerMask _layerMask;
    #endregion

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _aceleration = _speedToAcelerate / _timeToAcelerate;
        _deceleration = _speedToAcelerate / _timeToDecelerate;
        _layerMask = LayerMask.GetMask("Floor");
        _jumpComponent = GetComponent<JumpComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Es posible que haya que cambiar el comportamiento en función de si se esta en el aire o no
        SetSpeed();
        if (_jumpComponent.OnFloor() || !Physics2D.BoxCast(_myTransform.position, new Vector2(1.1f,0.5f), 0,Vector2.zero,0, _layerMask))
        {
            _rigidbody.position += Vector2.right * Time.fixedDeltaTime * _speed;
        }
    }
    #endregion


    #region MyMethods

    /// <summary>
    /// Metodo de abstracion para ajustar el parametro _speed para realizar la frenada y la aceleracion
    /// </summary>
    private void SetSpeed()
    {
        //aceleración
        if(_speed < _speedToAcelerate && _speed > -_speedToAcelerate && _horizontalDirecction !=0)
        {
            _speed += Time.fixedDeltaTime * _horizontalDirecction * _aceleration;
            _lastDirecciton = _horizontalDirecction;
        }
        //deceleracion
        if(_horizontalDirecction == 0 && Math.Abs(_speed) > 0.0001f)
        {
            _speed -= _lastDirecciton * _deceleration * Time.fixedDeltaTime;
        }
        //Mecanismos de seguridad para ajustar la velocidad debido a las operaciones con coma flotante
        _speed = Math.Clamp(_speed, -_speedToAcelerate, _speedToAcelerate);
        _speed = (float) Math.Round(_speed,3);
    }

    public void HorizontalMovement(InputAction.CallbackContext context)
    {
        _horizontalDirecction = context.ReadValue<Vector2>().x;
    }
    #endregion
}
