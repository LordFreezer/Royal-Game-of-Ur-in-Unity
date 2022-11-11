using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DiceTotalDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        theStateManager = GameObject.FindObjectOfType<StateManager>();
    }

    StateManager theStateManager;


    // Update is called once per frame
    void Update()
    {
        if (theStateManager.IsDoneRolling == false)
        {
            GetComponent<TextMeshProUGUI>().text = ": " + "?";
        }
        else
        {
            GetComponent<TextMeshProUGUI>().text = ": " + theStateManager.DiceTotal;
        }

    }
}
