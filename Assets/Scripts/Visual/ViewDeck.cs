using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDeck : MonoBehaviour
{
    [SerializeField] Transform targetDeck;
    public void Click2ViewOrCloseDeck()
    {
        if (targetDeck.localEulerAngles.y == 90)
        {
            targetDeck.localEulerAngles = Vector3.zero;
        }
        else targetDeck.localEulerAngles = new Vector3(0, 90, 0);
    }
}
