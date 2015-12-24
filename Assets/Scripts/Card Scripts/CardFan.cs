using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CardFan : MonoBehaviour {

    private List<Card> cards;
    public float cardOffset;

    public CardFan()
    {
        cards = new List<Card>();
    }

    public Card addCard(Card c)
    {
        cards.Add(c);
        UpdateCards();
        return c; //TODO: not sure if this return is usefull yet.
    }

    public void UpdateCards()
    {
        Vector3 startPosition = gameObject.transform.position;
        float offset = 0;
        foreach(Card c in cards)
        {
            Vector3 nextPos = new Vector3(offset, 0, 0);
            c.MoveTo(startPosition + nextPos);
            offset = offset + cardOffset;
        }
    }
}
