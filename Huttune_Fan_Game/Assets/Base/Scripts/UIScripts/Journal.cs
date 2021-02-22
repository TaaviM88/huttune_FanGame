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
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if(countdown <= 0f)
        {
            textbox.text = "";
        }
    }

    public void Log(string newLog)
    {
        textbox.text = newLog;
        countdown = textCooldown;
    }
}
