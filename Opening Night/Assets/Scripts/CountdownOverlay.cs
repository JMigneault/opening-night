using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownOverlay : MonoBehaviour
{

    private Text text;

    // Start is called before the first frame update
    void Awake()
    {
        text = GetComponentInChildren<Text>();
    }

    public void SetText(string newText)
    {
        text.text = newText;
    }

}
