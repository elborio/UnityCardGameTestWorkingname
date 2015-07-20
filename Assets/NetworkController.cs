using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkController : MonoBehaviour 
{
    public Text ipText; 

    void Start()
    {
        ipText = GameObject.Find("Canvas/Text_IP").GetComponent<Text>();
    }

    void Update()
    {
        ipText.text = Time.frameCount.ToString();
    }   
}
