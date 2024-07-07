using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NigelSquareCol : MonoBehaviour
{
    public static bool activateNigelQuestMenu = false;
    public static bool nigelQuestComp = false;
    public GameObject interactPrompt;
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
            if (PlayerScript.onionCollected != true)
            {
                activateNigelQuestMenu = true;
            }
            else if (PlayerScript.onionCollected == true)
            {
                nigelQuestComp = true;
                Debug.Log(nigelQuestComp);
                Debug.Log("NIgel quest has been complete");
                PlayerScript.currentWeapon = Weapons.Nigel;
            }
        }

        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                interactPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                interactPrompt.gameObject.SetActive(false);
            }
        }
    }
}
