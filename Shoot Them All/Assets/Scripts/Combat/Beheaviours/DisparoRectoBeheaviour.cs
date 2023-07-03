using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoRectoBeheaviour : MonoBehaviour
{
    /// <summary>
    /// Instacia la bala del tipo <paramref name="bulletPrefab"/> y le da comportamiento
    /// de disparo recto
    /// Devuelve la referencia a la bala instanciada
    /// </summary>
    /// <param name="bulletPrefab"></param>
    /// <param name="player"></param>
    public GameObject PerfomShoot(GameObject bulletPrefab, PointsComponent player, Vector2 direction,
                        Vector3 spawnPosition, float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        //asignacion de quien proviene el daño
        bullet.GetComponent<Choque>().SetPlayerFather(player);
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;

        if(player.transform.localScale.x < 0)
        {
            bullet.transform.localEulerAngles += new Vector3(0, 0, 180);
        }
        return bullet;
    }
}
