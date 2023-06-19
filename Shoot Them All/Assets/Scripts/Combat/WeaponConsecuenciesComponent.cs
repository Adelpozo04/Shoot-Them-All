using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent (typeof(KnockbackComponent))]
[RequireComponent(typeof(PercentageComponent))]
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
    public void ApplyConsecuencies(int weaponDamage, Vector2 direction, PointsComponent damager)
    {
        _myPercentageComponent.AddDamage(weaponDamage);     // Se le aplica el daño del arma al jugador
        _myKnockBackComponent.Knockback(direction.normalized, _myPercentageComponent.Percentage); // Se le aplica al jugador el knockback del arma
        _myDeathComponent.ProcessDamage(damager);
        Debug.Log("El jugador: " + gameObject.name + "/ Daño recibido: " + weaponDamage + "/ Porcentaje: " + _myPercentageComponent.Percentage);
    }
}
