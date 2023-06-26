using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashComponent : MonoBehaviour
{
    #region parameters

    private float _enfriamiento;
    public float Enfriamiento
    {
        set{ _enfriamiento = value; }
    }
    #endregion


    #region properties

    private Rigidbody2D _myFatherRB;
    private ChoqueArmaDashComponent _choqueDash;
    private HorizontalComponent _horizontalComponent;
    private JumpComponent _jumpComponent;
    private EdgeComponet _edgeComponet;
    //colocar trail renderer en el centro del arma
    private TrailRenderer _trailRenderer;
    private GameObject _jugador;
    private bool _canDash = true;
    public bool CanDash
    {
        get { return _canDash; }
    }
    private float _elapsedTime = 0;


    #endregion

    void Start()
    {
        _jugador = transform.root.gameObject;

        _myFatherRB = _jugador.GetComponent<Rigidbody2D>();
        _horizontalComponent = _jugador.GetComponent<HorizontalComponent>();
        _jumpComponent = _jugador.GetComponent<JumpComponent>();
        _edgeComponet = _jugador.GetComponent<EdgeComponet>();

        _trailRenderer = transform.parent.GetComponent<TrailRenderer>();

        _choqueDash = GetComponent<ChoqueArmaDashComponent>();
    }



    public IEnumerator Dash(Vector2 direction, float dashingPower, float dashingTime)
    {

        _canDash = false;
        _horizontalComponent.enabled = false;
        _jumpComponent.enabled = false;
        _edgeComponet.enabled = false;
        _myFatherRB.velocity += direction * dashingPower * _horizontalComponent.SpeedToAcelerate;


        _trailRenderer.emitting = true;

        while (_elapsedTime < dashingTime)
        {
            _elapsedTime += Time.deltaTime;
            yield return null;
        }
        _horizontalComponent.enabled = true;
        _jumpComponent.enabled = true;
        _edgeComponet.enabled = true;
        _elapsedTime = 0;

        _myFatherRB.velocity = Vector2.zero;
        _trailRenderer.emitting = false;
        _choqueDash.ChangeDamageStage(false);
        yield return new WaitForSeconds(_enfriamiento);
        _canDash = true;
    }
}
