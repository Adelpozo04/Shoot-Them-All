using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarArma : MonoBehaviour
{

    #region methods

    public void RotarLaArma(Vector2 direccion)
    {
        direccion = direccion.normalized;
        float k = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;
        if (k < 0)
        {
            k += 360;
        }

        transform.Rotate(new Vector3(0f, 0f, k));

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
