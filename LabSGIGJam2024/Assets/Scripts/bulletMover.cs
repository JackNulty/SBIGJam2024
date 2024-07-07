using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMover : MonoBehaviour
{
    public float speed;

    public GameObject bulletHitWall;
    public GameObject bulletHitNan;

    private GameObject stolenGameOnject;
    bool thisIsBeyondFucked = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Instantiate(bulletHitWall, this.transform);
        if(thisIsBeyondFucked == true)
        {
            thisIsBeyondFucked = false;
            stolenGameOnject = collision.gameObject;
        }

        if (collision.gameObject.tag == "Enemy")
        {
            Instantiate(bulletHitNan, stolenGameOnject.transform);

            Destroy(collision.gameObject);
            Destroy(this.gameObject);
            //LevelManager.score += 1;
        }
        if (collision.gameObject.tag == "Walls")
        {
            Instantiate(bulletHitWall, collision.transform);

            Destroy(this.gameObject);
        }
    }
}
