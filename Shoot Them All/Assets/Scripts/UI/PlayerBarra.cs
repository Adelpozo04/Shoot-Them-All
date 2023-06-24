using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarra : MonoBehaviour
{
    #region properties

    [SerializeField] private GameObject _barraPrefab;
    [SerializeField] private PercentageComponent _percentageJugador;
    //Puntuacion rondas ganadas
    [SerializeField] private SpriteRenderer _colorJugador;
    //Iconos armas y habilidades
    [SerializeField] private Sprite _provisionalSprite; //SOLO PARA TESTEO 
    [SerializeField] private GameObject _canvas;

    private GameObject _barraJugador;

    #endregion





    // Start is called before the first frame update
    void Start()
    {
        _barraJugador = Instantiate(_barraPrefab, _canvas.transform);
        _barraJugador.GetComponent<BattleUIManager>().InicializacionBarra(_provisionalSprite, _provisionalSprite, _provisionalSprite, 2, 5, _colorJugador.color);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
