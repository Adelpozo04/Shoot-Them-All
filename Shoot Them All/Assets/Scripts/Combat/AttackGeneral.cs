using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//Clase padre de las armas
public class AttackGeneral : MonoBehaviour
{
    private AnimatorsManager _animatorsManager;

    private Transform _player;
    private Transform _myTransform;
    protected PointsComponent _playerFather;

    private RaycastHit2D raycast;
    protected Vector3 _raycastDir;
    private LayerMask _floorLayer;
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
    protected bool WeaponWallDetector()
    {
        _raycastDir = _myTransform.position - _player.position;
        raycast = Physics2D.Raycast(_player.transform.position, _raycastDir, _raycastDir.magnitude, _floorLayer);
        return raycast;
    }
    #endregion
    #region InsertInUnityMethods
    protected void StatMethod()
    {
        _myTransform = transform;
        _player = transform.parent;
        _animatorsManager = GetComponentInParent<AnimatorsManager>();
        _playerFather = GetComponent<PointsComponent>();
        _floorLayer = LayerMask.GetMask("Floor");
        _raycastDir = _myTransform.position - _player.position;
    }
    #endregion

    #endregion
}
