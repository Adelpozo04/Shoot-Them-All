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

    //Se calcula la direcciom a la que va el dash a partir de la rotación del centro del arma 
    public override void AtaquePrincipal()
    {
        _direction = AngleToDirection();
        //Se pide al jugador que haga el dash
        _dashJugador.HacerDash(_direction);
        GetComponent<ChoqueArmaDashComponent>().ChangeDamageStage();

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
