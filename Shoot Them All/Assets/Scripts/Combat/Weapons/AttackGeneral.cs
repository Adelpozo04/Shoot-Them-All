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
    [Tooltip("Daño del arma")]
    [SerializeField]
    protected int _damage;

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
        
        if (GetPlayer().transform.localScale.x < 0)
        {
            return -_direction;
        }
        else
        {
            return _direction;
        }
    }

    protected GameObject GetPlayer()
    {
        return transform.parent.parent.gameObject;
    }
    /// <summary>
    /// Metodo que lanza el rayo para comprobar si hay un muro delante
    /// </summary>
    /// <returns></returns>
    protected bool WeaponWallDetector()
    {
        _raycastDir = _myTransform.position - _player.position;
        raycast = Physics2D.Raycast(_player.transform.position, _raycastDir, _raycastDir.magnitude, _floorLayer);
        return raycast;
    }
    #endregion

    #region InsertInUnityMethods
    protected void StartMethod()
    {
        _myTransform = transform;
        _player = transform.parent;
        _animatorsManager = GetComponentInParent<AnimatorsManager>();
        _playerFather = transform.parent.parent.GetComponent<PointsComponent>();
        _floorLayer = LayerMask.GetMask("Floor");
        _raycastDir = _myTransform.position - _player.position;
    }
    #endregion

    #endregion
}
