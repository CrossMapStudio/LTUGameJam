using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock_Hands : MonoBehaviour
{
    public GameObject hoursHand;
    public GameObject minutesHand;
    public GameObject secondsHand;

    void Update()
    {
        hoursHand.transform.localEulerAngles = new Vector3(0, (hoursHand.transform.localEulerAngles.y + Time.deltaTime * 5) % 360, 0);
        minutesHand.transform.localEulerAngles = new Vector3(0, (minutesHand.transform.localEulerAngles.y + Time.deltaTime * 60) % 360, 0);
        secondsHand.transform.localEulerAngles = new Vector3(0, (secondsHand.transform.localEulerAngles.y + Time.deltaTime * 360) % 360, 0);
    }
}
