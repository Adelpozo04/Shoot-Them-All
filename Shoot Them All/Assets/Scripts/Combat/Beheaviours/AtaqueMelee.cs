using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using TreeEditor;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent (typeof (PolygonCollider2D))]
[RequireComponent (typeof (MeshFilter))]
public class AtaqueMelee : MonoBehaviour
{
    #region references
    private PolygonCollider2D _weaponCollider;
    #endregion

    #region parameters
    [Tooltip("Cantidad de raycast para fabricar la hitbox")]
    [SerializeField]
    private int meshRays;
    [Tooltip("Ancho de la hitbox din�mica")]
    [Range(0f, 360f)]
    [SerializeField]
    private float _attackWidth;
    [Tooltip("Posicionamiento de la hitbox din�mica en relaci�n al arma")]
    [Range(-180f, 180f)]
    [SerializeField]
    private float _attackPosition;
    private float _meshAngle;
    private float _variation;
    [Tooltip("Longitud de la hitbox din�mica")]
    [SerializeField]
    private float _distance;

    [Tooltip("Duraci�n del pollazo")]
    [SerializeField]
    private float _hitTime;
    public float HitTime
    {
        get { return _hitTime; }
    }
    private float thisTime;

    private Mesh _weaponMesh;

    private bool isAttacking;
    public bool IsAttacking { get { return isAttacking; } }
    #endregion

    #region methods
    private void EstablishCollider()
    {
        Vector2[] newPoints = new Vector2[_weaponMesh.vertices.Length];
        for (int i = 0; i < _weaponMesh.vertices.Length; i++)
        {
            newPoints[i] = _weaponMesh.vertices[i];
        }
        _weaponCollider.points = newPoints;
    }

    /// <summary>
    /// M�todo auxiliar para transformar un �ngulo en float a un Vector3
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
        _meshAngle = atkPos;

        //Arrays necesarios para crear un mesh
        Vector3[] vertices = new Vector3[rays + 2];
        Vector2[] uv = new Vector2[rays + 2];
        int[] triangles = new int[rays * 3];

        //Colocamos el primer v�rtice en la posici�n de origen
        vertices[0] = Vector3.zero;

        //Variables auxiliares e �ndices para el m�todo siguiente
        int vertexIndex = 1, triangleIndex = 0;
        //Vector3 vertex;

        for (int i = 0; i <= rays; i++)
        {   
            //Se asigna el vector a los v�rtices
            vertices[vertexIndex] = GetVectorFromAngle(_meshAngle) * dis;
            uv[vertexIndex] = vertices[vertexIndex].normalized;

            //Cuando tengamos tres v�tices (el 0 y dos raycast), asignamos al array de tri�ngulos la posici�n
            //en el array de v�rtices de los v�rtices que necesita utilizar para crear un tri�ngulo en concreto
            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;
                triangleIndex += 3;
            }

            //Aumentamos el �ndice y variamos el �ngulo del siguiente raycast
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
        Debug.Log("tu vieja");
        _weaponCollider.enabled = true;
        isAttacking = true;
        thisTime = 0;
    }
    #endregion 

    void Start()
    {
        //Asigna el objeto que tiene la hitbox del arma, as� como asignar un mesh a las armas en espec�fico
        //y el transform
        _weaponCollider = GetComponent<PolygonCollider2D>();
        _weaponMesh = CreateMesh(_attackWidth, meshRays, _attackPosition, _distance);
        EstablishCollider();
        GetComponent<MeshFilter>().mesh = _weaponMesh;      
        //thisTime = cooldown + _hitTime + 1;
        isAttacking = false;
    }

    // Update is called once per frame
    void Update()
    {

        if(thisTime < _hitTime)
        {
            thisTime += Time.deltaTime;
        }

        if(isAttacking && thisTime > _hitTime)
        {
            _weaponCollider.enabled = false;
            isAttacking = false;
        }
        //Movido por propositos de balanceo
        _weaponMesh = CreateMesh(_attackWidth, meshRays, _attackPosition, _distance);
        EstablishCollider();
        GetComponent<MeshFilter>().mesh = _weaponMesh;
    }
}
