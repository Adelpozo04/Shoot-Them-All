using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsComponent : MonoBehaviour
{
    #region parameters

    private int _roundWon;

    private int _playerKillPoints;

    #endregion


    #region methods

    public void ChangeKillPoints(int points)
    {
        _playerKillPoints += points;
        Debug.Log("Menus uno campeon");
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _playerKillPoints = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
