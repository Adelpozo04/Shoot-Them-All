using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallKnockBackComponent : MonoBehaviour
{
    private Rigidbody2D _myrigidbody2d;
    [SerializeField] int percentage = 100;

    // Start is called before the first frame update
    void Start()
    {
        _myrigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.GetComponent<KnockbackComponent>() != null)
        {
            //collision.gameObject.GetComponent<KnockbackComponent>().Knockback(this.gameObject, percentage);
            Destroy(gameObject);
        }
    }
}
