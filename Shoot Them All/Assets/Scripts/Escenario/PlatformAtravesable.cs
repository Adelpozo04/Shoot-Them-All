using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformAtravesable : MonoBehaviour
{
    #region references
    private Collider2D _myCollider;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _myCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Physics2D.IgnoreCollision(collision, _myCollider, false);
    }
}
