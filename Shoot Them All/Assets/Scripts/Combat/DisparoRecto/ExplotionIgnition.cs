using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplotionIgnition : MonoBehaviour
{

    #region parameters

    [Tooltip("Tiempo que tarda en desaparecer la explosion")]
    [SerializeField]
    private float _explotionTime;
    #endregion

    #region properties

    [SerializeField]
    private Sprite _explotionSprite;

    [SerializeField]
    private ContactFilter2D _playerLayer;

    private Collider2D[] _results;
    private PointsComponent _playerFather;
    private Rigidbody2D _myRigidBody2D;

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
        StartExplotionDamage();
    }

    private IEnumerator StartExplotionDamage()
    {
        Physics2D.OverlapCircle(transform.position, transform.localScale.x, _playerLayer, _results);

        for(int i = 0; i < _results.Length; i++)
        {
            _results[i].gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(5, gameObject, _playerFather);
        }

        yield return new WaitForSeconds(_explotionTime);

        Destroy(gameObject);
        
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
