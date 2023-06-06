using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockbackComponent : MonoBehaviour
{
    private Rigidbody2D _myRigidBody2D;
    private JumpComponent _myJumpComponent;

    //[SerializeField] private float powerRegulator;
    private Vector2 _impulseForce;
    private float _verticalImpulse;
    [Tooltip("Proporci�n del impulso que se a�adira de forma vertical si se est� en el suelo")]
    [Range(0f, 1f)] [SerializeField] float _proportionPercentagePerVerticalImpulse;     // Se puede cambiar a int

    /// <summary>
    /// Se pasa el enemigo/arma que nos da�a para retornar la velocidad del mismo
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    private Vector2 ConvertDirection(GameObject collision)
    {
        return collision.GetComponent<Rigidbody2D>().velocity.normalized;
    }

    /// <summary>
    /// Convierte la cantidad de da�o recibido en la potencia a recibir
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
    /// Ataque balas y dash
    /// Pas�ndole el objeto de la colisi�n y el porcentaje del receptor, llama a los m�todos necesarios para realizar el knockback.
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="percentage"></param>
    public void Knockback(GameObject collision, int percentage)
    {
        Debug.Log("Hay knockback");
        if (!_myJumpComponent.Floor)        //Si estoy en el aire se realiza el impulso realista
        {
            _impulseForce = ConvertDirection(collision) * ConvertPercentageToPower(percentage);
        }
        else                                //Si estoy en el suelo se realiza el impulso con el a�adido vertical
        {
            _impulseForce = ConvertDirection(collision) * ConvertPercentageToPower(percentage) + Vector2.up * _verticalImpulse;
        }        
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
        Debug.Log("Hay knockback");
        if (!_myJumpComponent.Floor)        //Si estoy en el aire se realiza el impulso realista
        {
            _impulseForce = direction * ConvertPercentageToPower(percentage);
        }
        else                                //Si estoy en el suelo se realiza el impulso con el a�adido vertical
        {
            _impulseForce = direction * ConvertPercentageToPower(percentage) + Vector2.up * _verticalImpulse;
        } 
        _myRigidBody2D.velocity = Vector2.zero;
        _myRigidBody2D.AddForce(_impulseForce, ForceMode2D.Impulse);
        //corrutina puede ser una buena opcci�n en este caso
        // Desactivar Input (por un periodo de tiempo) TODO
        // iFrames (por un periodo de tiempo) TODO

    }

    private void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myJumpComponent = GetComponent<JumpComponent>();
    }
}
