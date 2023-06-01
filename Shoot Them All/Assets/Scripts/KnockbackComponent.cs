using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackComponent : MonoBehaviour
{
    [SerializeField] GameObject square;
    [SerializeField]int percentaje = 100;
    [SerializeField] Vector2 cubovelocidad;



    private Rigidbody2D _myRigidBody2D;

    [SerializeField] private float powerRegulator;

    /// <summary>
    /// Se pasa el enemigo/arma que nos daña para retornar la velocidad del mismo
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private Vector2 ConvertDirection(GameObject collision)
    {
        return collision.GetComponent<Rigidbody2D>().velocity;
    }

    private float ConvertPercentageToPower(int percentage)
    {
        return percentage * powerRegulator;
    }

    public void Knockback(GameObject collision, int percentage)
    {
        _myRigidBody2D.AddForce(ConvertDirection(collision) * ConvertPercentageToPower(percentage) * Time.deltaTime);
    }

    private void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        square.GetComponent<Rigidbody2D>().velocity = cubovelocidad;
        Knockback(square, percentaje);
    }
}
