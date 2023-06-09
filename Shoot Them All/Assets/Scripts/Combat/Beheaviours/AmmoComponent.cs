using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoComponent : MonoBehaviour
{
    #region references
    private HorizontalComponent _speedComponent;
    #endregion

    #region parameters
    [SerializeField]
    private int _maxAmmo;
    private int _currentAmmo;

    [SerializeField]
    private float _rechargeTime;
    [Tooltip("Proporci�n a la que se ver� reducida la velocidad el jugador al recargar")]
    [SerializeField]
    [Range(0f, 1f)]
    private float _rechargeSlowdown;
    [Tooltip("Establece si recarga todas las balas a la vez (false) o de una en una (true)")]
    [SerializeField]
    private bool _multipleBulletReload;

    private float _currentTime;
    private float _initialSpeed;
    #endregion

    #region methods
    public void RecargaUna()
    {
        if (_currentAmmo < _maxAmmo)
        {
            _currentAmmo++;
        }
    }

    private void RecargaTodas()
    {
        _currentAmmo = _maxAmmo;
    }

    public void Recargar()
    {
        _speedComponent._speedToAcelerate = _speedComponent._speedToAcelerate * _rechargeSlowdown;
        enabled = true;
        _currentTime = 0;
    }

    /// <summary>
    /// Condici�n de disparo
    /// </summary>
    /// <returns></returns>
    public bool PuedeDisparar()
    {
        return _currentAmmo > 0; //&& !enabled; Hay que discutir si podemos interrumpir la recarga para disparar
    }

    public void Dispara()
    {
        _currentAmmo--;
        Cancela();
    }

    public void Cancela()
    {
        enabled = false;
        //se puede guardar en una variable el valor de la velocidad original y asi no nos arriesgamos
        _speedComponent._speedToAcelerate = _initialSpeed;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentAmmo = _maxAmmo;
        _speedComponent = GetComponentInParent<HorizontalComponent>();
        _initialSpeed = _speedComponent._speedToAcelerate;
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime < _rechargeTime)
        {
            _currentTime += Time.deltaTime;
        }
        else if (!_multipleBulletReload && _currentAmmo < _maxAmmo)
        {
            RecargaUna();
            if (_currentAmmo == _maxAmmo)
            {
                Cancela();
            }
            else
            {
                _currentTime = 0;
            }
        }
        else if(_currentAmmo < _maxAmmo)
        {
            RecargaTodas();
            Cancela();
        }
    }
}
