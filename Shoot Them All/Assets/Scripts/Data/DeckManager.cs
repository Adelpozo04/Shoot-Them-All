using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    [SerializeField]
    static public List<WeaponScriptable> Deck;

    public void AddWeapon(WeaponScriptable weapon)
    {
        Deck.Add(weapon);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
