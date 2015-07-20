using UnityEngine;
using System.Collections;


public class Card : MonoBehaviour
{
    public bool isDraggable;
    public bool isPickedUp;
    private bool isSnapped;
    private GameObject snapTo;
    public int cardDropSnapRange = 10;
    private GameObject[] snaps;
    private Vector3 lastPosition;
    public int distanceFromCamera = 5;
    private Vector3 showingPosition;
    public float moveSpeed = 10;
    public bool hoverOver, wasHovered = false;
    private Transform visualRepresentation;

    //Card Stats and all that.
    public Types.cardType cardType;

    //gamecontroller script.
    GameController gc;

    void Start()
    {
        gc = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        showingPosition = Camera.main.transform.position + Vector3.forward * distanceFromCamera;
        snaps = GameObject.FindGameObjectsWithTag("CardSnap");
        isDraggable = true;
        isPickedUp = false;
        isSnapped = false;
        lastPosition = transform.position;
        visualRepresentation = transform.FindChild("CardVisualHolder");
    }

    void Update()
    {
        //Move card with mouse
        if (isPickedUp) //when the card is being picked up.
        {
            MoveCardWithMouse();
            Debug.Log("moveing with mouse");
        } else if (hoverOver && !isPickedUp) //when the card is not picked up but is hovered.
        {
            MoveCardInFrontOfCam();
            Debug.Log("when the card is not picked up but is hovered");
        } else if (!hoverOver && !isPickedUp) //was recently hovered and needs to be returend to parent object.
        {
            ResetVisualRep();
            Debug.Log("was recently hovered and needs to be returned to parent object.");
        }
    }

    void OnMouseDown()
    {
        Debug.Log("test");
        if (!isPickedUp && isDraggable && !gc.isDragging)
        {   
            lastPosition = transform.position;
            isPickedUp = true;
            //set global value to make sure no more than one card is dragged.
            gc.isDragging = true;
        } else if (isPickedUp)
        {
            isPickedUp = false;
            if (snapTo != null && isSnapped)
            {
                lastPosition = snapTo.transform.position;
            }
            gc.isDragging = false;
            transform.position = lastPosition;
        }
    }

    void OnMouseOver()
    {
        hoverOver = true;
    }

    void OnMouseExit()
    {
        hoverOver = false;
        Debug.Log("moved from collider");
    }

    void MoveCardWithMouse()
    {
        ResetVisualRep();
        Vector3 mousePosition = Input.mousePosition;
        Vector3 convertedPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 18)); //now uses 20 cuz camera is 20 away from the board
        transform.position = convertedPosition;
        SnapToClosest();
    }

    void MoveCardInFrontOfCam()
    {
        visualRepresentation.position = Vector3.Lerp(visualRepresentation.position, showingPosition, Time.deltaTime * moveSpeed);
        visualRepresentation.GetComponent<SpriteRenderer>().sortingOrder = 2;
        visualRepresentation.GetComponentInChildren<Canvas>().sortingOrder = 3;
    }

    void MoveToLastPos()
    {
        transform.position = Vector3.Lerp(transform.position, lastPosition, Time.deltaTime * moveSpeed);
    }

    void MoveToLastPos(Transform t)
    {
        t.position = Vector3.Lerp(t.position, lastPosition, Time.deltaTime * moveSpeed);
    }

    void ResetVisualRep()
    {
        visualRepresentation.position = Vector3.Lerp(visualRepresentation.position, transform.position, Time.deltaTime * moveSpeed);
        visualRepresentation.GetComponent<SpriteRenderer>().sortingOrder = 0;
        visualRepresentation.GetComponentInChildren<Canvas>().sortingOrder = 1;
    }

    void SnapToClosest()
    {
        float closest = int.MaxValue;
        foreach (GameObject snap in snaps)
        {
            float dist = Vector3.Distance(snap.transform.position, transform.position);
            //Debug.Log(dist);
            if (dist < cardDropSnapRange)
            {
                isSnapped = true;
                if (dist < closest)
                {
                    snapTo = snap;
//                    Debug.Log(snap.name+"\n");
//                    Debug.Log(snap.transform.position);
                    transform.position = snapTo.transform.position;
                    closest = dist;
                }
            }
        }
    }
}
    