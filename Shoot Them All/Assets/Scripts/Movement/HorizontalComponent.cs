using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.InputSystem;

public class HorizontalComponent : MonoBehaviour
{
    Transform _myTransform;
    Rigidbody2D _rigidbody;
    JumpComponent _jumpComponent;

    [SerializeField]    
    private float _timeToAcelerate;
    [SerializeField]
    private float _speedToAcelerate;
    [SerializeField]
    private float _maxSpeed;

    private float _aceleration;

    LayerMask _layerMask;

    private float _horizontalDirecction;
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _rigidbody = GetComponent<Rigidbody2D>();
        _aceleration = _speedToAcelerate / _timeToAcelerate;
        _layerMask = LayerMask.GetMask("Floor");
        _jumpComponent = GetComponent<JumpComponent>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!Physics2D.BoxCast(_myTransform.position, new Vector2(1.1f,0.5f), 0,Vector2.zero,0, _layerMask) || _jumpComponent.OnFloor())
        {
            _rigidbody.position += Vector2.right * _maxSpeed * _horizontalDirecction * Time.fixedDeltaTime;
        }
    }
    public void HorizontalMovement(InputAction.CallbackContext context)
    {
        _horizontalDirecction = context.ReadValue<Vector2>().x;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;   
        Gizmos.DrawWireCube(_myTransform.position, new Vector2(1.1f, 0.5f));
    }
}
