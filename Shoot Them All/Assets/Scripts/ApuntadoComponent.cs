using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ApuntadoComponent : MonoBehaviour
{

    #region properties

    private PlayerInput _playerInput;
    private Vector2 _direction;
    private LayerMask _layer;
    private RaycastHit2D _weaponPosition;
    private RotarArma _rotarArma;

    #endregion


    public void Apuntado(InputAction.CallbackContext contex)
    {
        _direction = contex.ReadValue<Vector2>();
    }






    // Start is called before the first frame update
    void Start()
    {
        _playerInput= GetComponent<PlayerInput>();
        _layer = LayerMask.GetMask("AreaApuntado");
        _rotarArma = transform.GetChild(1).GetComponent<RotarArma>();
    }

    // Update is called once per frame
    void Update()
    {

        if(_direction != Vector2.zero)
        {
            _rotarArma.RotarLaArma(_direction);
        }
        else
        {
            _rotarArma.RotarLaArma(Vector2.right);
        }
        


    }
}
