using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    private PercentageComponent _playerPercentage;
    public PercentageComponent PlayerPercentage
    {
        set { _playerPercentage = value; }
    }
    [SerializeField] private TMP_Text _percentage;

    [SerializeField] private Image _weaponIcon;
    public Image WeaponIcon
    {
        get { return _weaponIcon; }
    }

    [SerializeField] private Slider _roundsSlider;

    [SerializeField] private Image _abilitySliderLFront;
    [SerializeField] private Image _abilitySliderLBack;
    [SerializeField] private Image _abilitySliderRFront;
    [SerializeField] private Image _abilitySliderRBack;
    #region Getter

    public Image AbilitySliderLFront
    {
        get { return _abilitySliderLFront; }
    }
    public Image AbilitySliderLBack
    {
        get { return _abilitySliderLBack; }
    }
    public Image AbilitySliderRFront
    {
        get { return _abilitySliderRFront; }
    }
    public Image AbilitySliderRBack
    {
        get { return _abilitySliderRBack; }
    }
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _percentage.text = _playerPercentage?.Percentage + "%";
    }

}
