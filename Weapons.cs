using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class Weapons : itemController
{
    [SerializeField] protected GameObject particle;
    [SerializeField] protected GameObject cam;
    protected bool auto = false;
    protected float cooldown = 0;
    protected float timer = 0;
    protected int ammoCurrent;
    protected int ammoMax;
    protected int ammoBackPack;
    [SerializeField] TMP_Text ammoText;
    [SerializeField] AudioSource shoot;
    [SerializeField] AudioClip bulletSound, noBulletSound, reload;
    Transform sight;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;


    }
    protected override void Initialize() 
    {
        sight = GetComponentInChildren<Canvas>();
        sight.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
            timer += Time.deltaTime;
            if (Input.GetMouseButton(0))
            {
                Shoot();

            }
            AmmoTextUpdate();
            if (Input.GetKeyDown(KeyCode.R))
            {
                //Si nuestro cargador no está lleno, O si tenemos al menos una bala en las reservas, entonces
                if (ammoCurrent != ammoMax || ammoBackPack != 0)
                {
                    //Activar la función de recarga con un ligero retraso.
                    //Puedes configurar el retraso con cualquier número que desees
                    Invoke("Reload", 1);
                    shoot.PlayOneShot(reload);
                }
            }
        
    }
    protected virtual void OnShoot()
    {

    }
    public void Shoot()
    {
        if (Input.GetMouseButtonDown(0) || auto)
        {
            if (timer > cooldown)
            {
                if (ammoCurrent > 0)
                {
                    OnShoot();
                    timer = 0;
                    ammoCurrent = ammoCurrent - 1;
                    shoot.PlayOneShot(bulletSound);
                    shoot.pitch = Random.Range(1f, 1.5f);
                }
                else
                {
                    shoot.PlayOneShot(noBulletSound);
                }

            }
        }

    }
    private void AmmoTextUpdate()
    {
        ammoText.text = ammoCurrent + " / " + ammoBackPack;

    }
    private void Reload()
    {
        //declarando una variable y calculando el número de balas que debemos añadir al cargador
        int ammoNeed = ammoMax - ammoCurrent;
        //Si la cantidad de balas de reserva que tenemos es mayor o igual a la cantidad de balas necesarias para recargar, entonces
        if (ammoBackPack >= ammoNeed)
        {
            //restando el número de balas necesarias de las reservas
            ammoBackPack -= ammoNeed;
            //añadiendo el número necesario de balas al cargador
            ammoCurrent += ammoNeed;
        }
        //de lo contrario (si las reservas tienen menos balas de las necesarias para una recarga completa)
        else
        {
            //añadiendo toda nuestra munición de reserva al cargador
            ammoCurrent += ammoBackPack;
            //establecer la munición de reserva en 0
            ammoBackPack = 0;
        }
    }

}
