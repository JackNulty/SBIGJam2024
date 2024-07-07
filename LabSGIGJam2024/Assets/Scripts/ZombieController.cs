using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ZombieController : MonoBehaviour
{
    Vector3 currentTarget = new Vector3(0, 0, 0);
    NavMeshAgent agent;
    public Rigidbody2D playerRB;
    public bool moveToPlayer = false;
    private float repelForce = 1f;

    Vector3 lockedPosition = Vector3.zero;
    bool dementia = false;
    int rememberTimer = 0;
    int howLongToRemember = 30;

    public GameObject BloodBath;

    void Start()
    {
        if(playerRB != null)
        {
            currentTarget = playerRB.transform.position;
        }
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        //agent.SetDestination(currentTarget);
    }


    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if(dementia == true)
        {
            //transform.position = lockedPosition;
            

            rememberTimer--;
            if(rememberTimer == 0)
            {
                dementia = false;
                agent.Resume();
            }
        }
    }

    void RotateTowardsTarget(Vector3 targetPosition)
    {
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            PlayerScript.damagePlayer(22);
            Debug.Log("playerHasBeenHit");
            Repel(collision);
        }
    }

    private void Repel(Collision2D collision)
    {
        Vector2 enemyPosition = transform.position;
        Vector2 playerPosition = collision.transform.position;
        Vector2 repelDirection = enemyPosition - playerPosition;
        repelDirection.Normalize();

        transform.position = enemyPosition + repelDirection * repelForce;

        lockedPosition = transform.position;
        dementia = true;
        rememberTimer = howLongToRemember;
        agent.Stop();
        agent.velocity = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            LevelManager.score += 1;
            Destroy(gameObject);
            Instantiate(BloodBath,transform.position, Quaternion.identity);
        }
    }

    public void moveToSound(Vector3 t_soundOrigin)
    {
        RotateTowardsTarget(t_soundOrigin);
        agent.SetDestination(t_soundOrigin);
    }
}
