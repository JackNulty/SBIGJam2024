using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderKeywordFilter;
using UnityEngine;

public class NPCFound : MonoBehaviour
{
    public AudioSource yippee;
    public AudioSource yippee2;
    public GameObject parent;
    public GameObject particles;

    private bool readyToGo = false;
    private bool readyToGo2 = false;//this is scuffed

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(yippee.isPlaying == true)
        {
            readyToGo = true;
        }
        if (yippee2.isPlaying == true)
        {
            readyToGo2 = true;
        }

        if(readyToGo == true && yippee.isPlaying == false)//says yippee then fucking dies
        {
            Destroy(parent);
        }
        if (readyToGo2 == true && yippee2.isPlaying == false)
        {
            Destroy(parent);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            particles.SetActive(true);
            if(Random.Range(0,10) == 5)
            {
                yippee2.Play();
            }
            else yippee.Play();
        }
    }
}
