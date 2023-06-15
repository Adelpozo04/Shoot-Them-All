using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsComponent : MonoBehaviour
{
    #region parameters
    //private int _roundWon;
    [SerializeField] private int _playerKillPoints;
    #endregion


    #region methods
    /// <summary>
    /// Puntos que otorgan las eliminaciones.
    /// </summary>
    /// <param name="points"></param>
    public void ChangeKillPoints(int points)
    {
        _playerKillPoints += points;
        Debug.Log(_playerKillPoints);
    }
    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _playerKillPoints = 0;
    }
}
