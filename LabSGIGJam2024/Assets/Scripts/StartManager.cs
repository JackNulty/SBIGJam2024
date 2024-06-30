using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;


public class StartManager : MonoBehaviour
{
    public Canvas canvas;
    public UnityEngine.UI.Button StartButton;
    public UnityEngine.UI.Button WaitButton;

    // Start is called before the first frame update
    void Start()
    {
        StartButton = GetComponent<UnityEngine.UI.Button>();
        WaitButton = GetComponent<UnityEngine.UI.Button>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Input.GetKey(KeyCode.Space))
        {
            canvas.gameObject.SetActive(true);
        }
    }

    public void onStartButtonPressed()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void onWaitButtonPressed()
    {
        canvas.gameObject.SetActive(false);
    }
}
