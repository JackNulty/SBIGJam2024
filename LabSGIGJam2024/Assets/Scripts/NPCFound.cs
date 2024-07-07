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

    private float rotation = 0;

    // Start is called before the first frame update
    void Start()
    {
        rotation = parent.transform.rotation.z;
    }

    // Update is called once per frame
    void Update()
    {
        if(yippee.isPlaying == true)
        {
            parent.transform.rotation = Quaternion.Euler(0, 0, rotation);
            rotation+=4;
            readyToGo = true;
        }
        if (yippee2.isPlaying == true)
        {
            parent.transform.rotation = Quaternion.Euler(0, 0, rotation);
            rotation+=10;
            readyToGo2 = true;
        }
        
        if (readyToGo == true && yippee.isPlaying == false)//says yippee then fucking dies
        {
            SaveData.NPCsSaved += 1;
            Destroy(parent);
        }
        if (readyToGo2 == true && yippee2.isPlaying == false)
        {
            SaveData.NPCsSaved += 1;
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
