using UnityEngine.AI;
using UnityEngine;

public class EnemyMage : Enemy
{
    public Transform[] points;  
    private int destPoint = 0;
    private EnemySight sight;
    private bool deathTriggered,chasing;
    public float range;
    public int damage;
    AudioSource sound;
    public AudioClip footStep;
    Vector3 lookPos;


    // Start is called before the first frame update
    public override void Start()
    {

        base.Start();
        sight = gameObject.GetComponent<EnemySight>();
        agent.autoBraking = false;
        sound = GetComponent<AudioSource>();
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        lookPos = playerTransform.position;
        lookPos.y = transform.position.y;

        if (attacking)
        {
            transform.LookAt(lookPos);
        }
        
        anim.SetFloat("Walking", anim.speed);
        if (!myHealth.IsAlive())
        {
            if (!deathTriggered)
            {
                deathTriggered = true;
                agent.isStopped = true;
                anim.SetTrigger("Death");
            }
            return;
        }
        if (!chasing && sight.isChasing)
        {
            chasing = sight.isChasing;
            anim.SetBool("IsChasing", true);
            GotoPlayer();
        }
        if(!sight.isChasing)
        {
            chasing = sight.isChasing;
            anim.SetBool("IsChasing", false);
            if (!blocked && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }
        }else
        {
            // check distance between mage and player
            float distance = Vector3.Distance(transform.position, playerTransform.position);
            if (distance <= range)
            {
                // stop mage movement
                attacking = true;
                StopMovement();
                // play attack animation
                anim.SetBool("Attack", true);
                //deal damage
                
            }
        }

    }

    void GotoNextPoint()
    {
        if (points.Length == 0)
        {
            return;
        }

        agent.destination = points[destPoint].position;

        destPoint = (destPoint + 1) % points.Length;
    }
    public void Attack()
    {
        //check if hit lands
        Vector3 origin = transform.position + transform.up;
        Vector3 destination = playerTransform.position + playerTransform.up;
        Vector3 dir = destination - origin;
        Debug.DrawRay(destination,dir,Color.cyan, 1.0f);
        if (Physics.Raycast(origin,dir, out RaycastHit hit)&& hit.distance <= range && hit.collider.CompareTag("Player"))
        {
            var player = playerTransform.GetComponent<Player>();

            if (player.Parry.IsParryActive())
            {
                myHealth.Damage(myHealth.GetHealth());
            }
            else
            {
                player.TakeDamage(damage);
            }
            
        }
        print("attack hit: "+hit.collider.gameObject.name);
    }
    public override void StartMovement()
    {
        print("StartMovement triggered");
        attacking = false;
        anim.SetBool("Attack", false);
        blocked = false;
        agent.isStopped = false;
    }
    public override void Die()
    {
        Destroy(gameObject);
    }
    public void Footstep()
    {
        sound.PlayOneShot(footStep);
    }
    
}
