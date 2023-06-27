using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimerComponent : MonoBehaviour
{
    private Text _myTimerText;
    private float _timer = 75;
    [SerializeField] private bool _timerCanCount = false;       // Quitar SerializeField de testeo

    // Update is called once per frame
    void Update()
    {
        if (_timerCanCount)
        {
            // Actualizamos el tiempo del contador
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                _timer = 0;
            }

            DisplayTimer(_timer);
        }
    }

    /// <summary>
    /// Método que muestra por pantalla el tiempo restante de partida, imporante EN PARTIDA
    /// </summary>
    /// <param name="myTime"></param>
    private void DisplayTimer(float myTime)
    {
        if (myTime < 0)
        {
            myTime = 0;
        }
        else if(myTime > 0)
        {
            myTime += 1;
        }

        float minutes = Mathf.FloorToInt(myTime / 60);
        float seconds = Mathf.FloorToInt(myTime % 60);

        _myTimerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    /// <summary>
    /// Método para indicar la duración de cada ronda
    /// </summary>
    /// <param name="newTimer"></param>
    public void SetRoundTimer(float newTimer)
    {
        _timer = newTimer;
    }

    private void Start()
    {
        _myTimerText = GetComponent<Text>();
    }
}
