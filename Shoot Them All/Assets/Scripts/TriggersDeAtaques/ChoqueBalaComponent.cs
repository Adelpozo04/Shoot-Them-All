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
        GameObject gameObjectAux = collision.gameObject;

        if (aux == _floor || aux == _limit)
        {
            Destroy(gameObject);
        }
        if (gameObjectAux.GetComponent<KnockbackComponent>() != null && gameObjectAux.GetComponent<PointsComponent>() != _playerFather)            // Si la bala colisiona con otro jugador        
        {
            gameObjectAux.GetComponent<WeaponConsecuenciesComponent>().
            ApplyConsecuencies(_damage, GetComponent<Rigidbody2D>().velocity.normalized, _playerFather);
            if (_progresive)
            {
                CallProgresive(collision.GetComponent<ProgresiveDamage>());
            }
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
