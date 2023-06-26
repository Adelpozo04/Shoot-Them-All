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
    public List<PlayerInput> playerList = new List<PlayerInput>();
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
}
