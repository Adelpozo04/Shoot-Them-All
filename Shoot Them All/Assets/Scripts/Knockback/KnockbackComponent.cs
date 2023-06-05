using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackComponent : MonoBehaviour
{
    private Rigidbody2D _myRigidBody2D;

    //[SerializeField] private float powerRegulator;
    private Vector2 _impulseForce;
    private float _verticalImpulse;
    [Range(0f, 1f)] [SerializeField] float _proportionPercentagePerVerticalImpulse;     // Se puede cambiar a int

    /// <summary>
    /// Se pasa el enemigo/arma que nos daña para retornar la velocidad del mismo
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private Vector2 ConvertDirection(GameObject collision)
    {
        return collision.GetComponent<Rigidbody2D>().velocity.normalized;
    }

    /// <summary>
    /// Convierte la cantidad de daño recibido en la potencia a recibir
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    private float ConvertPercentageToPower(int percentage)
    {
        _verticalImpulse = _proportionPercentagePerVerticalImpulse * percentage;
        return 0.01f * percentage * (float)Math.Log(percentage);
        //return powerRegulator * percentage;
    }

    /// <summary>
    /// Pasándole el objeto de la colisión y el porcentaje del receptor, llama a los métodos necesarios para realizar el knockback.
    /// Ataque balas y dash
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="percentage"></param>
    public void Knockback(GameObject collision, int percentage)
    {
        //Se puede usar el modo impulso para esto (LUIS)
        //igual hace falta quitar el delta time
        Debug.Log("Hay knockback");
        _impulseForce = ConvertDirection(collision) * ConvertPercentageToPower(percentage) + Vector2.up * _verticalImpulse;
        _myRigidBody2D.velocity = Vector2.zero;
        _myRigidBody2D.AddForce(_impulseForce, ForceMode2D.Impulse);
        // Desactivar Input (por un periodo de tiempo) TODO
        // iFrames (por un periodo de tiempo) TODO

    }
     /// <summary>
     /// Knockback del ataque mele
     /// </summary>
     /// <param name="collision"></param>
     /// <param name="percentage"></param>
    public void Knockback(Vector2 direction, int percentage)
    {
        //Se puede usar el modo impulso para esto (LUIS)
        //igual hace falta quitar el delta time
        Debug.Log("Hay knockback");
        _impulseForce = direction * ConvertPercentageToPower(percentage) + Vector2.up * _verticalImpulse;
        _myRigidBody2D.velocity = Vector2.zero;
        _myRigidBody2D.AddForce(_impulseForce, ForceMode2D.Impulse);
        // Desactivar Input (por un periodo de tiempo) TODO
        // iFrames (por un periodo de tiempo) TODO

    }

    private void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
    }
}
