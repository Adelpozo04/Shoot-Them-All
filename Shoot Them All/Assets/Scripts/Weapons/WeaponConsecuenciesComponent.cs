using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConsecuenciesComponent : MonoBehaviour
{
    private KnockbackComponent _myKnockBackComponent;
    private PercentageComponent _myPercentageComponent;

    void Start()
    {
        _myKnockBackComponent = GetComponent<KnockbackComponent>();
        _myPercentageComponent = GetComponent<PercentageComponent>();
    }
    /// <summary>
    /// M�todo que se llama cuando el jugador recibe un ataque con toda la informaci�n del ataque
    /// </summary>
    /// <param name="weaponDamage"></param>
    /// <param name="weapon"></param>
    public void ApplyConsecuencies(int weaponDamage, GameObject weapon)
    {
        Debug.Log("El jugador: " + gameObject.name + "/ Da�o recibido: " + weaponDamage + "/ Porcentaje: " + _myPercentageComponent.Percentage);
        _myPercentageComponent.AddDamage(weaponDamage);     // Se le aplica el da�o del arma al jugador
        _myKnockBackComponent.Knockback(weapon, _myPercentageComponent.Percentage); // Se le aplica al jugador el knockback del arma
    }
}