using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class LanzaPiñatas : AttackGeneral
{

    #region references
    [SerializeField] private GameObject _bulletPrefab;

    [SerializeField] private Transform _bulletSpawnPoint;

    [SerializeField] private DisparoParabolico _disparoParabolico;
    #endregion

    #region parameters

    [Tooltip("Número de balas máximas del cargador")]
    [SerializeField] private int _maxBalasInScreen;

    [Tooltip("Fuerza con la que sale la bala ")]
    [SerializeField] private float _force;
    #endregion


    #region properties

    private GameObject bullet;
    private Queue<ExplotionIgnition> _bullets;
    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        if(PriTimeCondition() && !WeaponWallDetector())
        {
            base.AtaquePrincipal();

            if (_bullets.Count >= _maxBalasInScreen)
            {
                _bullets.Dequeue().Explote();
            }

            GameObject proyectile = _disparoParabolico.PerfomShoot(_bulletPrefab, _playerPoints, _raycastDir,
                _bulletSpawnPoint.position, ref _timerPri, _force);
            proyectile.GetComponent<ExplotionIgnition>().SetDamage(_damagePri);
            _bullets.Enqueue(proyectile.GetComponent<ExplotionIgnition>());
        } 
    }

    public override void AtaqueSecundario()
    {
        base.AtaqueSecundario();

        while(_bullets.Count > 0)
        {
            if(_bullets.Peek() != null)
            {
                _bullets.Dequeue().Explote();
            }
            else
            {
                _bullets.Dequeue();
            }
            
        }

        Debug.Log(_bullets.Count + " Piñatas Explotadas");
    }


    #endregion
    // Start is called before the first frame update
    void Start()
    {
        StartMethod();
        _bullets = new Queue<ExplotionIgnition>();
    }

    // Update is called once per frame
    void Update()
    {
        RunTimerPri();
    }
}
