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
    private float _originalGravity;

    #endregion

    #region methods

    public void HacerDash(Vector2 direction)
    {


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
        _originalGravity = _myRB.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Dash(Vector2 direction)
    {
        _canDash = false;
        _myRB.gravityScale = 0f;
        _myRB.velocity = direction * _dashingPower;
        _myTrailRenderer.emitting = true;
        yield return new WaitForSeconds(_dashingTime);
        _myTrailRenderer.emitting = false;
        _myRB.gravityScale = _originalGravity;
        yield return new WaitForSeconds(_enfriamiento);
        _canDash = true;
    }
}
