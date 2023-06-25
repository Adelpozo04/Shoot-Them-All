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
    [SerializeField] private CopiarseYPegarse _marcoRondas;
    [SerializeField] private Slider _sliderRondas;
    [SerializeField] private Image _barColor;
    // Referencia a los sliders para cuando tengamos que manipularlos en el futuro
    [SerializeField] private Slider _abilitySliderL;
    [SerializeField] private Slider _abilitySliderR;



    #endregion

    #region methods

    public void ChangePercentage(int newPercentage)
    {
        _percentageText.text = newPercentage.ToString() + "%";
    }

    public void InicializacionBarra(Sprite arma, Sprite habilidad1, Sprite habilidad2, int rondasGanadas, int numeroRondas, Color jugadorColor)
    {
        _armaIconoLugar.sprite = arma;

        _abilitySliderL.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = habilidad1;
        _abilitySliderL.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = habilidad1;

        _abilitySliderR.gameObject.transform.GetChild(0).GetComponent<Image>().sprite = habilidad2;
        _abilitySliderR.gameObject.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = habilidad2;

        _sliderRondas.maxValue = numeroRondas;
        _sliderRondas.value = rondasGanadas;
        _marcoRondas.CopyPaste(numeroRondas);
        _barColor.color = jugadorColor;
        _percentageText.text = "0%";
    }

    //Hacer el cooldown

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
