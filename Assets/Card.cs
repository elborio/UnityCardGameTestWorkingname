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
    private bool hoverOver, wasHovered = false;
    private Transform visualRepresentation;

    void Start()
    {
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
        MoveCardWithMouse();
        //Debug.Log(Input.mousePosition);

        if (hoverOver && !isPickedUp)
        {
            MoveCardInFrontOfCam();
        } else if (wasHovered && !isPickedUp)
        {
            MoveToLastPos(visualRepresentation);

            Debug.Log("Why is this called: "+ wasHovered + " isPickedup: " +isPickedUp+"\n");
            wasHovered = false;
        }
    }

    void OnMouseDown()
    {
        Debug.Log("test");
        if (!isPickedUp && isDraggable)
        {   
            lastPosition = transform.position;
            isPickedUp = true;
        } else if (isPickedUp)
        {
            isPickedUp = false;
            if (snapTo != null && isSnapped)
            {
                lastPosition = snapTo.transform.position;
            }
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
        wasHovered = true;
        Debug.Log("moved from collider");
    }

    void MoveCardWithMouse()
    {
        if (isPickedUp)
        {
            ResetVisualRep();
            MoveToLastPos(visualRepresentation);
            Vector3 mousePosition = Input.mousePosition;
            Vector3 convertedPosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, 10));
            transform.position = convertedPosition;
            SnapToClosest();
        }
    }

    void MoveCardInFrontOfCam()
    {
        visualRepresentation.position= Vector3.Lerp(visualRepresentation.position, showingPosition, Time.deltaTime * moveSpeed);
    }

    void MoveToLastPos()
    {
        transform.position = Vector3.Lerp(transform.position, lastPosition, Time.deltaTime * moveSpeed);
    }

    void MoveToLastPos(Transform t )
    {
        t.position = Vector3.Lerp(t.position, lastPosition, Time.deltaTime * moveSpeed);
    }

    void ResetVisualRep()
    {
        visualRepresentation.position = Vector3.Lerp(visualRepresentation.position,transform.position, Time.deltaTime * moveSpeed);
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
    