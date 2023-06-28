using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D.Animation;

public class Customization : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        SpriteResolver[0].SetCategoryAndLabel(SpriteResolver[0].GetCategory(), HatName[index]);
        for(int i = 1; i < NamesParts.Length; i++)
        {
            SpriteResolver[i].SetCategoryAndLabel(SpriteResolver[i].GetCategory(), NamesParts[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        SpriteResolver[0].SetCategoryAndLabel(SpriteResolver[0].GetCategory(), HatName[index]);
    }
}
