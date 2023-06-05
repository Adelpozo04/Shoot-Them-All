using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{

    #region parameters

    [Tooltip("Tiempo en el que se guarda el último atacante")]
    [SerializeField] private float _coolDown;

    #endregion

    #region properties

    private GameObject _damager;
    private float _elapsedTime;

    #endregion


    #region methods

    public void ProcessDamage(GameObject damager)
    {
        _damager = damager;
        _elapsedTime = 0;
    }



    public void SendPoints(int points)
    {
        if(_damager != null)
        {
            _damager.GetComponent<PointsComponent>().ChangeKillPoints(points);
        }
        else
        {
            GetComponent<PointsComponent>().ChangeKillPoints(-1);
        }
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_damager != null)
        {
            if (_elapsedTime < _coolDown)
            {
                Debug.Log(_elapsedTime);
                _elapsedTime += Time.deltaTime;
            }
            else
            {
                _damager = null;
            }
        }
        

    }
}
