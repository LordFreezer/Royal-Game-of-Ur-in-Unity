using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DiceRoller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() {
        DiceValues = new int[4];
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    // Update is called once per frame
    void Update() {
        
    }

    public int[] DiceValues;

    
    public Sprite[] DiceImageOne;
    public Sprite[] DiceImageZero;

    StateManager theStateManager;
    

    public void RollTheDice() {
        if (theStateManager.IsDoneRolling == true) {
            return;
        }


        //In Ur, 4 dice will be rolled.
        theStateManager.DiceTotal = 0;
        for (int i = 0; i < DiceValues.Length; i++)
        {
            DiceValues[i] = Random.Range(0, 2); // 0 or 1
            theStateManager.DiceTotal += DiceValues[i];
            //update visuals
            if (DiceValues[i] == 0)
            {
                this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImageZero[Random.Range(0, DiceImageZero.Length)];

            }
            else {                 
                this.transform.GetChild(i).GetComponent<Image>().sprite = DiceImageOne[Random.Range(0, DiceImageOne.Length)];

            }

            theStateManager.IsDoneRolling = true;

        }

        
       // Debug.Log("Rolled: " +  DiceTotal );
    }
}
