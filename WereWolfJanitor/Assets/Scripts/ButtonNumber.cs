using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonNumber : MonoBehaviour
{
    public static int buttonAmount;
    private Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        buttonText = GetComponent<Text>();
        buttonAmount = 4;
    }

    // Update is called once per frame
    void Update()
    {
        buttonText.text = "Buttons Left: " + buttonAmount;
    }
}
