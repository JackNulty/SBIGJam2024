using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static int score;
    public TextMeshProUGUI scoreText;
    public Image slot1;
    public Image slot2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if(PlayerScript.currentWeapon == Weapons.Stick)
        {
            slot1.color = Color.red;
        }
        else
        {
            slot1.color = Color.blue;
        }

        if (PlayerScript.currentWeapon == Weapons.Nunchuckes)
        {
            slot2.color = Color.red;
        }
        else
        {
            slot2.color= Color.blue;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = "Grandparents Harmed: " + score;
    }
}
