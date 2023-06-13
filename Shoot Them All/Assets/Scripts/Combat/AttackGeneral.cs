using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Clase padre de las armas
public class AttackGeneral : MonoBehaviour
{
    protected AnimatorsManager _animatorsManager;

    #region methods

    #region Virutal methods

    public virtual void AtaquePrincipal()
    {
        AnimacionesAtaquePrincipal();
    }

    public virtual void AtaqueSecundario()
    {
        AnimacionesAtaqueSecundario();   
    }
    #endregion


    #region Private methods

    private void AnimacionesAtaquePrincipal()
    {
        // funcion base para hacer las animaciones
        _animatorsManager?.TriggerShoot1();
        _animatorsManager?.TriggerShoot();
    }

    private void AnimacionesAtaqueSecundario()
    {
        // funcion base para hacer las animaciones
        _animatorsManager?.TriggerShoot2();
    }
    #endregion


    #region Protected methods

    protected Vector2 AngleToDirection()
    {
        Vector2 _direction = new Vector2(Mathf.Cos(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad),
                                         Mathf.Sin(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad));
        
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


    #endregion
}
