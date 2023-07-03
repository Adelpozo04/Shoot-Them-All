using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arco : AttackGeneral
{

    #region parameters
    [Tooltip("Fuerza con la que sale la bala")]
    [SerializeField] private float _force;
    #endregion

    #region references
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private GameObject _burningBulletPrefab;
    #endregion

    #region properties
    private DisparoParabolico _disparoParabolicoBehaviour;


    private int _currentBullets;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        if (ShootConditionPri() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            GameObject bullet = _disparoParabolicoBehaviour.PerfomShoot(_bulletPrefab, _playerPoints, _raycastDir,
                _myTransform.position, ref _currentBullets, ref _timerPri, _force);
            bullet.GetComponent<ChoqueBalaComponent>().SetDamage(_damagePri);
        }
    }

    //TODO
    public override void AtaqueSecundario()
    {
        if (ShootConditionSec() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            GameObject bullet = _disparoParabolicoBehaviour.PerfomShoot(_burningBulletPrefab, _playerPoints, _raycastDir,
                _myTransform.position, ref _currentBullets, ref _timerSec, _force);
            bullet.GetComponent<ChoqueBalaComponent>().SetDamage(_damageSec);
        }
    }

    /// <summary>
    /// Metodo para las condiciones de disparo normales del disparo recto
    /// </summary>
    /// <returns></returns>
    private bool ShootConditionPri()
    {
        return _timerPri > _coolDownPri;
    }
    private bool ShootConditionSec()
    {
        return _timerSec > _coolDownSec;
    }    
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _myTransform = transform;
        _disparoParabolicoBehaviour = GetComponent<DisparoParabolico>();
        _timerPri = 0;
        _timerSec = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (_timerPri < _coolDownPri)
        {
            _timerPri += Time.deltaTime;
        }
        if (_timerSec < _coolDownSec)
        {
            _timerSec += Time.deltaTime;
        }
    }
}
