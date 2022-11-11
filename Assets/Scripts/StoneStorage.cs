using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneStorage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < this.transform.childCount; i++) {
            GameObject theStone = Instantiate(StonePrefab);
            theStone.GetComponent<PlayerStone>().StartingTile = this.StartingTile;
            AddStoneToStorage(theStone, this.transform.GetChild(i));
            
        }
    }

    

    public void AddStoneToStorage (GameObject theStone, Transform thePlaceholder=null){

        if (thePlaceholder == null) { 
            for (int i = 0; i < this.transform.childCount; i++)
            {
                Transform p = this.transform.GetChild(i);
                if (p.childCount == 0) { 
                    thePlaceholder = p;
                    break;
                }
            }
            if (thePlaceholder == null) {
                return;
            }
        }

        theStone.transform.SetParent(thePlaceholder);
        theStone.transform.localPosition = Vector3.zero;
        
    }

    public GameObject StonePrefab;
    public Tile StartingTile;

    // Update is called once per frame
    void Update()
    {
        
    }
}
