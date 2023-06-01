using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputTest : MonoBehaviour
{
    [SerializeField]
    Transform _foot;
    Rigidbody2D cuerpoRigido;
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
    float _additionalSpeedTime;
    [Range(0.0f, 1)]
    [SerializeField]
    float _additionalJumpProportion;
    [SerializeField]
    int _additionalJumps;
    
    [SerializeField]
    LayerMask _layerMask;

    Transform _myTransform;

    bool _salto;
    [SerializeField]
    bool _floor;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        cuerpoRigido = GetComponent<Rigidbody2D>();
        Debug.Log(Physics2D.gravity);
        Debug.Log(cuerpoRigido.position);
        _layerMask = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        _initialSpeed = (2 * _minimumMaxHeith * _horizontalSpeedInAir) / _middleJump;
        Physics2D.gravity = Vector2.up * -_initialSpeed * (_horizontalSpeedInAir / _middleJump);
        if (!_floor)
        {
            Debug.Log("Hola" + _floor);
            cuerpoRigido.position += cuerpoRigido.velocity * Time.fixedDeltaTime + 0.5f * Physics2D.gravity * Time.fixedDeltaTime * Time.fixedDeltaTime;
            cuerpoRigido.velocity += Vector2.up * Physics2D.gravity * Time.fixedDeltaTime;
        }

        //Ajuste de Salto
        if (_salto && _additionalSpeedTime < _maxAdditionalSpeedTime && _additionalJumps == 1)
        {
            cuerpoRigido.velocity += -Vector2.up * Physics2D.gravity * Time.fixedDeltaTime * _additionalJumpProportion;
            _additionalSpeedTime += Time.fixedDeltaTime;
        }
    }
    public void TuVieja(InputAction.CallbackContext context)
    {
        if (context.started && _additionalJumps > 0)
        {
            cuerpoRigido.velocity = Vector2.up * _initialSpeed;
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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _floor = true;
        _additionalJumps = 2;
    }
}
