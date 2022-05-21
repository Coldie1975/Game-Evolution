using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paddles : MonoBehaviour
{
    [SerializeField] GameObject leftpaddle;
    [SerializeField] GameObject rightpaddle;
    [SerializeField] TMP_Dropdown dropdownplayerspeed;
    [SerializeField] float speed = 0.01f;

    [SerializeField] GameObject settings;

    void Update()
    {
        float arrowmovementX = 0;
        float arrowmovementY = 0;
        float keymovementX = 0;
        float keymovementY = 0;
        if (Input.GetKey(KeyCode.A))
        {
            keymovementX -= speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            keymovementX += speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            keymovementY -= speed;
        }
        if (Input.GetKey(KeyCode.W))
        {
            keymovementY += speed;
        }

        if (!AI.useAI)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                arrowmovementX -= speed;
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                arrowmovementX += speed;
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                arrowmovementY -= speed;
            }
            if (Input.GetKey(KeyCode.UpArrow))
            {
                arrowmovementY += speed;
            }
            float arrowX = rightpaddle.transform.position.x + arrowmovementX;
            if (arrowX > 15) arrowX = 15;
            if (arrowX < 3) arrowX = 3;

            float arrowY = rightpaddle.transform.position.y + arrowmovementY;
            if (arrowY > 8) arrowY = 8;
            if (arrowY < -8) arrowY = -8;
            rightpaddle.transform.position = new Vector2(arrowX, arrowY);
        }

        float keyX = leftpaddle.transform.position.x + keymovementX;
        if (keyX > -3) keyX = -3;
        if (keyX < -15) keyX = -15;

        float keyY = leftpaddle.transform.position.y + keymovementY;
        if (keyY > 8) keyY = 8;
        if (keyY < -8) keyY = -8;


        leftpaddle.transform.position = new Vector2(keyX, keyY);
        if (Input.GetKey(KeyCode.T)) settingsmenu();
    }

    public void updatespeed()
    {
        speed = (((float)dropdownplayerspeed.value + 1) * 8 / 100);
    }

    void settingsmenu()
    {
        Time.timeScale = 0;
        settings.SetActive(true);

    }

    public void closesettings()
    {
        settings.SetActive(false);
        Time.timeScale = 1;
    }
}
