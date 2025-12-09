using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.ParticleSystem;

public class CharacterScript : MonoBehaviour
{
    public enum Weapons
    {
        None,
        Pistol,
        escopeta1,
        Gun,
        Bat,
        pipe
    }
    Weapons weapons = Weapons.None;
    [SerializeField] float movementSpeed = 5f;
    float currentSpeed;
    Rigidbody rb;
    Vector3 direction;
    [SerializeField] protected float attackDistance;
    [SerializeField] protected float cooldown;
    [SerializeField] protected float damage;
    [SerializeField] float shiftSpeed = 10f;
    [SerializeField] float jumpForce = 7f;
    bool isGrounded = true;
    [SerializeField] Animator anim;
    [SerializeField] int health = 100;
    public bool dead = false;
    bool Dead;
    GameObject Pistol, escopeta1, Gun, Bat, Pipe;
    bool isPistol, isescopeta1, isGun, isBat, isPipe;

    // Start is called before the first frame update
    public void ChangeHealth(int count)
    {
        health -= count;
        
        Invoke("RemoveDamageUI", 0.1f);
        if (health <= 0)
        {

            anim.SetBool("Die", true);
            dead = true;
            
            this.enabled = false;

        }
        void RemoveDamageUI()
        {
            
        }


    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        currentSpeed = movementSpeed;
        anim = GetComponent<Animator>();
        ChangeHealth(-100);
        
        

    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        direction = new Vector3(moveHorizontal, 0.0f, moveVertical);
        direction = transform.TransformDirection(direction);
        

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isGrounded = false;
            anim.SetBool("Jump", true);
            
        }
        



    }
    void FixedUpdate()
    {
        rb.MovePosition(transform.position + direction * currentSpeed * Time.deltaTime);
    }
    
    public void Getdamage(int count) 
    {
        
    }
    public void OnTriggerEnter(Collider other)
    {
        isGrounded = true;
        if (other.gameObject.CompareTag("Pistol"))
        {
            isPistol = true;
            Pistol.SetActive(true);
        }
        if (other.gameObject.CompareTag("Shotgun"))
        {
            isescopeta1 = true;
            escopeta1.SetActive(true);
        }
    }
    void OnCollisionEnter(Collision other)
    {
        

    }
}
