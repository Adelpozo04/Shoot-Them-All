using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopiarseYPegarse : MonoBehaviour
{

    #region methods

    public void CopyPaste(int manyTimes)
    {
        for(int i = 0; i < manyTimes - 1; i++)
        {
            Instantiate(gameObject, transform.parent);
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
