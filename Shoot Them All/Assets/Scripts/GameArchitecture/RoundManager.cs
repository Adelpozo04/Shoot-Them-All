using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class RoundManager : MonoBehaviour
{
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private GameObject _playerUIPrefab;
    private void Awake()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < GameManager.Instance.PlayersNumber; i++)
        {
            PlayerInputManager.instance.JoinPlayer(i, -1, pairWithDevice: Gamepad.all[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPlayerJoined(PlayerInput player)
    {
        player.transform.position = _spawnPoint.position;
        GameObject playerUI = Instantiate(_playerUIPrefab, GameManager.Instance.InfoPlayerTransform);
        playerUI.GetComponent<PlayerUI>().PlayerPercentage = player.GetComponent<PercentageComponent>();
        GameManager.Instance.playerList.Add(player);
    }
    public void OnPlayerLeft(PlayerInput player)
    {
        Debug.Log("Desconectado");
    }
}
