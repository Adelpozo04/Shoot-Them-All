using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputTest : MonoBehaviour
{
    [SerializeField]
    Transform _foot;
    Rigidbody2D cuerpoRigido;
    [SerializeField]
    float _jumpHeith = 4;
    [SerializeField]
    float _maxJumpHeith = 4;
    float _initialHeith;
    Transform _myTransform;

    Vector2 _verticalSpeedVector;

    bool salto;
    bool _floor;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        cuerpoRigido = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (salto && _myTransform.position.y - _initialHeith < _maxJumpHeith)
        {
            Debug.Log("TU Vieja");
            //_verticalSpeedVector += Vector2.up * _jumpHeith;
        }
        if (_myTransform.position.y - _initialHeith >= _maxJumpHeith)
        {
            Debug.Log("Me mato");
            _verticalSpeedVector = Vector2.zero;
        }
        cuerpoRigido.velocity += _verticalSpeedVector;
    }
    public void TuVieja(InputAction.CallbackContext context)
    {
        if (context.started && _floor)
        {
            _initialHeith = _myTransform.position.y;
            _verticalSpeedVector = Vector2.up * _jumpHeith;
            _floor = false;
        }
        if (context.canceled)
        {
            _verticalSpeedVector = Vector2.zero;
        }
        salto = context.ReadValueAsButton();
        Debug.Log( context.ReadValueAsButton());
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        _floor = true;
    }
}
