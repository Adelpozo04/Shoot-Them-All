using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choque : MonoBehaviour
{
    protected PointsComponent _playerFather;
    protected int _damage;
    [SerializeField]
    protected bool _progresive;
    [SerializeField]
    protected bool _stun = false;
    public bool Stun
    {
        set { _stun = value; }
    }

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
    protected void CallProgresive(ProgresiveDamage objetive)
    {
        if(_progresive)
        {
            objetive.IniciaDaño();
        }
    }
    protected void CallStun(StunComponent objective)
    {
        if (_stun)
        {
            StartCoroutine(objective.Stun(3));
        }
    }
}
