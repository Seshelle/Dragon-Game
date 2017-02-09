using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class SpiderAI : MonoBehaviour
{

    public Transform goal;
    public string attackAnim = "attack1";
    public float health = 10;

    private NavMeshAgent agent;
    private Animation anim;
    private CapsuleCollider hitbox;

    private bool dead = false;
    private bool attacking = false;
    private float attackDelay;
    private float attackLength;
    private float stunTime = 0;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animation>();
        hitbox = GetComponent<CapsuleCollider>();
        attackLength = anim.GetClip(attackAnim).length;
    }

    void Update()
    {
        if (!dead && !attacking && stunTime <= 0)
        {
            if (Vector3.Distance(transform.position, goal.position) < 1f)
            {
                //start attack when close enough to aggro target
                if (!attacking)
                {
                    attacking = true;
                    attackDelay = attackLength / 2f;
                    anim.Play(attackAnim);
                    agent.Stop();
                }
            }
            else
            {
                //stop attacking when to far away and give chase
                attacking = false;
                anim.Play("run");
                agent.Resume();
                agent.destination = goal.position;
            }
        }

        if (attacking && stunTime <= 0)
        {
            attackDelay -= Time.deltaTime;
            //turn hitbox on and off for attack duration
            if (attackDelay <= -attackLength / 2f - 0.1f)
            {
                hitbox.enabled = false;
                attacking = false;
            }
            else if (attackDelay <= 0)
            {
                hitbox.enabled = true;
            }
        }
        else
        {
            hitbox.enabled = false;
            attacking = false;
        }

        stunTime -= Time.deltaTime;
    }

    void OnParticleCollision(GameObject other)
    {
        if (other.CompareTag("Attack"))
        {
            Kill();
        }
    }

    /*void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            Kill();
        }
    }*/

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<DragonController>().DamagePlayer();
        }
    }

    public void Kill()
    {
        if (!dead)
        {
            dead = true;
            agent.enabled = false;
            anim.Play("death1");
        }
    }

    public void ReceiveDamage(float damage)
    {
        if (!dead)
        {
            anim.Play("hit1");
            stunTime = 1f;
            health -= damage;
            if (health <= 0)
            {
                Kill();
            }
        }
    }
}
