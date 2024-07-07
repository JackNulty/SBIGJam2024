using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnionSpin : MonoBehaviour
{
    public float rotationSpeed = 10f;

    void Update()
    {
        float rotationAmount = rotationSpeed * Time.deltaTime;
        transform.Rotate(0, 0, rotationAmount);
        if (PlayerScript.onionCollected == true )
        {
            this.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            //PlayerScript.onionCollected = true;
            //Debug.Log("Onion Collision");
        }
    }
}
