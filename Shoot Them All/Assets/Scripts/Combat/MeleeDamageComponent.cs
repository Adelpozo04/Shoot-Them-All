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
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)                   
        {
            Debug.Log("y tu padre essssss" + _playerFather);
            Debug.Log(_damage);
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().
                ApplyConsecuencies(_damage, _weaponTransform.position - _playerFather.transform.position, _playerFather);
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
