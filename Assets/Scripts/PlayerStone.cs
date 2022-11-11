using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStone : MonoBehaviour
{

    StateManager theStateManager;
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
        targetPosition = this.transform.position;
      
    }

    public Tile StartingTile;
    Tile currentTile;
    Tile[] moveQueue;
    public int PlayerId;
    int moveQueueIndex = 9999;

    Vector3 targetPosition;
    Vector3 velocity;

    float smoothTime = 0.25f;
    float smoothTimeVertical = 0.01f;
    float smoothDistance = 0.01f;
    float smoothHeight = 0.5f;
    
    bool scoreMe = false;
    bool isAnimating = false;

    void AdvanceMoveQueue()
    {
        if (moveQueue != null && moveQueueIndex < moveQueue.Length)
        {
            Tile nextTile = moveQueue[moveQueueIndex];
            if (nextTile == null)
            {
                SetNewTargetPosition(this.transform.position + Vector3.right * 100);
            }
            else
            {
                SetNewTargetPosition(nextTile.transform.position);
                moveQueueIndex++;
            }
        }
        else {
            this.isAnimating = false;
            theStateManager.IsDoneAnimating = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (isAnimating == false) {
            return;
        }


        if(Vector3.Distance(new Vector3(this.transform.position.x, targetPosition.y, this.transform.position.z), targetPosition) < smoothDistance)
        {
            if (moveQueue != null && moveQueueIndex == moveQueue.Length && this.transform.position.y > smoothDistance)
            {

                this.transform.position = Vector3.SmoothDamp(
                   this.transform.position,
                   new Vector3(this.transform.position.x, 0, this.transform.position.z),
                   ref velocity,
                   smoothTimeVertical
               ) ;
                this.transform.position += new Vector3(0, -1.2f, 0);
            }
            else { 
                AdvanceMoveQueue();
            }
        }
        else if (this.transform.position.y < (smoothHeight-smoothDistance))
        {
            this.transform.position = Vector3.SmoothDamp(
                this.transform.position, 
                new Vector3(this.transform.position.x, smoothHeight, this.transform.position.z), 
                ref velocity,
                smoothTimeVertical
            );

        }
        else 
        {
            //old:
            //  this.transform.position = Vector3.SmoothDamp(this.transform.position, targetPosition, ref velocity, smoothTime);

            this.transform.position = Vector3.SmoothDamp(
                this.transform.position,
                new Vector3(targetPosition.x, smoothHeight, targetPosition.z),
                ref velocity,
                smoothTime
            );
        }

    }

    void SetNewTargetPosition(Vector3 pos)
    {
        //targetPosition = pos+(Vector3.down*1);
        targetPosition = pos;
        velocity = Vector3.zero;
        
    }

    void OnMouseUp() {
        //Debug.Log("Click!");

        if (theStateManager.CurrentPlayerId != PlayerId) {
            return;
        }

        if (theStateManager.IsDoneRolling == false) {
            return;
        }
        if (theStateManager.IsDoneClicking == true) {
            return;
        }

        int spacesToMove = theStateManager.DiceTotal;

        if (spacesToMove == 0) {
            return;
        }

        moveQueue = new Tile[spacesToMove];
        Tile finalTile = currentTile;



        for (int i = 0; i < spacesToMove; i++)
        {
            if (finalTile == null && scoreMe == false)
            {
                finalTile = StartingTile;
            }
            else
            {
                if (finalTile.NextTiles == null || finalTile.NextTiles.Length == 0)
                {
                    //Debug.Log("Score!");
                    //Destroy(gameObject);
                    //return;
                    scoreMe = true;
                    finalTile = null;
                }
                else if (finalTile.NextTiles.Length > 1)
                {
                    finalTile = finalTile.NextTiles[PlayerId];
                }
                else { 
                    finalTile = finalTile.NextTiles[0];
                }
                
            }
            moveQueue[i] = finalTile;
        }

        //old:
        //  this.transform.position = finalTile.transform.position;
        //  this.transform.Translate(Vector3.down * 1);
        // SetNewTargetPosition(finalTile.transform.position);

        this.transform.SetParent(null);

        moveQueueIndex = 0;
        currentTile = finalTile;
        theStateManager.IsDoneClicking = true;
        this.isAnimating = true;
    }
}
