using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AI : MonoBehaviour
{
    [SerializeField] GameObject Ball;
    [SerializeField] GameObject rightPaddle;
    float speed = 15f;

    private void FixedUpdate()
    {
            var step = speed * Time.deltaTime; // calculate distance to move
            rightPaddle.transform.position = Vector2.MoveTowards(rightPaddle.transform.position, Ball.transform.position, step);

            float arrowY = rightPaddle.transform.position.y;
            if (arrowY > 2.2f) arrowY = 2.2f;
            if (arrowY < -4) arrowY = -4;
            rightPaddle.transform.position = new Vector2(4, arrowY);
    }
}
