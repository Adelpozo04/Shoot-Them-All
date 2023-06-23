using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    //Adapta la barra a los cambios que le emitan

    #region properties

    [SerializeField] private TMP_Text _percentageText;
    [SerializeField] private Image _armaIconoLugar;
    [SerializeField] private Image _armaHabilidad1Lugar;
    [SerializeField] private Image _armaHabilidad2Lugar;

    #endregion

    #region methods

    public void ChangePercentage(int newPercentage)
    {
        _percentageText.text = newPercentage.ToString() + "%";
    }

    public void SetIcons(Sprite arma, Sprite habilidad1, Sprite habilidad2)
    {
        _armaHabilidad1Lugar.sprite = arma;
        _armaHabilidad1Lugar.sprite = habilidad1;
        _armaHabilidad2Lugar.sprite = habilidad2;
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
