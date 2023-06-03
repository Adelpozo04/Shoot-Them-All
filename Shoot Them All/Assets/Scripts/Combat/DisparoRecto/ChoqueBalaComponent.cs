using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueBalaComponent : MonoBehaviour
{

    #region methods

    public void OnCollisionEnter2D(Collision2D collision)
    {

        if(collision.gameObject.layer == LayerMask.NameToLayer("Floor") || collision.gameObject.layer == LayerMask.NameToLayer("Limit"))
        {
            Debug.Log("Choco con suelo");
            Destroy(gameObject);
        }
    }

    #endregion



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
