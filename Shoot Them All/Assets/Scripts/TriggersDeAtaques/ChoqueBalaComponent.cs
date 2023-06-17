using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBalaComponent : MonoBehaviour
{

    #region Properties

    private LayerMask _floor;
    private LayerMask _limit;
    private PointsComponent _playerFather;

    #endregion

    #region Get/Set
    /// <summary>
    /// Setea al jugador que te ha golpeado en un componente de la propia bala 
    /// </summary>
    /// <param name="PlayerFather"></param>
    public void SetPlayerFather(PointsComponent PlayerFather)
    {
        _playerFather = PlayerFather;
        //Intento limites
        Debug.Log(_playerFather.name);
    }
    #endregion

    #region methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask aux = collision.gameObject.layer;

        if (aux == _floor)
        {
            //Debug.Log("Choco con suelo");
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null && collision.GetComponent<PointsComponent>() != _playerFather)            // Si la bala colisiona con otro jugador        
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
    }  
}
