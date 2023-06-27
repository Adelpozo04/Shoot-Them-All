using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorreccionDireccion : MonoBehaviour
{
    #region properties

    private Vector3 _originPosition;
    private Rigidbody2D _myRB;

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _originPosition = transform.position;
        _myRB= GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 _moveDirection = _myRB.velocity;

        if (_moveDirection != Vector3.zero )
        {
            float angle = Mathf.Atan2(_moveDirection.y, _moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
