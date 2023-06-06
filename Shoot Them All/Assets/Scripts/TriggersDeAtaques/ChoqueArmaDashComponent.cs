using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueArmaDashComponent : MonoBehaviour
{
    #region properties
    //Cachear dede otro lado
    [SerializeField] GameObject _player;
    private Collider2D _collider;

    #endregion

    #region methods

    public void ChangeDamageStage(bool inDash)
    {
        _collider.enabled = inDash;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<CapsuleCollider2D>();
        _collider.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)            // Si el arma colisiona con otro jugador        
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(5, _player);
        }
    
    }
}
