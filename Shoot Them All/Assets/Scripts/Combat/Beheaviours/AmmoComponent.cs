using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoComponent : MonoBehaviour
{
    #region references
    #endregion

    #region parameters
    [SerializeField]
    private int _maxAmmo;
    private int _currentAmmo;

    [SerializeField]
    private float _rechargeTime;
    [SerializeField]
    private float _rechargeSlowdown;
    [Tooltip("Establece si recarga todas las balas a la vez (false) o de una en una (true)")]
    [SerializeField]
    private bool _multipleBulletReload;

    private float _currentTime;
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
        if (_currentAmmo < _maxAmmo)
        {
            enabled = true;
            _currentTime = 0;
        }
    }

    /// <summary>
    /// Condición de disparo
    /// </summary>
    /// <returns></returns>
    public bool PuedeDisparar()
    {
        return _currentAmmo > 0; //&& !enabled; Hay que discutir si podemos interrumpir la recarga para disparar
    }

    public void Dispara()
    {
        _currentAmmo--;
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _currentAmmo = _maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentTime < _rechargeTime)
        {
            _currentTime += Time.deltaTime;
        }
        else if (_multipleBulletReload && _currentAmmo < _maxAmmo)
        {
            RecargaUna();
            if (_currentAmmo == _maxAmmo)
            {
                enabled = false;
            }
            else
            {
                _currentTime = 0;
            }
        }
        else
        {
            RecargaTodas();
            enabled = false;
        }
    }
}
