using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBalaComponent : Choque
{

    #region Properties

    private LayerMask _floor;
    private LayerMask _limit;
    #endregion

    #region methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask aux = collision.gameObject.layer;

        if (aux == _floor || aux == _limit)
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null && collision.GetComponent<PointsComponent>() != _playerFather)            // Si la bala colisiona con otro jugador        
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().
                ApplyConsecuencies(_damage, GetComponent<Rigidbody2D>().velocity.normalized, _playerFather);
            Destroy(gameObject);
        }
    }
    
    #endregion



    void Start()
    {
        _floor = LayerMask.NameToLayer("Floor");
        _limit = LayerMask.NameToLayer("Limit");
    }  
}
