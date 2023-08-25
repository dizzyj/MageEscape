using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{

    [FormerlySerializedAs("movementSpeed")] public float moveForwardSpeed;
    public float moveBackwardSpeed;
    public float strafeSpeed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float stealthSpeed;
    public PlayerParry Parry { get; private set; }
    private Vector3 velocity;
    public bool blockMovement;
    public bool crouching, dead;
    //private bool inAir;
    //Rigidbody rb;
    public GameObject target;
    private Animator animator;
    private Health myHealth;
    public Transform spawnPoint;
    HUD hud;
    private AudioSource audio;
    
    void Start()
    {
        
        Parry = GetComponentInChildren<PlayerParry>();
        audio = GetComponent<AudioSource>();
        hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        //rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        //init health
        myHealth = GetComponent<Health>();
        myHealth.Damage(15, false);
        UpdateHealthBar();
    }

    void Update()
    {
        if (dead && Input.GetKeyDown(KeyCode.E))
        {
            Respawn();
        }
        if (!dead && !myHealth.IsAlive())
        {
            dead = true;
            // Death animation and respawn
            blockMovement = true;
            animator.SetBool("Dead",true);
            //would be nice to rotate camera around dead body
            //animation should trigger respawn
        }

        var movement = new Vector3();
        if (Input.GetKeyDown(KeyCode.C))
        {
            crouching = !crouching;
            animator.SetBool("Crouching", crouching);
            print("crouching ="+ crouching);
        }
        if (Input.GetKey(KeyCode.W)&& !crouching)
        {
            movement += new Vector3(0f, 0f, moveForwardSpeed);
        }else if (Input.GetKey(KeyCode.W) && crouching)
        {
            movement += new Vector3(0f, 0f, stealthSpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            movement -= new Vector3(strafeSpeed, 0f);
        } 
        
        if (Input.GetKey(KeyCode.D))
        {
            movement += new Vector3(strafeSpeed, 0f);
        } 
        
        if (Input.GetKey(KeyCode.S) && !crouching)
        {
            movement -= new Vector3(0f, 0f, moveBackwardSpeed);
        }else if (Input.GetKey(KeyCode.S) && crouching)
        {
            movement -= new Vector3(0f, 0f, stealthSpeed);
        }

        // Moving forward and to the side should not be any faster then moving just forward. Same with moving backwards.
        if (movement.z > 0.001 && movement.magnitude > moveForwardSpeed)
        {
            movement.Normalize();
            movement *= moveForwardSpeed;
        } 
        else if (movement.z < 0.001 && movement.magnitude > moveBackwardSpeed)
        {
            movement.Normalize();
            movement *= moveBackwardSpeed;
        }
        
        // This lerp adds some floaty-ness to the character.
        velocity = Vector3.Lerp(velocity, movement, 1.5f);
        MoveCharacter(velocity);    
    
        var rotation = Input.GetAxis("Mouse X") * rotationSpeed;
        RotateCharacter(rotation, movement);
    }

    private void MoveCharacter(Vector3 movement)
    {
        if (blockMovement)
        {
            return;
        }
        movement *= Time.deltaTime;
        transform.Translate(movement);
        animator.SetFloat("WalkSpeed", movement.z);
        animator.SetFloat("StrafeSpeed", movement.x);

    }
    public void KOTarget(GameObject target)
    {
        if (crouching)
        {
            this.target = target;
            blockMovement = true;
            animator.SetBool("KO", true);
            audio.PlayDelayed(0.5f);
        }
        
    }
    public GameObject GetTarget()
    {
        return target;
    }
    public Animator GetAnimator()
    {
        return animator;
    }
    private void RotateCharacter(float horizontalRotation, Vector3 movement)
    {
        transform.Rotate(Vector3.up, horizontalRotation);

        // We counter-rotate the actual character model when stationary to allow the player to look around without 
        // moving.
        if (movement.magnitude > 0.001)
        {
            animator.transform.rotation = Quaternion.Lerp(animator.transform.rotation,transform.rotation, 0.5f);
        }
        else
        {
            animator.transform.Rotate(Vector3.up, -horizontalRotation);
        }
    }
    public void GetLvlRespawn()
    {
        spawnPoint = GameObject.FindGameObjectWithTag("Respawn").transform;
    }
    public void Respawn()
    {
        GetLvlRespawn();
        if (hud == null)
        {
            hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        }
        animator.SetBool("Dead", false);
        blockMovement = false;
        transform.position = spawnPoint.transform.position;
        myHealth.Heal(5);
        UpdateHealthBar();
        hud.UIRespawn();
        dead = false;
    }
    public void UpdateHealthBar()
    {
        if (hud == null)
        {
            hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        }
        hud.UpdateHealthBar((float)myHealth.GetHealth() / (float)myHealth.maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (hud == null)
        {
            hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        }
        myHealth.Damage(damage);
        hud.UpdateHealthBar((float)myHealth.GetHealth() / (float)myHealth.maxHealth);
        //do an animation prob
    }
    public void DeathUI()
    {
        if (hud == null)
        {
            hud = GameObject.FindWithTag("HUD").GetComponent<HUD>();
        }
        hud.UIDeath();
        dead = true;
    }
}
        