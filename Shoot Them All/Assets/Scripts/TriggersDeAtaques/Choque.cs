using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choque : MonoBehaviour
{
    protected PointsComponent _playerFather;
    protected int _damage;

    /// <summary>
    /// Setea al jugador que te ha golpeado en un componente de la propia bala 
    /// </summary>
    /// <param name="PlayerFather"></param>
    public void SetPlayerFather(PointsComponent PlayerFather)
    {
        _playerFather = PlayerFather;
    }
    public void SetDamage(int damage)
    {
        _damage = damage;
    }
}
