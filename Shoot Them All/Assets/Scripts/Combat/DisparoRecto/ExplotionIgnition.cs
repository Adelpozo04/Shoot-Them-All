using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionIgnition : MonoBehaviour
{
    #region properties

    [SerializeField]
    private GameObject _explotionPrefab;

    #endregion


    #region methods

    public void Explote()
    {
        Instantiate(_explotionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
