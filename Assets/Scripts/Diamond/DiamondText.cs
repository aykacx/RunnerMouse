using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondText : MonoBehaviour
{
    Text text;
    public static int DiamondAmount;

    void Start()
    {
        DiamondAmount = PlayerPrefs.GetInt("DiamondText", DiamondAmount);
        text = GetComponent<Text>();
    }

    void Update()
    {
        text.text = DiamondAmount.ToString();
        PlayerPrefs.SetInt("DiamondText", DiamondAmount);
        PlayerPrefs.Save();
    }
}
