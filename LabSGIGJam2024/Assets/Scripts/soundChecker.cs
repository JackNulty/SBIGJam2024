using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundChecker : MonoBehaviour
{
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //this.gameObject.transform.position = player.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;

        ZombieController targetObject = collidedObject.GetComponent<ZombieController>();
        if (targetObject != null)
        {
            targetObject.moveToSound(transform.position);
        }
    }
}
