using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/SpawnManagerScriptableObject", order = 1)]
public class WeaponScriptable : ScriptableObject
{
    public GameObject Weapon;
    public int NumberInAnimator;
    public Sprite WeaponSprite;
}
