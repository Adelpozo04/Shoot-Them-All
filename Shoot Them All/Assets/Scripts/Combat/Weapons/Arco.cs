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
        if (PriTimeCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            GameObject bullet = _disparoParabolicoBehaviour.PerfomShoot(_bulletPrefab, _playerPoints, _raycastDir,
                _myTransform.position, ref _timerPri, _force);
            bullet.GetComponent<ChoqueBalaComponent>().SetDamage(_damagePri);
        }
    }

    //TODO
    public override void AtaqueSecundario()
    {
        if (SecTimeCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();
            GameObject bullet = _disparoParabolicoBehaviour.PerfomShoot(_burningBulletPrefab, _playerPoints, _raycastDir,
                _myTransform.position, ref _timerSec, _force);
            bullet.GetComponent<ChoqueBalaComponent>().SetDamage(_damageSec);
        }
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
        RunTimerPri();
        RunTimerSec();
    }
}
