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

    [Tooltip("Tiempo que debe pasar entre un disparo y otro")]
    [SerializeField] private float _enfriamiento;

    #endregion

    #region references

    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _bulletSpawnPoint;
    #endregion

    #region properties

    private GameObject bullet;
    private int _currentBullets;
    private float _elapsedTime;

    /// <summary>
    /// Indica si se puede disparar en relacion al ENFRIAMIENTO
    /// </summary>
    private bool _canShot = true;


    #endregion

    #region methods

    public void AtaquePrincipal(InputAction.CallbackContext contex)
    {
        if (contex.performed && _currentBullets > 0 && _canShot)
        {
            bullet = Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
            bullet.transform.rotation = transform.rotation;
            _currentBullets--;
            _canShot = false;
            _elapsedTime = 0;
        }
        
    }

    public void AtaqueSecundario(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            Recargar();
        }
    }

    private void Recargar()
    {
        _currentBullets = _maxBalas;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //cambiado por asignacion serializada mediante prefab, asi es mas escalable
        //_cañon = transform.GetChild(0);
        
        _currentBullets = _maxBalas;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_canShot)
        {
            if (_elapsedTime < _enfriamiento)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                //juraria que no hace falta pq ya se pone a 0 en ataque principal
                //_elapsedTime = 0;
                _canShot = true;
            }
        }
        


    }
}
