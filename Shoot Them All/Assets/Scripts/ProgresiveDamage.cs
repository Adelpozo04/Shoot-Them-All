using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgresiveDamage : MonoBehaviour
{
    #region parameters
    [SerializeField] private float _tiempoHerido = 3;
    [SerializeField] private int _danoPorSegHerido = 5;
    private float _tiempoHeridoInicial, _contadorDeSegundos;
    private PercentageComponent _myPercentageComponent;
    #endregion

    public void IniciaDaņo()
    {
        _tiempoHerido = _tiempoHeridoInicial;
        _contadorDeSegundos = _tiempoHerido - 1f;
        if (enabled == false)
        {
            enabled = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _tiempoHeridoInicial = _tiempoHerido;
        _myPercentageComponent = GetComponent<PercentageComponent>();
        enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        _tiempoHerido -= Time.deltaTime;
        if (_tiempoHerido < _contadorDeSegundos)
        {
            _myPercentageComponent.AddDamage(_danoPorSegHerido);
            _contadorDeSegundos--;
        }
        if (_tiempoHerido <= 0)
        {
            enabled = false;
        }
    }
}
