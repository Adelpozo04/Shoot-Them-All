using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StunComponent : MonoBehaviour
{
    private KnockbackComponent _myKnockBackComponent;
    private PercentageComponent _myPercentageComponent;

    private PlayerInput _myPlayerInput;
    private Vector2 _direction;
    private bool _stunned = false;
    [SerializeField] private float _stunTime = 1;       // Seguramente "stunTime" habrá que cambiarlo a otro sitio
    public bool Stunned
    {
        get { return _stunned; }
    }

    public IEnumerator Stun(float tiempoStun)           // De momento el parámetro "tiempoStun" no se usa, aunque en el futuro
    {                                                   // se vienen cositas
        _stunned = true;

        _myPlayerInput.currentActionMap.Disable();      // Desactivamos el Input que nos interesa (battle)

        yield return new WaitForSeconds(_stunTime);    // Esperamos los segundos que deseemos

        _myPlayerInput.currentActionMap.Enable();       // Volvemos a actuar el input

        _stunned = false;                               // Final estado de Stun

        //_myKnockBackComponent.Knockback(_direction, _myPercentageComponent.Percentage);     // Llamada al knockback final postStun

        //Hemos desactivado la linea de arriba y funka diferente pero hay que ver como desactivar bien el input
    }

    private void Start()
    {
        _myPlayerInput = GetComponent<PlayerInput>();
        _myKnockBackComponent = GetComponent<KnockbackComponent>();
        _myPercentageComponent = GetComponent<PercentageComponent>();
    }
}
