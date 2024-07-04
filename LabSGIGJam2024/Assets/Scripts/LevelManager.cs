using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public static int score;
    public TextMeshProUGUI scoreText;
    public Image healthBar;
    public Image slot1;
    public Image slot2;
    public Image slot3;
    public Image slot4;

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

        if (PlayerScript.currentWeapon == Weapons.Bat)
        {
            slot3.color = Color.red;
        }
        else
        {
            slot3.color = Color.blue;
        }

        if (PlayerScript.currentWeapon == Weapons.Pistol)
        {
            slot4.color = Color.red;
        }
        else
        {
            slot4.color = Color.blue;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreText.text = "Grandparents Harmed: " + score;

        healthBar.fillAmount = PlayerScript.playerHealth * 2;
    }
}
