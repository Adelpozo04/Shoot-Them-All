using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageComponent : Choque
{
    private Transform _weaponTransform;
    #region methods
    private Vector2 GetVectorFromAngle(float angle)
    {
        float angRadians = (angle * Mathf.PI) / 180;
        return new Vector2(Mathf.Cos(angRadians), Mathf.Sin(angRadians)).normalized;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null && !_stun)                   
        {
            Debug.Log("y tu padre essssss" + _playerFather);
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().
            ApplyConsecuencies(_damage, _weaponTransform.position - _playerFather.transform.position, _playerFather);
            _stun = false;
        }
        else if (collision.gameObject.GetComponent<KnockbackComponent>() != null)
        {
            collision.gameObject.GetComponent<StunComponent>().SetDirecction(_weaponTransform.position - _playerFather.transform.position);
            CallStun(collision.transform.root.GetComponent<StunComponent>());
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _weaponTransform = transform;
        _playerFather = transform.parent.parent.GetComponent<PointsComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
