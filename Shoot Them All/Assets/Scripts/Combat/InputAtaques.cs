using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Paso intermedio del input
public class InputAtaques : MonoBehaviour
{
    #region properties

    //por ahora es una referecia pero mas adelante se gestionara por codigo
    [SerializeField] private AttackGeneral _miArmaActual; 

    #endregion

    public void AtaquePrincipal(InputAction.CallbackContext contex)
    {
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
