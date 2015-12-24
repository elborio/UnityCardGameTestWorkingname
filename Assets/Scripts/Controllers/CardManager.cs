using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CardManager : MonoBehaviour {
    public List<GameObject> registeredCards;
    public GameObject cardPrefab; //use for easy changing.

    void Start()
    {
        registeredCards = new List<GameObject>();
        RegisterCards();
        CreateCard(0);
        CreateCard(1);
    }

    public void RegisterCards()
    {
        GameObject newCard;

        //Easy Way
        newCard = Instantiate(cardPrefab);
        Card c = newCard.GetComponent<Card>();
        c.hitpoints = 10;
        c.attack = 12;
        c.cardName = "Easy Card";
        registeredCards.Add(newCard);
        
        Debug.Log(newCard.GetHashCode());

        //Easy Way
        newCard = Instantiate(cardPrefab);
        c = newCard.GetComponent<Card>();
        c.hitpoints = 20;
        c.attack = 12;
        c.cardName = "Easy Card";
        registeredCards.Add(newCard);
        
        Debug.Log(registeredCards[0].GetHashCode());
        Debug.Log(registeredCards[0].GetComponent<Card>().hitpoints);
        Debug.Log(registeredCards[1].GetHashCode());
        Debug.Log(registeredCards[1].GetComponent<Card>().hitpoints);

    }

    public GameObject CreateCard(int i)
    {
        GameObject newCard = Instantiate(registeredCards[i]);
        return newCard;
    }
}
