using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoqueArmaMeleComponent : MonoBehaviour
{
    [SerializeField] GameObject _weapon;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)            // Si el ataque mele colisiona con otro jugador        
        {
            //collision.gameObject.GetComponent<WeaponConsecuenciesComponent>().ApplyConsecuencies(5, _weapon);
        }
    }
}
