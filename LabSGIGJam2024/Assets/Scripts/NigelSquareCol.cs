using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NigelSquareCol : MonoBehaviour
{
    public static bool activateNigelQuestMenu = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.Space))
        {
            activateNigelQuestMenu = true;
        }
    }
}
