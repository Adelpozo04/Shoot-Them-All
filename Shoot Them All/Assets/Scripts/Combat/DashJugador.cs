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
            Debug.Log("Deberia hacerlo");
            StartCoroutine(Dash(direction));
        }
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
        _myRB.velocity = direction * _dashingPower;
        _myTrailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashingTime);

        _myTrailRenderer.emitting = false;
        _weaponChoque.ChangeDamageStage();
        yield return new WaitForSeconds(_enfriamiento);
        _canDash = true;
    }
}
