using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponConsecuenciesComponent : MonoBehaviour
{
    private KnockbackComponent _myKnockBackComponent;
    private PercentageComponent _myPercentageComponent;
    private Death _myDeathComponent;

    void Start()
    {
        _myKnockBackComponent = GetComponent<KnockbackComponent>();
        _myPercentageComponent = GetComponent<PercentageComponent>();
        _myDeathComponent = GetComponent<Death>();
    }
    /// <summary>
    /// Método que se llama cuando el jugador recibe un ataque con toda la información del ataque
    /// </summary>
    /// <param name="weaponDamage"></param>
    /// <param name="weapon"></param>
    //Este metodo esta obsoleto?
    public void ApplyConsecuencies(int weaponDamage, GameObject weapon)
    {
        Debug.Log("El jugador: " + gameObject.name + "/ Daño recibido: " + weaponDamage + "/ Porcentaje: " + _myPercentageComponent.Percentage);
        _myPercentageComponent.AddDamage(weaponDamage);     // Se le aplica el daño del arma al jugador
        _myKnockBackComponent.Knockback(weapon, _myPercentageComponent.Percentage); // Se le aplica al jugador el knockback del arma
    }
    public void ApplyConsecuencies(int weaponDamage, GameObject weapon, PointsComponent damager)
    {
        Debug.Log("El jugador: " + gameObject.name + "/ Daño recibido: " + weaponDamage + "/ Porcentaje: " + _myPercentageComponent.Percentage);
        _myPercentageComponent.AddDamage(weaponDamage);     // Se le aplica el daño del arma al jugador
        _myKnockBackComponent.Knockback(weapon, _myPercentageComponent.Percentage); // Se le aplica al jugador el knockback del arma
        _myDeathComponent.ProcessDamage(damager);
    }
}
