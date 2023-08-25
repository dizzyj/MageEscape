
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    GameObject player;
    private Health playerHealth;
    private Animator anim;
    public float range;
    public int damage;
    public Vector3 origin;
    Enemy self;
    CapsuleCollider col;
    bool attacking;

    private AudioSource audio;

    public AudioClip AttackSoundEffect;
    // Start is called before the first frame update
    void Start()
    {
        col = GetComponent<CapsuleCollider>();
        origin = transform.TransformPoint(col.center);
        self = GetComponent<Enemy>();
        player = GameObject.FindWithTag("Player");
        playerHealth = player.GetComponent<Health>();
        anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        origin = transform.TransformPoint(col.center);
        if ( Physics.Raycast(origin,transform.forward,out RaycastHit hit, .75f))
        {
            if (!attacking && hit.collider.gameObject.CompareTag("Player") && playerHealth.IsAlive())
            {
                self.StopMovement();
                anim.SetBool("Attack", true);
                audio.clip = AttackSoundEffect;
                audio.PlayDelayed(.5f);
                self.attacking = attacking = true;
                
            }
        }
    }
    public void DetectHit()
    {
        if (Physics.Raycast(origin,transform.forward, out RaycastHit hit, range))
        {
            if (hit.collider.gameObject.CompareTag("Player") && playerHealth.IsAlive())
            {
                player.GetComponent<Player>().TakeDamage(damage);
            }
        }
        Debug.DrawRay(origin,transform.forward*range,Color.green,1f);
    }

    
    public void ResetAttack()
    {
        attacking = self.attacking = false;
        anim.SetBool("Attack", false);
        self.StartMovement();
    }
}
