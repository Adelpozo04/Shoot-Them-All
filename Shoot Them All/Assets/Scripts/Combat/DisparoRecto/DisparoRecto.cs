using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//cambiar el nombre de la clase por cañon
public class DisparoRecto : AttackGeneral
{
    [SerializeField]
    private DisparoRectoBeheaviour _disparoRecto;

    #region parameters
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

    private RaycastHit2D raycast;
    private Vector3 _raycastDir;


    private float _raycastDistance;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {

        _raycastDir = _weaponSpawnPoint.position - _playerFather.transform.position;

        raycast = Physics2D.Raycast(_playerFather.transform.position,_raycastDir , _raycastDistance, _floorLayer);

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

        //para guardar la distancia del raycast
        _raycastDir = _weaponSpawnPoint.position - _playerFather.transform.position;
        _raycastDistance = _raycastDir.magnitude;
    }

}
