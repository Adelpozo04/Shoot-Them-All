using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashJugador : MonoBehaviour
{

    #region parameters

    [Tooltip("Tiempo para poder usar la habilidad otra vez")]
    [SerializeField] private float _enfriamiento;

    [Tooltip("Fuerza con la que se hace el dash")]
    [SerializeField] private float _dashingPower;

    [Tooltip("Tiempo en el que esta haciendo dash")]
    [SerializeField] private float _dashingTime;

    #endregion

    #region properties

    private Rigidbody2D _myRB;
    private bool _canDash = true;
    private TrailRenderer _myTrailRenderer;
    private float _elapsedTime = 0;
    

    #endregion

    #region references

    [SerializeField] private ChoqueArmaDashComponent _weaponChoque;

    #endregion

    #region methods

    public void HacerDash(Vector2 direction)
    {
        

        //Llamada a la corrutina
        if (_canDash)
        {
            
            StartCoroutine(Dash(direction));
        }
    }

    /// <summary>
    /// Metodo para frenar el movimiento del dash antes de tiempo
    /// </summary>
    public void StopDash()
    {
        
        _elapsedTime = _dashingTime;
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _myRB = GetComponent<Rigidbody2D>();
        _myTrailRenderer = GetComponent<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Corrutina que realiza el funcionamiento del dash, aplicando la fuerza sin gravedad por un tiempo limitado
    //e impidiendo volver ha hacer otro hasta el enfriamiento
    private IEnumerator Dash(Vector2 direction)
    {
        
        _canDash = false;

        if(transform.localScale.x < 0)
        {
            _myRB.velocity += -direction * _dashingPower;
        }
        else
        {
            _myRB.velocity += direction * _dashingPower;
        }
        
        _myTrailRenderer.emitting = true;
        Debug.Log(direction);

        while (_elapsedTime < _dashingTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }

        _elapsedTime = 0;

        _myRB.velocity = Vector2.zero;
        _myTrailRenderer.emitting = false;
        _weaponChoque.ChangeDamageStage(false);
        yield return new WaitForSeconds(_enfriamiento);
        _canDash = true;
    }
}
