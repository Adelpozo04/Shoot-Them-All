using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;
[RequireComponent(typeof(JumpComponent))]

public class DescentComponent : MonoBehaviour
{
    #region references
    private JumpComponent _jumpComponent;
    #endregion

    #region properties
    private float _downTimer;
    private float _currentTime;
    private LayerMask _playerLayer;
    private LayerMask _playerBajandoLayer;

    private LayerMask _layerAtravesar;

    private float _lateralFootOffset;
    private Transform _loQueLeGustaAJosé;

    private RaycastHit2D hit1;
    private RaycastHit2D hit2;
    private RaycastHit2D hit3;

    private Collider2D _myCollider;

    [SerializeField]
    private bool segso;
    #endregion

    #region methods
    /*public void Descend()
    {
        this.enabled = true;
        _jumpComponent.Atravesando = true;
        gameObject.layer = _playerBajandoLayer;
    }*/

    public void Descend2_DescendHarder()
    {
        hit1 = Physics2D.Raycast(_loQueLeGustaAJosé.position, Vector2.down, 0.5f, _layerAtravesar);
        hit2 = Physics2D.Raycast(_loQueLeGustaAJosé.position + Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerAtravesar);
        hit3 = Physics2D.Raycast(_loQueLeGustaAJosé.position - Vector3.right * _lateralFootOffset, Vector2.down, 0.5f, _layerAtravesar);
        if (hit1)
        {
            Debug.Log("centro");
            Physics2D.IgnoreCollision(hit1.collider, _myCollider);
            _jumpComponent.Atravesando = true;
        }
        if (hit2)
        {
            Debug.Log("chambear");
            Physics2D.IgnoreCollision(hit2.collider, _myCollider);
            _jumpComponent.Atravesando = true;
        }
        if (hit3)
        {
            Debug.Log("comunismo");
            Physics2D.IgnoreCollision(hit3.collider, _myCollider);
            _jumpComponent.Atravesando = true;
        }
    }
    #endregion


        // Start is called before the first frame update
    void Start()
    {
        _jumpComponent = GetComponent<JumpComponent>();
        _currentTime = _downTimer;
        _loQueLeGustaAJosé = _jumpComponent.Feet;
        _lateralFootOffset = _jumpComponent.FootOffset;
        _myCollider = GetComponent<Collider2D>();
        _layerAtravesar = LayerMask.GetMask("Atravesable");
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (_currentTime <= _downTimer)
        {
            _currentTime += Time.deltaTime;
        }
        else
        {
            gameObject.layer = _playerLayer;
            _jumpComponent.Atravesando = false;
            this.enabled = false;
        }
        */
        if(segso)
        {
            Debug.Log("segs");
            Descend2_DescendHarder();
            segso = false;
        }
    }
}
