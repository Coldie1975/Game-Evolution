using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] TMP_Dropdown numberofplayers;
    [SerializeField] TMP_Dropdown AIspeed;
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject rightPaddle;
    [SerializeField] float speed = 0.1f;

    static public bool useAI = true;

    private void FixedUpdate()
    {
        if (useAI)
        {
            var step = speed * Time.deltaTime; // calculate distance to move
            rightPaddle.transform.position = Vector2.MoveTowards(rightPaddle.transform.position, Ball.transform.position, speed);

            float arrowX = rightPaddle.transform.position.x;
            if (arrowX > 15) arrowX = 15;
            if (arrowX < 3) arrowX = 3;

            float arrowY = rightPaddle.transform.position.y;
            if (arrowY > 8) arrowY = 8;
            if (arrowY < -8) arrowY = -8;
            rightPaddle.transform.position = new Vector2(8, arrowY);
        }
    }

    public void changeAI()
    {
        if (numberofplayers.value == 0)
        {
            useAI = true;
        }
        else
        {
            useAI = false;
        }
    }

    public void updatespeed()
    {
        speed = (((float)AIspeed.value + 1) * 2 / 10);
    }
}
