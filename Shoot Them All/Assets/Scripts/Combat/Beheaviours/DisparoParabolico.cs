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
        GameObject bullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);

        //igual hace falta hacer un parent component para casos como el de abajo
        //asignacion de quien proviene el daño
        if (bullet.GetComponent<ChoqueBalaComponent>() != null)
        {
            bullet.GetComponent<ChoqueBalaComponent>().SetPlayerFather(player);
        }
        if (bulletPrefab.GetComponent<ExplotionIgnition>() != null)
        {
            bullet.GetComponent<ExplotionIgnition>().SetPlayerFather(player);
        }
        //configuracion de lanzamiento
        bullet.transform.rotation = transform.rotation;
        bullet.GetComponent<Rigidbody2D>().AddForce(direction*force,ForceMode2D.Impulse);

        //if (player.transform.localScale.x < 0)
        //{
        //    bullet.transform.localEulerAngles += new Vector3(0, 0, 180);
        //}
        currentBullets--;
        elapsedTime = 0;
        return bullet;
    }
}
