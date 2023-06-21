using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SpinAttack : MonoBehaviour
{
    #region references
    private PolygonCollider2D _weaponCollider;
    #endregion

    #region parameters
    [SerializeField]
    private int _numberSpins;
    [SerializeField]
    private float _oscillationTime;

    [SerializeField]
    private float cooldownTime;
    private float coolTimer;

    private int _currentSpin;
    private float _currentTime;
    #endregion

    #region methods   
    public bool AttackCondition()
    {
        return coolTimer > cooldownTime;
    }

    public bool IsAttacking()
    {
        return _currentSpin <= _numberSpins;
    }

    public void StartSpin()
    {
        _currentSpin = 1;
        _currentTime = 0;
        coolTimer = 0;
        _weaponCollider.enabled = true;
        Debug.Log(_weaponCollider.enabled);
    }

    private void Oscillate()
    {
        if (_currentSpin == _numberSpins)
        {
            Debug.Log(_weaponCollider);
            _weaponCollider.enabled = false;
            _currentSpin++;
        }
        else
        {
            _currentSpin++;
            if (_currentSpin == _numberSpins - 1)
            {
                //Activa el metodo del knockback una vez esté
            }
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + 180);
        }
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _weaponCollider = GetComponent<PolygonCollider2D>();
        _currentSpin = _numberSpins + 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (_currentSpin > _numberSpins && coolTimer < cooldownTime)
        {
            coolTimer += Time.deltaTime;
        }
        else if (_currentTime < _oscillationTime * _currentSpin)
        {
            _currentTime += Time.deltaTime;
        } 
        else if (_currentSpin <= _numberSpins)
        {
            Oscillate();
        }
    }
}
