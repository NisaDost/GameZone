using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClickTimer : MonoBehaviour
{
    private float clickTime1 = 0;
    private float clickTime2 = 0;
    public float timeDifference;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (clickTime1 == 0f)
            {
                clickTime1 = Time.time;
            }
            else if (clickTime2 == 0f)
            {
                clickTime2 = Time.time;
                CalculateTimeDifference();
            }
        }
    }
    private void CalculateTimeDifference()
    {
        timeDifference = clickTime2 - clickTime1;
        clickTime1 = clickTime2;
        clickTime2 = 0f;
    }
}
