using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBalaComponent : MonoBehaviour
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
            //Debug.Log("Choco con suelo");
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
