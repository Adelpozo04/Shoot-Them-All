using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWhoThrow : MonoBehaviour
{

    #region parameters

    [Tooltip("How fast the weapon is going to follow the player")]
    [SerializeField] private float _followingSpeed;

    #endregion


    #region properties

    private GameObject _playerWhoThrow;
    private bool _following;
    private Collider2D _myCollider;
    private Vector3 _direction;
    private Rigidbody2D _myRigidBody;

    #endregion



    #region methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject == _playerWhoThrow)
        {
            collision.gameObject.GetComponent<Hacha1>().Recargar();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _playerWhoThrow)
        {
            collision.gameObject.GetComponent<Hacha1>().Recargar();
            Destroy(gameObject);
        }
    }

    public void RegisterPlayerWhoThrow(GameObject player)
    {
        _playerWhoThrow = player;
    }

    public void FollowPlayerWhoThrow()
    {
        _myCollider.isTrigger = true;
        _following = true;

    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider= GetComponent<Collider2D>();
        _following = false;
        _myRigidBody = GetComponent<Rigidbody2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        //quiza esto se pueda hacer con un .Lerp pero por ahora asi
        if(_following)
        {
            _direction = (_playerWhoThrow.transform.position - transform.position).normalized * _followingSpeed;
            _myRigidBody.velocity = _direction;
        }

    }
}
