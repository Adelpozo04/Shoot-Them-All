using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    #region parameters

    [Tooltip("Tiempo en el que se guarda el último atacante")]
    [SerializeField] private float _coolDown = 10;

    private PointsComponent _myPointsComponent;

    #endregion

    #region properties

    private PointsComponent _damager;
    private float _elapsedTime;

    #endregion


    #region methods
    /// <summary>
    /// Recoge quien hace 
    /// </summary>
    /// <param name="damager"></param>
    public void ProcessDamage(PointsComponent damager)
    {
        _damager = damager;
        _elapsedTime = 0;
    }


    /// <summary>
    /// Decide cuantos puntos se ganan o pierden
    /// </summary>
    /// <param name="points"></param>
    public void SendPoints(int points)
    {
        if(_damager != null)
        {
            _damager.ChangeKillPoints(points);
        }
        else
        {
            _myPointsComponent.ChangeKillPoints(-1);
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myPointsComponent = GetComponent<PointsComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Se puede compactar en un solo if
        if(_damager != null)
        {
            if (_elapsedTime < _coolDown)
            {
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                _damager = null;
            }
        }
        

    }
}
