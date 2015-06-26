using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider2D))]

public class Card : MonoBehaviour
{
    public bool isDraggable;
    public bool isPickedUp;
    private GameObject snapTo;
    public int cardDropSnapRange = 10;

    void Start()
    {
        isDraggable = true;
        isPickedUp = false;
    }

    void Update()
    {
        //Move card with mouse
        MoveCard();
        //Debug.Log(Input.mousePosition);
    }

    void OnMouseDown()
    {
        Debug.Log("test");
        if (!isPickedUp && isDraggable)
        {
            isPickedUp = true;
        } else if (isPickedUp)
        {
            isPickedUp = false;
            GameObject[] snaps = GameObject.FindGameObjectsWithTag("CardSnap");
            float closest = int.MaxValue;
            foreach (GameObject snap in snaps)
            {
                float dist = Vector3.Magnitude(snap.transform.position - transform.position);
                Debug.Log(dist);
                if (dist < cardDropSnapRange)
                {
                    if (dist < closest)
                    {
                        snapTo = snap;
                        transform.position = snapTo.transform.position;
                    }
                }
            }

        }
    }

    void MoveCard()
    {
        if (isPickedUp)
        {
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            transform.position = convertedPosition;
        }
    }
}
    