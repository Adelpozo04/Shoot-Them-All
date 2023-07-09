using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minicamara : MonoBehaviour
{
    #region References

    [SerializeField]
    private Camera _myCamera;


    [SerializeField]
    private Image[] _bordes = new Image[4];

    #endregion

    #region Parameters


    public bool _muestraCamara;

    [SerializeField]
    private Color _colorBordes;


    #endregion	

    #region Properties




    #endregion	

    #region Methods

    #region Unity Methods
	
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Canvas>().worldCamera = _myCamera;
        _muestraCamara = false;
        SetColorBordes(_colorBordes);
    }

    // Update is called once per frame
    void Update()
    {
        //cambiar por mostrar solo cuando esta fuera del mapa
        _myCamera.gameObject.SetActive(_muestraCamara);       
    }

    private void SetColorBordes(Color color)
    {
        Color aux = new Color(color.r, color.g, color.b,1);

        for(int i  =0;i < _bordes.Length; i++)
        {
            if (_bordes[i] != null)
            {
                _bordes[i].color = aux;
            }
        }
    }

    #endregion	
  
    #endregion	
}

