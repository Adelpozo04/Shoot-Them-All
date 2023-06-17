using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionIgnition : MonoBehaviour
{

    #region parameters

    [Tooltip("Tiempo que tarda en desaparecer la explosion")]
    [SerializeField]
    private float _explotionTime;
    [Tooltip("Radio de efecto de la explision")]
    [SerializeField]
    private float _acitonRange = 1;
    #endregion

    #region properties

    [SerializeField]
    private Sprite _explotionSprite;

    [SerializeField]
    private ContactFilter2D _playerLayer;

    private Collider2D[] _results;
    private PointsComponent _playerFather;
    private Rigidbody2D _myRigidBody2D;
    private int _choquesExplosion;

    #endregion

    #region Get/Set

    public void SetPlayerFather(PointsComponent PlayerFather)
    {
        _playerFather = PlayerFather;
        //Intento limites
        Debug.Log(_playerFather.name);
    }

    #endregion


    #region methods

    public void Explote()
    {
        GetComponent<SpriteRenderer>().sprite = _explotionSprite;
        _myRigidBody2D.velocity = Vector2.zero;
        _myRigidBody2D.gravityScale = 0;
        _myRigidBody2D.constraints = RigidbodyConstraints2D.FreezeAll;
        StartCoroutine(StartExplotionDamage());
    }

    private IEnumerator StartExplotionDamage()
    {
        _choquesExplosion = Physics2D.OverlapCircle(transform.position, _acitonRange, _playerLayer, _results);

        Debug.Log(_choquesExplosion + "Explosion");

        for (int i = 0; i < _choquesExplosion; i++)
        {
            _results[i].gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(15, gameObject, _playerFather);
        }

        yield return new WaitForSeconds(_explotionTime);

        Destroy(gameObject);

        
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
        _results = new Collider2D[4];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _acitonRange);
    }
}
