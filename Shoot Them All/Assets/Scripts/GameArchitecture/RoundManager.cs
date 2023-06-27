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
    private Transform[]  _spawnPoints;
    [SerializeField]
    private GameObject _playerUIPrefab;
    [SerializeField]
    private WeaponScriptable[]  _weaponsPrefabs;
    [SerializeField]
    private Color[] _playerColor;

    private int _roundNumber;


    // esto es para testeooooo
    [SerializeField]
    private List<Slider> _listaSliders = new List<Slider>();



    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.ChangeNPlayers.AddListener(ChangeNPlayers);
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
        player.transform.position = _spawnPoints[player.playerIndex].position;


        //el numero de elementos en la lista
        int playerListCount = GameManager.Instance.GetPlayerListCount();
        player.GetComponent<AnimatorsManager>().SpriteRenderer.color = _playerColor[playerListCount];

        //instaciciacion del arma
        Transform weapon = Instantiate(_weaponsPrefabs[playerListCount].Weapon,player.transform.GetChild(1)).transform;
        player.GetComponent<ApuntadoComponent>().ArmaTransform = weapon;
        player.GetComponent<ApuntadoComponent>().Distance = _weaponsPrefabs[playerListCount]._weaponDistance;
        player.GetComponent<AnimatorsManager>().ChangeWeaponType(_weaponsPrefabs[playerListCount].NumberInAnimator);
        player.GetComponent<InputAtaques>().MiArmaActual = weapon.GetComponent<AttackGeneral>();

        //colocacion de la ui del jugador
        GameObject playerUI = Instantiate(_playerUIPrefab, GameManager.Instance.InfoPlayerTransform);
        PlayerUI UI = playerUI.GetComponent<PlayerUI>();
        Debug.Log(UI);
        playerUI.GetComponent<Image>().color = _playerColor[playerListCount];

        UI.PlayerPercentage = player.GetComponent<PercentageComponent>();

        UI.WeaponIcon.sprite = _weaponsPrefabs[playerListCount].WeaponSpriteIcon;
        //Sprites de los cooldowns
        UI.AbilitySliderLBack.sprite = _weaponsPrefabs[playerListCount].HabiltyL;
        UI.AbilitySliderLFront.sprite = _weaponsPrefabs[playerListCount].HabiltyL;
        UI.AbilitySliderRBack.sprite = _weaponsPrefabs[playerListCount].HabilyR;
        UI.AbilitySliderRFront.sprite = _weaponsPrefabs[playerListCount].HabilyR;
        //TODO Añadir sprite de arma



        GameManager.Instance.AñadeJugador(player);
    }
    public void OnPlayerLeft(PlayerInput player)
    {
        GameManager.Instance.QuitaJugador(player);
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




    public void GanarRonda(PlayerInput ganador)
    {
        GameManager.Instance.IncrementaRondasGanadas(ganador);
        AumentaSlider(ganador);
    }


    //testeo
    public void AumentaSlider(PlayerInput ganador)
    {
        int index = GameManager.Instance.GetIndexOfPlayer(ganador);

        _listaSliders[index].value++;
    }

    //testeo
    public void AumentaSlider(int index)
    {
        _listaSliders[index].value++;
        Debug.Log("tu vieja");
    }

    #endregion
}
