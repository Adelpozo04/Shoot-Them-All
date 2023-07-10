using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

//cambiar el nombre de la clase por cañon
public class ArmaCañon : AttackGeneral
{

    #region parameters
    [Tooltip("Velocidad que lleva la bala ")]
    [SerializeField] private float _speed;

    //[SerializeField]
    //private bool _infiniteAmo;
    #endregion

    #region references
    [SerializeField] private GameObject _bulletPrefab;
    private AmmoComponent _myAmmo;
    private DisparoRectoBeheaviour _disparoRectoBehaviour;
    #endregion

    #region properties
    private float _elapsedTime;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        if(PriTimeCondition() && !WeaponWallDetector() && _myAmmo.PuedeDisparar())
        {
            base.AtaquePrincipal();
            _myAmmo.Dispara();
            GameObject bullet = _disparoRectoBehaviour.PerfomShoot(_bulletPrefab, _playerPoints, _raycastDir,
                _myTransform.position, ref _elapsedTime, _speed);
            bullet.GetComponent<ChoqueBalaComponent>().SetDamage(_damagePri);
        }
    }

    //TODO
    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();
        _myAmmo.Recargar();
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _myTransform = transform;
        _disparoRectoBehaviour = GetComponent<DisparoRectoBeheaviour>();
        _myAmmo = GetComponent<AmmoComponent>();
        _elapsedTime = 0;
    }


    // Update is called once per frame
    void Update()
    {
        RunTimerPri();
    }

}
