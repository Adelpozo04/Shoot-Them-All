using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class JumpComponent : MonoBehaviour
{
    [SerializeField]
    Transform _foot;
    Rigidbody2D _rigidBody;
    [SerializeField]
    float _minimumMaxHeith = 1;
    [SerializeField]
    float _middleJump = 1;
    [SerializeField]
    float _initialSpeed = 4;
    [SerializeField]
    float _horizontalSpeedInAir = 3;
    [SerializeField]
    float _maxAdditionalSpeedTime;
    [Range(0.0f, 1)]
    [SerializeField]
    float _additionalJumpProportion;
    [SerializeField]
    int _additionalJumps;
    [SerializeField]
    bool _floor;

    LayerMask _layerMask;
    float _additionalSpeedTime;
    bool _salto;

    #region UnityMethods

    // Start is called before the first frame update
    void Start()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        Debug.Log(Physics2D.gravity);
        Debug.Log(_rigidBody.position);
        _layerMask = LayerMask.GetMask("Floor");
    }

    void FixedUpdate()
    {
        //calculo de los parametros del salto
        _initialSpeed = (2 * _minimumMaxHeith * _horizontalSpeedInAir) / _middleJump;
        Physics2D.gravity = Vector2.up * -_initialSpeed * (_horizontalSpeedInAir / _middleJump);
        //Aplicacion de las formulas de aceleracion
        if (!_floor)
        {
            _rigidBody.position += _rigidBody.velocity * Time.fixedDeltaTime + 0.5f * Physics2D.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            _rigidBody.velocity += Vector2.up * Physics2D.gravity * Time.fixedDeltaTime;
        }

        //Ajuste de Salto
        if (_salto && _additionalSpeedTime < _maxAdditionalSpeedTime && _additionalJumps == 1)
        {
            _rigidBody.velocity += -Vector2.up * Physics2D.gravity * Time.fixedDeltaTime * _additionalJumpProportion;
            _additionalSpeedTime += Time.fixedDeltaTime;
        }
        _floor = Physics2D.Raycast(_foot.position, Vector2.down, 0.5f, _layerMask);
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //Si esta en el suelo reinicia los saltos
        if(_floor)
        {
            _additionalJumps = 2;
        }
    }
    #endregion
    /// <summary>
    /// Al pulsar el boton el personaje recibe un impulso inicial y acciona los 
    /// </summary>
    /// <param name="context"></param>
    public void Jump(InputAction.CallbackContext context)
    {
        if (context.started && _additionalJumps > 0)
        {
            _rigidBody.velocity = Vector2.up * _initialSpeed;
            _floor = false;
            _salto = true;
            _additionalJumps--;
        }
        if (context.canceled)
        {
            _additionalSpeedTime = 0;
            _salto = false;
        }
    }
    public bool OnFloor()
    {
        return _floor;
    }
}
