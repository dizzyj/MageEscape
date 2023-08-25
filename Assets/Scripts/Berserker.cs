using UnityEngine;

public class Berserker : Enemy
{

    private bool deathTriggered = false;
    Vector3 lookPos;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        //agent.autoBraking = false;
        GotoPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if (attacking)
        {
            lookPos = playerTransform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
        }
        if (!myHealth.IsAlive())
        {
            if (!deathTriggered)
            {
                Die();
            }
            return;
        }
        if (!blocked)
        {
            base.GotoPlayer();
        }
    }
    public void Death()
    {
        Destroy(gameObject);
        //maybe random chance to spawn an apple or something for player health?

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
