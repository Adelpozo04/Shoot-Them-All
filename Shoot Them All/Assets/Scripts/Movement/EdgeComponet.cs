using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EdgeComponet : MonoBehaviour
{
    JumpComponent _jumpComponent;
    Transform _myTransform;
    Rigidbody2D _rb;
    Bounds _bodyBounds;
    LayerMask _floorLayer;
    RaycastHit2D _hit1;
    RaycastHit2D _hit2;
    Ray2D _ray1;
    Ray2D _ray2;

    [SerializeField]
    float _ray1Distance = 0.5f;
    [SerializeField]
    float _ray2OffSetX = 0.5f;
    [SerializeField]
    float _ray2Length = 0.5f;

    bool _onEdge;
    public bool OnEdge
    {
        get { return _onEdge; }
    }
    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        _jumpComponent = GetComponent<JumpComponent>();
        _floorLayer = LayerMask.GetMask("Floor");
        _rb = GetComponent<Rigidbody2D>();
        _bodyBounds = GetComponent<CapsuleCollider2D>().bounds;
    }

    // Update is called once per frame
    void Update()
    {
        _ray1 = new Ray2D(_myTransform.position, new Vector2(_myTransform.localScale.x,0));
        _hit1 = Physics2D.Raycast(_ray1.origin, _ray1.direction,_ray1Distance, _floorLayer);
        _ray2 = new Ray2D(_hit1.point + new Vector2(_ray1.direction.x * _ray2OffSetX,1), Vector2.down * _ray2Length);
        _hit2 = Physics2D.Raycast(_ray2.origin, _ray2.direction, 1, _floorLayer);
        _onEdge = _hit1 && _hit2;
        if (_onEdge && !_jumpComponent.Floor)
        {
            _rb.velocity = Vector2.zero;
            _jumpComponent.enabled = false;
            _myTransform.position = _hit2.point + Vector2.down * _bodyBounds.extents.y - Vector2.right * _ray1.direction.x * (_ray2OffSetX + _bodyBounds.extents.x);
        }
        else
        {
            _jumpComponent.enabled = true;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(_ray1.origin,_ray1.direction * _ray1Distance);
        Gizmos.DrawRay(_ray2.origin, _ray2.direction);
        
    }
    public void ClimbEdge(InputAction.CallbackContext context)
    {
        if (!_jumpComponent.Floor && _hit2 && context.started)
        {
            _myTransform.position = _hit2.point + Vector2.up * _bodyBounds.extents.y;
        }
    }
}
