using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRoundComponent : MonoBehaviour
{
    [SerializeField] private Text _myText;

    /// <summary>
    /// Método a llamar cuando se avance de ronda para editar el número de la misma (pasándole el número de ronda)
    /// </summary>
    /// <param name="newRound"></param>
    public void UpdateRound(int newRound)
    {
        _myText.text = "Round " + newRound;
    }
}
