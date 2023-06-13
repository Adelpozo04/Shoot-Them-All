using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Clase padre de las armas
public class AttackGeneral : MonoBehaviour
{

    protected AnimatorsManager _animatorsManager;


    #region methods

    public virtual void AtaquePrincipal()
    {
        // funcion base para hacer las animaciones
        _animatorsManager?.TriggerShoot1();
        _animatorsManager?.TriggerShoot();

    }

    public virtual void AtaqueSecundario()
    {
        // funcion base para hacer las animaciones
        _animatorsManager?.TriggerShoot2();
    }

    protected Vector2 AngleToDirection()
    {
        Vector2 _direction;
        _direction.x = Mathf.Cos(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad);
        _direction.y = Mathf.Sin(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad);

        if (GetFather().transform.localScale.x < 0)
        {
            return -_direction;
        }
        else
        {
            return _direction;
        }
        

        
    }

    protected GameObject GetFather()
    {
        return transform.parent.parent.gameObject;
    }

    #endregion
}
