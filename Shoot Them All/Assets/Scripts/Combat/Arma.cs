using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Paso intermedio del input
public class Arma : MonoBehaviour
{

    #region properties

    [SerializeField]
    private AttackGeneral _miArmaActual; 

    #endregion


    public virtual void AtaquePrincipal(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            _miArmaActual.AtaquePrincipal();
        }

    }

    public virtual void AtaqueSecundario(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            _miArmaActual.AtaqueSecundario();
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        //_miArmaActual = GetComponent<DisparoRecto>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
