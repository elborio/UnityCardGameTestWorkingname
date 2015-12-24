using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CardPrefab : MonoBehaviour {

    public Text nameVisual;
    public string nameText;

    public void Update()
    {
        nameVisual.text = nameText;
    }

    public void setName(string name)
    {
        this.nameText = name;
    }
}
