using UnityEngine;
using System.Collections;

[System.Serializable]
public class Card : MonoBehaviour {

    public string cardName;
        
    public int attack;
    public int hitpoints;

    private bool isPlayed;
    private bool canAttack;

    public GameObject visual;
    private CardPrefab visualControl;


    private SpecialEffect special;

    public Card(GameObject prefab, string name, int attack, int hitpoints)
    {
        canAttack = true; //TODO: change so cards can be passive to eg. attack is 0 or just a aura effect or something.
        isPlayed = false;
        this.cardName = name;
        this.attack = attack;
        this.hitpoints = hitpoints;
        visual = prefab;
        visualControl = visual.GetComponent<CardPrefab>();
        visualControl.setName(this.cardName);
    }

    public bool useOn(Card other)
    {
        if (isPlayed && other.isPlayed)
        {
            //play animation.
            hitpoints = hitpoints - other.attack;
            other.hitpoints = other.hitpoints - attack;
            return true;
        }
        return false;
    }

    public GameObject createVisual()
    {
        return GameObject.Instantiate(visual);
    }

    public void MoveTo(Vector3 v)
    {
        visual.transform.position = v;
    }
}
