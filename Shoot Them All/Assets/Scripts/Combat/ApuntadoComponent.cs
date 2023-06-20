using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApuntadoComponent : MonoBehaviour
{
    #region parameters

    [Tooltip("Mide la distancia del centro al arma")]
    [SerializeField] private float _distance;
    public float Distance
    {
        set { _distance = value; }
    }

    #endregion

    #region References

    /// <summary>
    /// Contenedor del arma al que se le aplica la rotacion
    /// </summary>
    [SerializeField]
    private Transform _centroArmaTransform;

   
    [SerializeField]
    private Transform _armaTranform;
    public Transform ArmaTransform
    {
        set { _armaTranform = value; }
    }
    private Transform _myTransform;
    private AnimatorsManager _animatorsManager;

    #endregion

    #region properties

    private Vector2 _direction;

    #endregion

    #region methods

    /// <summary>
    /// Actualiza el valor de la direccion en relacion al input, se llama desde el input component
    /// </summary>
    public void Apuntado(InputAction.CallbackContext contex)
    {
        _direction = contex.ReadValue<Vector2>();
    }

    /// <summary>
    /// Rota el transform dado en funcion a la direccion en el eje z
    /// </summary>
    private void RotarTransform(Vector2 direccion, Transform _transform)
    {
        direccion = direccion.normalized;
        float k = Mathf.Atan2(direccion.y, direccion.x) * Mathf.Rad2Deg;

        if (k < 0)
        {
            k += 360;
        }

        _transform.rotation = Quaternion.Euler(0f, 0f, k);
        _animatorsManager.ChangeDirX(direccion.normalized.x);
        _animatorsManager.ChangeDirY(direccion.normalized.y);
    }

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        _myTransform = transform;
        //para separar el arma del centro
        _armaTranform.localPosition = new Vector3 (_distance, 0, 0);
        _animatorsManager = GetComponent<AnimatorsManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //rota el arma, si no hay input, se apunta hacia la derecha de forma predeterminada
        if(_direction != Vector2.zero)
        {
            _animatorsManager.SetInflence(1);
            RotarTransform(_direction * _myTransform.localScale.x,_centroArmaTransform);
        }
        else
        {
            _animatorsManager?.SetInflence(0);
            RotarTransform(Vector2.right,_centroArmaTransform);
        }       
    }
}
