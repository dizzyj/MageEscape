using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    protected NavMeshAgent agent;
    protected bool blocked;
    protected Transform playerTransform;
    protected Animator anim;
    protected Health myHealth;
    protected KeyDrop drop;
    protected GameObject player;
    public bool attacking;
    // Start is called before the first frame update
    public virtual void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        myHealth = GetComponent<Health>();
        drop = GetComponent<KeyDrop>();
        playerTransform = FindObjectOfType<Player>().GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
    virtual public void Die()
    {
       
    }
    virtual public void TakeDamage(int damage)
    {
        myHealth.Damage(damage);
        Hit();
    }
    virtual public void DropItemChance()
    {

    }
    virtual public void StartMovement()
    {

    }
    public void StopMovement()
    {
        agent.velocity = Vector3.zero;
        agent.isStopped = true;
        blocked = true;
    }
    public void GotoPlayer()
    {
        agent.isStopped = false;
        if (playerTransform == null)
        {
            return;
        }
        agent.destination = playerTransform.position;
    }
    public virtual void Hit()
    {
        anim.SetTrigger("Hit");
    }
}
