using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalaRectaMovement : MonoBehaviour
{

    #region parameters

    [Tooltip("Velocidad que lleva la bala")]
    [SerializeField] private float _speed;

    #endregion

    #region properties

    private Rigidbody2D _myRB;

    #endregion



    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        _myRB.velocity = transform.up * _speed;
    }

    // Update is called once per frame
    void Update()
    {
        //_myRB.AddForce(transform.up * _speed * Time.deltaTime);
        
    }
}
