using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentageComponent : MonoBehaviour
{
    [SerializeField]
    private int percentage = 0;

    public int Percentage { get { return percentage; } }

    public void AddDamage(int damage)
    {
        percentage += damage;
    }
}
