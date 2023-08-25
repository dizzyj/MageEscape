
using UnityEngine;
using UnityEngine.AI;

public class Patroller : Enemy
{
    public Transform[] points;
    private int destPoint = 0;
    private EnemySight sight;
    private bool deathTriggered;
    

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        sight = gameObject.GetComponent<EnemySight>();
        agent.autoBraking = false;
        GotoNextPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (!myHealth.IsAlive())
        {
            if (!deathTriggered)
            {
                Die();
            }
            return;
        }
        if ( sight.isChasing)
        {
            anim.SetBool("IsChasing", true);
            if (!blocked)
            {
            GotoPlayer();
            }
        }
        else
        {
            anim.SetBool("IsChasing", false);
            if (!blocked&& !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
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
    public void Death()
    {
        Destroy(gameObject);
    }
    public override void Die()
    {
        deathTriggered = true;
        agent.isStopped = true;
        anim.SetTrigger("Death");
    }
    public override void StartMovement()
    {
        blocked = false;
        agent.isStopped = false;
    }
}
