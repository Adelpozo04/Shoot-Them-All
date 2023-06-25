using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderCooldown : MonoBehaviour
{
    private Slider _mySlider;
    private float _weaponMaxCooldown;           // Cooldown de cada arma
    private float _sliderCooldown = 0;

    /// <summary>
    /// El arma le pasa el cooldown que deberá tener la habilidad
    /// </summary>
    /// <param name="maxValue"></param>
    public void InicializaSlider(float maxValue)
    {
        _weaponMaxCooldown = maxValue;
        _mySlider.maxValue = maxValue;              // Inicializamos el slider adaptado al cooldown del arma
        _mySlider.value = maxValue;
    }
    /// <summary>
    /// Resetea el slider para empezar a contar de nuevo
    /// </summary>
    public void ResetCooldown()
    {
        _sliderCooldown = 0;                        // Valor del slider = 0
    }
    /// <summary>
    /// Actualiza el estado del slider
    /// </summary>
    /// <param name="maxCooldown"></param>
    /// <param name="sliderCooldown"></param>
    private void UpdateSlider()
    {
        _mySlider.value = _sliderCooldown;
    }

    private void Start()
    {
        _mySlider = GetComponent<Slider>();
    }
    private void Update()
    {
        if (_sliderCooldown <= _weaponMaxCooldown)
        {
            _sliderCooldown += Time.deltaTime;      // Aumenta el valor del slider
        }
    }
}
