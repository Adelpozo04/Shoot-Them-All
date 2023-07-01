using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ImagePrefabComponent : MonoBehaviour
{
    [SerializeField]
    Button m_Button;
    Customization customManager;
    [SerializeField]
    private int _id;
    public int ID
    {
        get { return _id; }
    }
    public void SetId(int id)
    {
        _id = id;
    }
    
    public void CallChange()
    {
        Debug.Log("Llamada");
        customManager.SetHat(_id);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void SetCustommization(Customization cust)
    {
        customManager = cust;
    }
}
