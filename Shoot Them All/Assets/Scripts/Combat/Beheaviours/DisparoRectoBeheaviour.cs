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
                        Vector3 spawnPosition,ref int currentBullets,ref float elapsedTime,float speed)
    {
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        //asignacion de quien proviene el daño
        bullet.GetComponent<ChoqueBalaComponent>().SetPlayerFather(player);
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().velocity = direction.normalized * speed;

        if(player.transform.localScale.x < 0)
        {
            bullet.transform.localEulerAngles += new Vector3(0, 0, 180);
        }

        currentBullets--;
        elapsedTime = 0;
        return bullet;
    }
    public void Reload(ref int currentBullets, int maxBalas)
    {
        currentBullets = maxBalas;
    }
}
