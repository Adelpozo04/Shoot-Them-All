using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApuntadoComponent : MonoBehaviour
{
    #region parameters

    [Tooltip("Mide la distancia del centro al arma")]
    [SerializeField] private float _distance;

    #endregion


    #region References

    /// <summary>
    /// Contenedor del arma al que se le aplica la rotacion
    /// </summary>
    [SerializeField]
    private Transform _centroArmaTransform;

   
    [SerializeField]
    private Transform _armaTranform;

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
    }


    #endregion


    // Start is called before the first frame update
    void Start()
    {
        //para separar el arma del centro
        _armaTranform.localPosition = new Vector3 (_distance, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {

        //rota el arma, si no hay input, se apunta hacia la derecha de forma predeterminada
        if(_direction != Vector2.zero)
        {
            RotarTransform(_direction,_centroArmaTransform);
        }
        else
        {
            RotarTransform(Vector2.right,_centroArmaTransform);
        }       
    }
}
