using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Paso intermedio del input
public class InputAtaques : MonoBehaviour
{
    #region properties

    [SerializeField] private AttackGeneral _miArmaActual;
    public AttackGeneral MiArmaActual
    {
        set { _miArmaActual = value; }
    }

    #endregion

    public void AtaquePrincipal(InputAction.CallbackContext contex)
    {
        //Debug.Log("Recibe input");

        if (contex.performed)
        {
            _miArmaActual.AtaquePrincipal();
        }
    }

    public void AtaqueSecundario(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            _miArmaActual.AtaqueSecundario();
        }
    }    
}
