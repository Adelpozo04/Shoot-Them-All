using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueArmaDashComponent : MonoBehaviour
{
    [SerializeField] GameObject _player;
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
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)            // Si la bala colisiona con otro jugador        
        {
            collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(5, _player);
        }
    
    }
}
