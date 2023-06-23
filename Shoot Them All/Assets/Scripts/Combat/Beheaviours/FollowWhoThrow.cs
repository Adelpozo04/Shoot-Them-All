using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWhoThrow : MonoBehaviour
{

    #region parameters

    [Tooltip("How fast the weapon is going to follow the player")]
    [SerializeField] private float _followingSpeed;

    [Tooltip("El tiempo que tarda el arma en regresar a su dueño")]
    [SerializeField] private float _tiempoRegreso;

    #endregion

    #region properties

    private GameObject _playerWhoThrow;
    private bool _following;
    public  bool Following
    {
        get { return _following; }
    }
    private Collider2D _myCollider;
    private Vector3 _direction;
    private Rigidbody2D _myRigidBody;
    private float _elapsedTime = 0;

    #endregion

    #region methods

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _playerWhoThrow)
        {
            collision.gameObject.BroadcastMessage("Recargar"); //Abierto al cambio
            Destroy(gameObject);
        }
    }

    public void RegisterPlayerWhoThrow(GameObject player)
    {
        Debug.Log("Registrado");
        _playerWhoThrow = player;
    }

    public void FollowPlayerWhoThrow()
    {
        Debug.Log("Llego");
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

        if (_elapsedTime < _tiempoRegreso)
        {
            _elapsedTime += Time.deltaTime;
        }
        else
        {
            FollowPlayerWhoThrow();
        }
        

    }
}
