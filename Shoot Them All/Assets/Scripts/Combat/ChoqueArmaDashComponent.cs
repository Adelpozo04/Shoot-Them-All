using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueArmaDashComponent : MonoBehaviour
{
    #region properties

    [SerializeField] GameObject _player;
    private bool _inDash = false;

    #endregion

    #region methods

    public void ChangeDamageStage()
    {
        _inDash = !_inDash;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(_inDash);

        if (collision.gameObject.GetComponent<KnockbackComponent>() != null && _inDash)            // Si el arma colisiona con otro jugador        
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(5, _player);
        }
    
    }
}
