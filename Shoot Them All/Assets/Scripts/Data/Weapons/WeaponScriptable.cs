using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Weapon", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public GameObject Weapon;
    public float _weaponDistance;
    public int NumberInAnimator;
    public Sprite WeaponSprite;
}