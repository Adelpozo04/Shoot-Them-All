using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Clase padre de las armas
public class AttackGeneral : MonoBehaviour
{




    #region methods

    public virtual void AtaquePrincipal()
    {
        

    }

    public virtual void AtaqueSecundario()
    {
        
    }

    protected Vector2 AngleToDirection()
    {
        Vector2 _direction;
        _direction.x = Mathf.Cos(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad);
        _direction.y = Mathf.Sin(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad);

        return _direction;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
