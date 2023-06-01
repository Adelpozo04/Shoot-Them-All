using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoRecto : MonoBehaviour
{

    #region parameters

    [Tooltip("Número de balas máximas del cargador")]
    [SerializeField] private int _maxBalas;

    #endregion

    #region references

    [SerializeField] private GameObject _bulletPrefab;

    #endregion

    #region properties

    private Transform _cañon;
    private GameObject bullet;
    private int _cartucho;

    #endregion

    #region methods

    public void AtaquePrincipal(InputAction.CallbackContext contex)
    {
        if (contex.performed && _cartucho > 0)
        {
            bullet = Instantiate(_bulletPrefab, _cañon.position, Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
            _cartucho--;
        }
        
    }

    public void AtaqueSecundario(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            Recargar();
        }
    }

    public void Recargar()
    {
        _cartucho = _maxBalas;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _cañon = transform.GetChild(0);
        _cartucho = _maxBalas;
    }

    // Update is called once per frame
    void Update()
    {
        



    }
}
