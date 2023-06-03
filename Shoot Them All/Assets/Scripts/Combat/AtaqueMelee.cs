using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtaqueMelee : MonoBehaviour
{
    #region references
    private GameObject _myWeapon;
    private Mesh _weaponMesh;
    private RotarArma _myRotation;
    #endregion

    #region parameters
    private bool _isAttacking;
    private bool _endAttack;
    private float _currentAngle;
    private float _nextAngle;
    private float _endAngle;

    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackSpeed;

    [SerializeField]
    private int meshRays;
    [SerializeField]
    private float _attackWidth;
    private float _meshAngle;
    private float _variation;
    [SerializeField]
    private float _distance;
    [SerializeField]
    private LayerMask _myLayerMask;
    #endregion

    #region methods
    private Vector3 GetVectorFromAngle(float angle)
    {
        float angRadians = (angle * Mathf.PI) / 180;
        return new Vector3(Mathf.Cos(angRadians), Mathf.Sin(angRadians)).normalized;
    }

    private void UpdateMesh()
    {
        Vector3[] vertices = new Vector3[meshRays + 2];
        Vector2[] uv = new Vector2[meshRays + 2];
        int[] triangles = new int[meshRays * 3];

        _currentAngle = _nextAngle;
        _nextAngle -= attackSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _nextAngle);
        _variation = _attackWidth / meshRays;
        _meshAngle = _attackWidth;

        vertices[0] = Vector3.zero;

        int vertexIndex = 1, triangleIndex = 0;
        Vector3 vertex;

        //mierda de meshes
        for (int i = 0; i <= meshRays; i++)
        {   
            RaycastHit2D raycast = Physics2D.Raycast(Vector3.zero, GetVectorFromAngle(_meshAngle), _distance, _myLayerMask);

            if (raycast.collider == null)
            {
                vertex = GetVectorFromAngle(_meshAngle) * _distance;
            }
            else
            {
                vertex = GetVectorFromAngle(_meshAngle) * raycast.distance;
            }

            vertices[vertexIndex] = vertex;
            uv[vertexIndex] = vertex.normalized;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            vertexIndex++;
            _meshAngle -= _variation;
        }

        _weaponMesh.vertices = vertices;
        _weaponMesh.uv = uv;
        _weaponMesh.triangles = triangles;

        
        //_myWeapon.GetComponent<MeshCollider>().sharedMesh = _weaponMesh;

        if (_nextAngle < _endAngle)
        {
            _isAttacking = false;
            _endAttack = true;
        }
    }

    public void Attack(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            _nextAngle = transform.rotation.z + attackRange / 2;
            Debug.Log(_nextAngle);
            _endAngle = transform.rotation.z - attackRange / 2;
            Debug.Log(_endAngle);
            Debug.Log(transform.rotation.z);
            if (_endAngle > _nextAngle)
            {
                _endAngle -= 360;
            }
            transform.rotation = Quaternion.Euler(0, 0, _nextAngle);           
            _isAttacking = true;
        }    
    }
    #endregion

    void Start()
    {
        _myRotation = gameObject.GetComponent<RotarArma>();
        _myWeapon = transform.GetChild(1).gameObject;
        _weaponMesh = new Mesh();
        _myWeapon.GetComponent<MeshFilter>().mesh = _weaponMesh;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_isAttacking) 
        { 
            UpdateMesh();
        }
        else if (_endAttack)
        {
            _endAttack = false;
            _weaponMesh = new Mesh();
            _myWeapon.GetComponent<MeshFilter>().mesh = _weaponMesh;
        }
    }
}
