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

    #endregion







    // Start is called before the first frame update
    void Start()
    {
        _playerInput= GetComponent<PlayerInput>();
        _layer = LayerMask.GetMask("AreaApuntado");
    }

    // Update is called once per frame
    void Update()
    {
        
        _direction = _playerInput.actions["Apuntado"].ReadValue<Vector2>();
        Debug.Log(_direction);

        if(_direction != Vector2.zero)
        {
            Debug.Log("Pilla el input");
            transform.GetChild(1).GetComponent<RotarArma>().RotarLaArma(_direction);
        }
        


    }
}
