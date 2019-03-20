using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinStateScript : MonoBehaviour
{
    string winner = "";
    string loser = "";

    [SerializeField] Text p1WinStateText;
    [SerializeField] Text p2WinStateText;

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.GetComponent<PlayerScript>().pollenAmount >= 38.0f)
        {
            if (col.gameObject.tag == "Player1" && string.Compare(winner,"") == 0)
            {
                p1WinStateText.text = "Winner!!!";
                p1WinStateText.gameObject.SetActive(true);

                p2WinStateText.text = "Loser...";
                p2WinStateText.gameObject.SetActive(true);
            }

            if (col.gameObject.tag == "Player2" && string.Compare(winner,"") == 0)
            {
                p2WinStateText.text = "Winner!!!";
                p2WinStateText.gameObject.SetActive(true);

                p1WinStateText.text = "Loser...";
                p1WinStateText.gameObject.SetActive(true);
            }
        }   
    }
}
