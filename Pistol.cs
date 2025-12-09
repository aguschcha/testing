using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapons
{
    protected override void OnShoot()
    {

        Vector3 rayStartPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        Ray ray = cam.GetComponent<Camera>().ScreenPointToRay(rayStartPosition);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            GameObject gameBullet = Instantiate(particle, hit.point, hit.transform.rotation);
            if (hit.collider.CompareTag("enemy"))
            {
                // Puedes cambiar el número 10 por lo que quieras. Esa es la cantidad de daño que causa una bala.
                //hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(10);
                hit.collider.gameObject.GetComponent<Enemy>().ChangeHealth(100);

            }
            else if (hit.collider.CompareTag("Player"))
            {
                hit.collider.gameObject.GetComponent<CharacterScript>().Getdamage(10);
            }
            Destroy(gameBullet, 1);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        cooldown = 0;
        auto = false;
        ammoCurrent = 10;
        ammoMax = 10;
        ammoBackPack = 70;
    }

    // Update is called once per frame

}
