using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AtaqueMelee : MonoBehaviour
{
    #region references
    private GameObject _myWeapon;
    private RotarArma _myRotation;
    #endregion

    #region parameters
    private bool _isAttacking;
    private float _currentAngle;
    private float _nextAngle;
    private float _endAngle;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackSpeed;
    #endregion

    #region methods
    private void UpdateMesh()
    {
        _currentAngle = _nextAngle;
        _nextAngle = _nextAngle + attackSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, _nextAngle);
        if (_nextAngle > _endAngle)
        {
            _isAttacking = false;
        }
    }

    public void Attack(InputAction.CallbackContext contex)
    {
        if (contex.performed)
        {
            _nextAngle = transform.rotation.z - attackRange / 2;
            transform.rotation = Quaternion.Euler(0, 0, _nextAngle);
            _endAngle = transform.rotation.z + attackRange / 2;
            _isAttacking = true;
            _myRotation.enabled = false;
        }    
    }
    #endregion

    void Start()
    {
        _myRotation = gameObject.GetComponent<RotarArma>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_isAttacking) 
        {
            UpdateMesh();
        }
    }
}
