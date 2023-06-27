using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    static private GameManager _instance = null;
    static public GameManager Instance
    {
        get { return _instance; }
    }
    [SerializeField]
    private int _playersNumber;//hacer estatico
    public int PlayersNumber
    {
        get { return _playersNumber; }
    }

    [SerializeField]
    private Transform _infoPlayerTransform;
    public Transform InfoPlayerTransform
    {
        get { return _infoPlayerTransform; }
    }

    //lista de los jugadores
    private List<PlayerInput> playerList = new List<PlayerInput>();

    //lista de las rondas ganadas de cada jugador, los indices se corresponden con la lista de jugadores
    private List<int> rondasGanadasJugadores = new List<int>();


    public List<WeaponScriptable> AlWeaponsList = new List<WeaponScriptable>();

    [HideInInspector]
    public UnityEvent<Int32> ChangeNPlayers;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != null)
        {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        //ChangeNPlayers.AddListener(SetPlayerNum);
    }

    // Update is called once per frame
    void Update()
    {
        //foreach (InputDevice Device in playerList[0].devices)
        //{
        //    Debug.Log(Device.name);
        //}
    }
    public void SetPlayerNum(Int32 num)
    {
        ChangeNPlayers?.Invoke(num + 1);
        _playersNumber = num + 1;
    }



    #region Añadir y quitar jugador

    public void AñadeJugador(PlayerInput player)
    {
        playerList.Add(player);
        rondasGanadasJugadores.Add(0);
    }

    public void QuitaJugador(PlayerInput player)
    {
        int index = playerList.IndexOf(player);

        playerList.RemoveAt(index);
        rondasGanadasJugadores.RemoveAt(index);
    }

    #endregion


    public void IncrementaRondasGanadas(PlayerInput player)
    {
        int index = playerList.IndexOf(player);

        rondasGanadasJugadores[index]++;
    }

    public int GetPlayerListCount()
    {
        return playerList.Count;
    }

    public int GetIndexOfPlayer(PlayerInput player)
    {
        return playerList.IndexOf(player);
    }

   
}
