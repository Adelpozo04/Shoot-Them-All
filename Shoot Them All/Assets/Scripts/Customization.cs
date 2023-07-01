using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.U2D.Animation;
using UnityEngine.UI;

public class Customization : MonoBehaviour
{
    [SerializeField]
    private SpriteLibraryAsset asset;
    //array de nombres de sombreros
    [SerializeField]
    private string[] HatName;
    //array auxiliar (se descartara mas adelante)
    [SerializeField]
    private string[] NamesParts;
    //indice del array de sombreros
    [SerializeField]
    int index;
    //Arary de sprite resolvers
    [SerializeField]
    private SpriteResolver[] SpriteResolver;
    [SerializeField]
    private Transform _panelTransform;
    [SerializeField]
    private GameObject _imagePrefab;
    [SerializeField]
    private List<Sprite> _spritesArray;


    public UnityEvent<int> SetImageInPlayer;
    // Start is called before the first frame update
    void Start()
    {
        HatName = asset.GetCategoryLabelNames("Hat").ToArray<string>();
        //obtencion de los sprites de la libreria
        for (int i = 0; i < HatName.Length; i++)
        {
            Image hatImage = Instantiate(_imagePrefab, _panelTransform).GetComponent<Image>();
            hatImage.sprite = asset.GetSprite("Hat", HatName[i]);
            hatImage.transform.GetChild(0).GetComponent<Image>().sprite = asset.GetSprite("Hat", HatName[i]); //agradeciria una mejora en este acceso no se hacerlo mejor ahora mismo
            hatImage.GetComponent<ImagePrefabComponent>().SetId(i);
            hatImage.GetComponent<ImagePrefabComponent>().SetCustommization(this);
            //_spritesArray.Add(asset.GetSprite("Hat", name));
        }

        //seteo del player inicial
        SpriteResolver[0].SetCategoryAndLabel(SpriteResolver[0].GetCategory(), HatName[index]);
        for(int i = 1; i < NamesParts.Length; i++)
        {
            SpriteResolver[i].SetCategoryAndLabel(SpriteResolver[i].GetCategory(), NamesParts[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //SpriteResolver[0].SetCategoryAndLabel(SpriteResolver[0].GetCategory(), HatName[index]);
    }
    public void SetHat(int index)
    {
        Debug.Log("Recibido");
        SpriteResolver[0].SetCategoryAndLabel(SpriteResolver[0].GetCategory(), HatName[index]);
    }
}
