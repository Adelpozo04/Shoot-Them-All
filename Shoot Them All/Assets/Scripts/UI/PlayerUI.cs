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
    [SerializeField]
    TMP_Text _percentage;
    [SerializeField]
    Image _weaponIcon;
    public Image WeaponIcon
    {
        get { return _weaponIcon; }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _percentage.text = _playerPercentage.Percentage + "%";
    }

}
