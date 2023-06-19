using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KnockbackComponent : MonoBehaviour
{
    private Rigidbody2D _myRigidBody2D;
    private JumpComponent _myJumpComponent;
    private PlayerInput _playerInput;

    //[SerializeField] private float powerRegulator;
    private Vector2 _impulseForce;
    private float _verticalImpulse;
    [SerializeField]
    private float _knockBackTime;
    [Tooltip("Proporción del impulso que se añadira de forma vertical si se está en el suelo")]
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
    /// Convierte la cantidad de daño recibido en la potencia a recibir y el tiempo de duracion del
    /// knockback
    /// </summary>
    /// <param name="percentage"></param>
    /// <returns></returns>
    private float ConvertPercentageToPower(int percentage)
    {
        _knockBackTime = 0.005f * percentage + 0.1f;
        _verticalImpulse = _proportionPercentagePerVerticalImpulse * percentage;
        return 0.02f * percentage * (float)Math.Log(percentage);
    }

    ///// <summary>
    ///// Ataque balas y dash
    ///// Pasándole el objeto de la colisión y el porcentaje del receptor, llama a los métodos necesarios para realizar el knockback.
    ///// </summary>
    ///// <param name="collision"></param>
    ///// <param name="percentage"></param>
    //public void Knockback(GameObject collision, int percentage)
    //{
    //    StartCoroutine(KockBackDisables());
    //    _impulseForce = ConvertDirection(collision) * ConvertPercentageToPower(percentage);
    //    if (_myJumpComponent.Floor)        //Si estoy en el aire se realiza el impulso realista
    //    {
    //        _impulseForce += Vector2.up * _verticalImpulse;
    //    }
    //    _myRigidBody2D.velocity = Vector2.zero;
    //    _myRigidBody2D.AddForce(_impulseForce, ForceMode2D.Impulse);

    //}

     /// <summary>
     /// Aplica el kockback al jugadror
     /// <para></para>
     /// Lamar siempre a este metodo pasando el vector 2 ya procesado
     /// </summary>
     /// <param name="collision"></param>
     /// <param name="percentage"></param>
    public void Knockback(Vector2 direction, int percentage)
    {
        StartCoroutine(KockBackDisables());
        Debug.Log("direccion knockback" + direction);
        _impulseForce = direction * ConvertPercentageToPower(percentage);
        if (_myJumpComponent.Floor)        //Si estoy en el aire se realiza el impulso realista
        {
            _impulseForce += Vector2.up * _verticalImpulse;
        }
        _myRigidBody2D.velocity = Vector2.zero;
        _myRigidBody2D.AddForce(_impulseForce, ForceMode2D.Impulse);

    }

    private void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _myJumpComponent = GetComponent<JumpComponent>();
        _playerInput = GetComponent<PlayerInput>();
    }
    /// <summary>
    /// Corrutina para desactivar el input y dar IFrames
    /// </summary>
    /// <returns></returns>
    private IEnumerator KockBackDisables()
    {
        _playerInput.actions.Disable();
        _playerInput.GetComponent<HorizontalComponent>().enabled = false;
        _playerInput.GetComponent<EdgeComponet>().enabled = false;
        yield return new WaitForSeconds(_knockBackTime);
        _playerInput.actions.Enable();
        _playerInput.GetComponent<HorizontalComponent>().enabled = true;
        _playerInput.GetComponent<EdgeComponet>().enabled = true;
    }
}
