using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBarra : MonoBehaviour
{
    #region properties

    [SerializeField] private GameObject _barraPrefab;
    [SerializeField] private PercentageComponent _percentageJugador;
    //Puntuacion rondas ganadas
    //Color jugador
    //Iconos armas y habilidades
    [SerializeField] private Sprite _provisionalSprite; //SOLO PARA TESTEO 

    private GameObject _barraJugador;

    #endregion





    // Start is called before the first frame update
    void Start()
    {
        _barraJugador = Instantiate(_barraPrefab, transform.position, Quaternion.identity);
        _barraJugador.GetComponent<BattleUIManager>().InicializacionBarra(_provisionalSprite, _provisionalSprite, _provisionalSprite, 2, 5, Color.yellow);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
