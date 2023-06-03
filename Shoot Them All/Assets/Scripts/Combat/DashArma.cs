using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashArma : AttackGeneral
{

    #region properties

    private Vector2 _direction;
    private DashJugador _dashJugador;

    #endregion

    #region methods

    public override void AtaquePrincipal()
    {
        _direction.x = Mathf.Cos(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad);
        _direction.y = Mathf.Sin(transform.parent.rotation.eulerAngles.z * Mathf.Deg2Rad);
        Debug.Log(_direction);
        _dashJugador.HacerDash(_direction);

    }

    #endregion





    void Start()
    {
        _dashJugador = transform.parent.parent.gameObject.GetComponent<DashJugador>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
