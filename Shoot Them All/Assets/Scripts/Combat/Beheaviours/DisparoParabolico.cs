using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoParabolico : MonoBehaviour
{
    //posiblemente se pueda sobrecargar

    /// <summary>
    /// Instacia la bala del tipo <paramref name="bulletPrefab"/> y 
    /// le da comportamiento de disparo parabolico
    /// <para></para>
    /// Devuelve la referencia a la bala instanciada
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="player"></param>
    public GameObject PerfomShoot(GameObject bulletPrefab, PointsComponent player, Vector2 direction,
                        Vector3 spawnPosition, ref int currentBullets, ref float elapsedTime, float force)
    {
        Debug.Log("instanciando " + bulletPrefab);
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        Debug.Log("instanciado " + bullet);
        bullet.GetComponent<Choque>().SetPlayerFather(player);
        //configuracion de lanzamiento
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction*force,ForceMode2D.Impulse);

        currentBullets--;
        elapsedTime = 0;
        return bullet;
    }
}
