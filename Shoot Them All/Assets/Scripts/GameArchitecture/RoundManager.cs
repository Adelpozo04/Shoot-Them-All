using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject _playerUIPrefab;
    [SerializeField]
    private WeaponScriptable[]  _weaponsPrefabs;

    private int _roundNumber;


    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeNPlayers.AddListener(ChangeNPlayers);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void ChangeNPlayers(Int32 num)
    {
        WeaponScriptable[] aux = _weaponsPrefabs;
        _weaponsPrefabs = new WeaponScriptable[(int)num];
        for (int i = 0; i < Math.Min(aux.Length,_weaponsPrefabs.Length); i++)
        {
            _weaponsPrefabs[i] = aux[i];
        }
    }
    public void StartRound()
    {
        for (int i = 0; i < GameManager.Instance.PlayersNumber; i++)
        {
            try
            {
                PlayerInputManager.instance.JoinPlayer(i, -1, pairWithDevice: Gamepad.all[i]);
            }
            catch(ArgumentOutOfRangeException)
            {
                Debug.Log("No se detectaron mas mandos");
            }
        }
    }
    public void SetWeapon(float weapon, int index)
    {
        //_weaponsPrefabs[index] = weapon;
    }
    public void OnPlayerJoined(PlayerInput player)
    {
        player.transform.position = _spawnPoint.position;
        //instaciciacion del arma
        Transform weapon = Instantiate(_weaponsPrefabs[GameManager.Instance.playerList.Count].Weapon,player.transform.GetChild(1)).transform;
        player.GetComponent<ApuntadoComponent>().ArmaTransform = weapon;
        player.GetComponent<ApuntadoComponent>().Distance = _weaponsPrefabs[GameManager.Instance.playerList.Count]._weaponDistance;
        player.GetComponent<AnimatorsManager>().ChangeWeaponType(_weaponsPrefabs[GameManager.Instance.playerList.Count].NumberInAnimator);
        player.GetComponent<InputAtaques>().MiArmaActual = weapon.GetComponent<AttackGeneral>();

        //colocacion de la ui del jugador
        GameObject playerUI = Instantiate(_playerUIPrefab, GameManager.Instance.InfoPlayerTransform);
        playerUI.GetComponent<PlayerUI>().PlayerPercentage = player.GetComponent<PercentageComponent>();
        playerUI.GetComponent<PlayerUI>().WeaponIcon.sprite = _weaponsPrefabs[GameManager.Instance.playerList.Count].WeaponSpriteIcon;
        //TODO Añadir sprite de arma


        //Añadir jugaodr a la lista de jugadores del gameManager
        GameManager.Instance.playerList.Add(player);
    }
    public void OnPlayerLeft(PlayerInput player)
    {
        GameManager.Instance.playerList.Remove(player);
    }
    #region AuxMethods
    public void SetWeaponP1(Int32 num)
    {
        _weaponsPrefabs[0] = GameManager.Instance.AlWeaponsList[num];
    }
    public void SetWeaponP2(Int32 num)
    {
        if(_weaponsPrefabs.Length >= 2)
        _weaponsPrefabs[1] = GameManager.Instance.AlWeaponsList[num];
    }
    public void SetWeaponP3(Int32 num)
    {
        if (_weaponsPrefabs.Length >= 3)
            _weaponsPrefabs[2] = GameManager.Instance.AlWeaponsList[num];
    }
    public void SetWeaponP4(Int32 num)
    {
        if (_weaponsPrefabs.Length >= 4)
            _weaponsPrefabs[3] = GameManager.Instance.AlWeaponsList[num];
    }
    #endregion
}
