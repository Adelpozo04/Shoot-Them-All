using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueArrojadiza : MonoBehaviour
{
    #region Properties

    private LayerMask _floor;
    private LayerMask _limit;
    private Rigidbody2D _rigidbody;
    private PointsComponent _playerFather;
    private FollowWhoThrow _followWhoThrow;
    private int _damage;

    #endregion

    #region Get/Set
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
    #endregion

    #region methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        LayerMask aux = collision.gameObject.layer;
        //Al chocar con el suelo se para la bala
        if (aux == _floor)
        {
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector2.zero;
        }
        //Deteccion de golpeo normal
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null && 
            collision.GetComponent<PointsComponent>() != _playerFather && !_followWhoThrow.Following)            // Si la bala colisiona con otro jugador        
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().
                ApplyConsecuencies(_damage, _rigidbody.velocity.normalized, _playerFather);
            _rigidbody.velocity = Vector2.zero;
        }
    }

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        _followWhoThrow = GetComponent<FollowWhoThrow>();
        _floor = LayerMask.NameToLayer("Floor");
        _limit = LayerMask.NameToLayer("Limit");
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
