using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class DisparoRecto : AttackGeneral
{
    [SerializeField]
    DisparoRectoBeheaviour _disparoRecto;
    #region parameters
    //esta distancia es calculable
    [Tooltip("Distancia a la que se spawnea la bala (para comprobar que no atraviese paredes")]
    [SerializeField]
    private float _distancia;

    [SerializeField]
    private LayerMask _floorLayer;

    [SerializeField]
    private PointsComponent _playerFather;
    #endregion

    #region references
    [SerializeField] private Transform _weaponSpawnPoint;
    [SerializeField] private GameObject _bulletPrefab;
    #endregion

    #region properties
    RaycastHit2D raycast;
    Vector3 _raycastDir;

    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        raycast = Physics2D.Raycast(_playerFather.transform.position,_raycastDir , _raycastDir.magnitude, _floorLayer);

        //Debug.DrawRay(transform.position, new Vector3(AngleToDirection().x, AngleToDirection().y,0), Color.red, 5);

        if(_disparoRecto.ShootCondition() && !raycast)
        {
            base.AtaquePrincipal();
            _disparoRecto.PerfomShoot(_bulletPrefab, _playerFather, _raycastDir);
        }
    }
    //TODO
    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();
        _disparoRecto.Reload();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _animatorsManager = GetComponentInParent<AnimatorsManager>();
        _floorLayer = LayerMask.GetMask("Floor");
    }

    // Update is called once per frame
    void Update()
    {
        _raycastDir = _weaponSpawnPoint.position - _playerFather.transform.position;
    }
}
