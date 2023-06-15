using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitComponent : MonoBehaviour
{

    enum Limite {Inferior, Lateral, Superior}

    #region parameters

    [SerializeField] private Limite _myLimit;

    #endregion

    #region methods

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Death _componenteMuerte = collision.gameObject.GetComponent<Death>();

        if (_componenteMuerte != null)
        {
            _componenteMuerte.SendPoints(LimitToPoint(_myLimit));
        }

        Destroy(collision.gameObject);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }

    private int LimitToPoint(Limite limit)
    {
        int points = 0;

        switch(limit)
        {
            case Limite.Inferior:
                points = 1; 
                break;

            case Limite.Lateral:
                points = 2;
                break;

            case Limite.Superior:
                points = 3; 
                break;
        }

        return points;
    }

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
