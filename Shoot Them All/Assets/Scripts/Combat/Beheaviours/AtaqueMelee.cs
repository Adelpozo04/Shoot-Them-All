using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtaqueMelee : MonoBehaviour
{
    #region references
    private GameObject _myWeapon;
    private Mesh _weaponMesh;
    private Transform _myTransform;
    private MeshCollider _weaponCollider;
    #endregion

    #region parameters
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

    [Tooltip("Duración del pollazo")]
    [SerializeField]
    private float timer;
    [Tooltip("Duración del cuwuldown")]
    [SerializeField]
    private float cooldown;
    private float thisTime;
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

    private Mesh CreateMesh(float atkWidth, int rays, float atkPos, float dis)
    {
        Mesh returnMesh = new Mesh();
        _variation = atkWidth / rays;
        _meshAngle = atkWidth * atkPos;

        //Arrays necesarios para crear un mesh
        Vector3[] vertices = new Vector3[rays + 2];
        Vector2[] uv = new Vector2[rays + 2];
        int[] triangles = new int[rays * 3];

        //Colocamos el primer vértice en la posición de origen
        vertices[0] = Vector3.zero;

        //Variables auxiliares e índices para el método siguiente
        int vertexIndex = 1, triangleIndex = 0;
        //Vector3 vertex;

        for (int i = 0; i <= rays; i++)
        {   
            /*
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
                vertex = GetVectorFromAngle(_meshAngle) * _distance;
            } */

            //Se asigna el vector a los vértices
            vertices[vertexIndex] = GetVectorFromAngle(_meshAngle) * dis;
            uv[vertexIndex] = vertices[vertexIndex].normalized;

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
        returnMesh.vertices = vertices;
        returnMesh.uv = uv;
        returnMesh.triangles = triangles;
        return returnMesh;
    }

    public void PerformAttack()
    {
        _weaponCollider.enabled = true;
        thisTime = 0;
    }

    public bool AttackCondition()
    {
        return thisTime > cooldown + timer;
    }
    #endregion

    void Start()
    {
        //Asigna el objeto que tiene la hitbox del arma, así como asignar un mesh a las armas en específico
        //y el transform
        _weaponMesh = CreateMesh(_attackWidth, meshRays, _attackPosition, _distance);
        _myWeapon = transform.GetChild(1).gameObject;
        _myWeapon.GetComponent<MeshFilter>().mesh = _weaponMesh;
        _myTransform = transform;
        _weaponCollider = _myWeapon.GetComponent<MeshCollider>();
        _weaponCollider.sharedMesh = _weaponMesh;
    }

    // Update is called once per frame
    void Update()
    {
        if (thisTime < cooldown + timer)
        {
            thisTime += Time.deltaTime;
        }

        if(_weaponCollider.enabled && thisTime > timer)
        {
            _weaponCollider.enabled = false;
        }
    }
}
