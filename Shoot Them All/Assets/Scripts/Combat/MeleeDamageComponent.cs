using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeDamageComponent : MonoBehaviour
{
    #region references
    private PointsComponent _playerPoints;
    private Transform _coreTransform;
    #endregion

    #region parameters
    private int _damage;
    #endregion

    #region methods
    private Vector2 GetVectorFromAngle(float angle)
    {
        float angRadians = (angle * Mathf.PI) / 180;
        return new Vector2(Mathf.Cos(angRadians), Mathf.Sin(angRadians)).normalized;
    }

    public void SetDamage(int damage)
    {
        _damage = damage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("y tu padre essssss" + collision);
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)                   
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().
                ApplyConsecuencies(_damage, GetVectorFromAngle(_coreTransform.rotation.eulerAngles.z), _playerPoints);
        }
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _playerPoints = transform.parent.parent.GetComponent<PointsComponent>();
        _coreTransform = transform.parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
