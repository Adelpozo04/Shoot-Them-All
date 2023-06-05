using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBalaComponent : MonoBehaviour
{

    #region Properties

    private LayerMask _floor;
    private LayerMask _limit;
    private GameObject _playerFather;

    #endregion


    #region methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask aux = collision.gameObject.layer;

        if (aux == _floor || aux == _limit)
        {
            //Debug.Log("Choco con suelo");
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)            // Si la bala colisiona con otro jugador        
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(5, gameObject, _playerFather);
            Destroy(gameObject);
        }
    }
    
    #endregion



    void Start()
    {
        _floor = LayerMask.NameToLayer("Floor");
        _limit = LayerMask.NameToLayer("Limit");

        //Intento limites
        _playerFather = GetComponent<DisparoRecto>().BulletSpawnPoint.parent.transform.parent.transform.parent.gameObject;
    }

    
}