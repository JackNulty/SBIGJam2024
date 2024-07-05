using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class NigelMenuStarter : MonoBehaviour
{
    public Canvas canvas;
    public UnityEngine.UI.Button StartButton;
    public UnityEngine.UI.Button WaitButton;
    public GameObject NigelMenu;
    public static bool startNigelQuest;
    // Start is called before the first frame update
    void Start()
    {
        StartButton = GetComponent<UnityEngine.UI.Button>();
        WaitButton = GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        if(NigelSquareCol.activateNigelQuestMenu ==  true)
        {
            NigelMenu.SetActive(true);
        }
    }

    public void onStartButtonPressed()
    {
        startNigelQuest = true;
    }

    public void onWaitButtonPressed()
    {
        NigelSquareCol.activateNigelQuestMenu = false;
    }
}
