using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackComponent : MonoBehaviour
{
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

    /// <summary>
    /// Convierte la cantidad de daño recibido en la potencia a recibir
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    private float ConvertPercentageToPower(int percentage)
    {
        return powerRegulator * percentage;
    }

    /// <summary>
    /// Pasándole el objeto de la colisión y el porcentaje del receptor, llama a los métodos necesarios para realizar el knockback
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="percentage"></param>
    public void Knockback(GameObject collision, int percentage)
    {
        _myRigidBody2D.AddForce(ConvertDirection(collision) * ConvertPercentageToPower(percentage) * Time.deltaTime);
        // Desactivar Input (por un periodo de tiempo) TODO
        // iFrames (por un periodo de tiempo) TODO

    }

    private void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
    }
}
