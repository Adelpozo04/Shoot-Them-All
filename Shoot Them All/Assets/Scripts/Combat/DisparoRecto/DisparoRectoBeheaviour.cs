using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoRectoBeheaviour : MonoBehaviour
{
    #region parameters
    //mas adelante estos parametros deberian ser cambiados por codigo igual hasta hacer un struct para estos
    [Tooltip("Número de balas máximas del cargador")]
    [SerializeField] private int _maxBalas;

    [Tooltip("Tiempo que debe pasar entre un disparo y otro")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Velocidad que lleva la bala ")]
    [SerializeField] private float _speed;

    [SerializeField]
    private LayerMask _floorLayer;
    #endregion

    #region references

    [SerializeField] private Transform _bulletSpawnPoint;
    #endregion
    #region properties
    private GameObject bullet;
    //se deberia hacer accesible para dar infomacíon de algun tipo
    private int _currentBullets;
    private float _elapsedTime;
    [SerializeField]
    private bool _infiniteAmo;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _elapsedTime = 0;
        _currentBullets = _maxBalas;
    }

    // Update is called once per frame
    void Update()
    {
        if (_elapsedTime < _enfriamiento)
        {
            _elapsedTime += Time.deltaTime;
        }
    }
    /// <summary>
    /// Metodo para las condiciones de disparo normales del disparo recto
    /// </summary>
    /// <returns></returns>
    public bool ShootCondition()
    {
        if (!_infiniteAmo)
        {
            return _currentBullets > 0 && _elapsedTime > _enfriamiento;
        }
        else
        {
            return _infiniteAmo && _elapsedTime > _enfriamiento;
        }
    }
    /// <summary>
    /// Instacia la bala del tipo <paramref name="bulletPrefab"/> y le da comportamiento
    /// de disparo recto
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="player"></param>
    public void PerfomShoot(GameObject bulletPrefab, PointsComponent player, Vector2 direction)
    {
        bullet = Instantiate(bulletPrefab, _bulletSpawnPoint.position, Quaternion.identity);
        //asignacion de quien proviene el daño
        bullet.GetComponent<ChoqueBalaComponent>().SetPlayerFather(player);
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * _speed;
        _currentBullets--;
        _elapsedTime = 0;
    }
    public void Reload()
    {
        _currentBullets = _maxBalas;
    }
}
