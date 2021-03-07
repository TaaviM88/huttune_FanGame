using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Journal : MonoBehaviour
{
    public static Journal Instance;
    public TMP_Text textbox;
    public float textCooldown = 3f;
    float countdown = 0;
    Color orginalColor;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        countdown = textCooldown;

        orginalColor = textbox.GetComponentInParent<Image>().color;
        textbox.GetComponentInParent<Image>().color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f)
        {
            if(textbox.text != "")
            {
                textbox.text = "";
                textbox.GetComponentInParent<Image>().color = Color.clear;
            }
            
        }
    }

    public void Log(string newLog)
    {
        textbox.text = newLog;
        countdown = textCooldown;
        textbox.GetComponentInParent<Image>().color = orginalColor;
    }
}
