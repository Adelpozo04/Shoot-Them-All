using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

//Clase padre de las armas
public class AttackGeneral : MonoBehaviour
{
    protected AnimatorsManager _animatorsManager;

    private Transform _player;
    protected Transform _myTransform;
    protected PointsComponent _playerPoints;

    private RaycastHit2D raycast;
    protected Vector3 _raycastDir;
    private LayerMask _floorLayer;

    [Tooltip("Enfriameito del ataque principal")]
    [SerializeField]
    protected float _coolDownPri;
    public float CoolDownPri
    {
        get { return _coolDownPri; }
    }
    protected float _timerPri;
    public float TimerPri
    {
        get { return _timerPri; }
    }
    [Tooltip("Enfriameito del ataque secundario")]
    [SerializeField]
    protected float _coolDownSec;
    protected float _timerSec;
    [Tooltip("Daño del arma")]
    [SerializeField]
    protected int _damagePri;
    [SerializeField]
    protected int _damageSec;

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
        // funcion base para hacer las animacionesS
        Debug.Log(_animatorsManager);
        _animatorsManager?.TriggerShoot1();
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
        Debug.Log(transform.parent.parent.gameObject);
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
    /// <summary>
    /// Condición de tiempo para hacer la habilidad 1
    /// </summary>
    /// <returns></returns>
    protected bool PriTimeCondition()
    {
        return _timerPri >= _coolDownPri;
    }
    /// <summary>
    /// Condición de tiempo para hacer la habilidad 2
    /// </summary>
    /// <returns></returns>
    protected bool SecTimeCondition()
    {
        return _timerSec >= _coolDownSec;
    }
    /// <summary>
    /// Metodo en el update para acutalizar el tiempo primero
    /// </summary>
    protected void RunTimerPri()
    {
        if(!PriTimeCondition()) _timerPri += Time.deltaTime;
    }
    /// <summary>
    /// Metodo en el update para acutalizar el tiempo segundo
    /// </summary>
    protected void RunTimerSec()
    {
        if(!SecTimeCondition()) _timerSec += Time.deltaTime;
    }
    #endregion

    #region InsertInUnityMethods
    protected void StartMethod()
    {
        _myTransform = transform;
        _player = transform.parent;
        _animatorsManager = GetComponentInParent<AnimatorsManager>();
        _playerPoints = transform.parent.parent.GetComponent<PointsComponent>();
        _floorLayer = LayerMask.GetMask("Floor");
        _raycastDir = _myTransform.position - _player.position;
    }
    #endregion

    #endregion
}
