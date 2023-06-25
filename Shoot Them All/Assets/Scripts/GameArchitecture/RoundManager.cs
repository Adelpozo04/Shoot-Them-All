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
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartRound()
    {
        for (int i = 0; i < GameManager.Instance.PlayersNumber; i++)
        {
            PlayerInputManager.instance.JoinPlayer(i, -1, pairWithDevice: Gamepad.all[i]);
        }
    }
    public void SetWeapon(WeaponScriptable weapon, int index)
    {
        _weaponsPrefabs[index] = weapon;
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
}
