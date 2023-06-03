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
    private Transform _myTransform;
    #endregion

    #region parameters
    private bool _isAttacking;
    private bool _endAttack;
    private float _nextAngle;
    private float _endAngle;

    [Tooltip("Ángulo de amplitud de ataque")]
    [SerializeField]
    private float attackRange;
    [Tooltip("Velocidad de ataque")]
    [SerializeField]
    private float attackSpeed;

    [Tooltip("Cantidad de raycast para fabricar la hitbox")]
    [SerializeField]
    private int meshRays;
    [Tooltip("Ancho de la hitbox dinámica")]
    [SerializeField]
    private float _attackWidth;
    [Tooltip("Posicionamiento de la hitbox dinámica en relación al arma")]
    [SerializeField]
    private float _attackPosition;
    private float _meshAngle;
    private float _variation;
    [Tooltip("Longitud de la hitbox dinámica")]
    [SerializeField]
    private float _distance;
    [Tooltip("Layermask del suelo")]
    [SerializeField]
    private LayerMask _myLayerMask;
    #endregion

    #region methods
    /// <summary>
    /// Método auxiliar para transformar un ángulo en float a un Vector3
    /// </summary>
    /// <param name="angle"></param>
    /// <returns></returns>
    private Vector3 GetVectorFromAngle(float angle)
    {
        float angRadians = (angle * Mathf.PI) / 180;
        return new Vector3(Mathf.Cos(angRadians), Mathf.Sin(angRadians)).normalized;
    }

    private void UpdateMesh()
    {
        //Parámetros que conforman la psudoanimación. Se puede quitar una vez se haya hecho con esqueleto
        _nextAngle -= attackSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _nextAngle);
        _variation = _attackWidth / meshRays;
        _meshAngle = _attackWidth * _attackPosition;
        //eop

        //Arrays necesarios para crear un mesh
        Vector3[] vertices = new Vector3[meshRays + 2];
        Vector2[] uv = new Vector2[meshRays + 2];
        int[] triangles = new int[meshRays * 3];

        //Colocamos el primer vértice en la posición de origen
        vertices[0] = Vector3.zero;

        //Variables auxiliares e índices para el método siguiente
        int vertexIndex = 1, triangleIndex = 0;
        Vector3 vertex;

        for (int i = 0; i <= meshRays; i++)
        {   
            //Raycast para comprobar la pared
            RaycastHit2D raycast = Physics2D.Raycast(Vector3.zero, GetVectorFromAngle(_meshAngle + _nextAngle), _distance, _myLayerMask);

            //Si no hay pared, se pone el vector a máxima distancia
            if (raycast.collider == null)
            {
                vertex = GetVectorFromAngle(_meshAngle) * _distance;
            }
            //Si hay pared, se pone el vector a la distancia de golpe
            else
            {
                vertex = GetVectorFromAngle(_meshAngle) * raycast.distance;
            }

            //Se asigna el vector a los vértices
            vertices[vertexIndex] = vertex;
            uv[vertexIndex] = vertex.normalized;

            //Cuando tengamos tres vétices (el 0 y dos raycast), asignamos al array de triángulos la posición
            //en el array de vértices de los vértices que necesita utilizar para crear un triángulo en concreto
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            //Aumentamos el índice y variamos el ángulo del siguiente raycast
            vertexIndex++;
            _meshAngle -= _variation;
        }

        //Se asigna al mesh los arrays creados anteriormente
        _weaponMesh.vertices = vertices;
        _weaponMesh.uv = uv;
        _weaponMesh.triangles = triangles;

        //Condición de parada de ataque, se podrá modificar cuando se modifique a por animación
        if (_nextAngle < _endAngle)
        {
            _isAttacking = false;
            _endAttack = true;
        }
    }

    public void Attack(InputAction.CallbackContext contex)
    {
        //Comprueba el input y si está ya atacando
        if (contex.performed && !_isAttacking)
        {
            //Asigna el ángulo inicial y el ángulo final dependiendo de ambas la posición actual y
            //el ancho del ataque
            _nextAngle = transform.rotation.eulerAngles.z + attackRange / 2;
            _endAngle = transform.rotation.eulerAngles.z - attackRange / 2;
            if (_endAngle > _nextAngle)
            {
                _endAngle -= 360;
            }

            //Rota el objeto al ángulo inicial
            _myTransform.rotation = Quaternion.Euler(0, 0, _nextAngle);     
            //eop

            _isAttacking = true;
        }    
    }
    #endregion

    void Start()
    {
        //Asigna el objeto que tiene la hitbox del arma, así como asignar un mesh a las armas en específico
        //y el transform
        _myWeapon = transform.GetChild(1).gameObject;
        _weaponMesh = new Mesh();
        _myWeapon.GetComponent<MeshFilter>().mesh = _weaponMesh;
        _myTransform = transform;
        _myWeapon.GetComponent<MeshCollider>().sharedMesh = _weaponMesh;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_isAttacking) //Si está en periodo de ataque updatea el mesh
        { 
            UpdateMesh();
        }
        else if (_endAttack) //Si está terminando el ataque, vacía el mesh
        {
            _endAttack = false;
            _weaponMesh = new Mesh();
            _myWeapon.GetComponent<MeshFilter>().mesh = _weaponMesh;
            _myWeapon.GetComponent<MeshCollider>().sharedMesh = _weaponMesh;
        }
    }
}
