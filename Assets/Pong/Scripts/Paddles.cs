using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Paddles : MonoBehaviour
{
    [SerializeField] GameObject leftpaddle;
    float speed = 10f;

    [SerializeField] GameObject settings;

    [SerializeField] GameObject keyW;
    [SerializeField] GameObject keyA;
    [SerializeField] GameObject keyS;
    [SerializeField] GameObject keyD;
    [SerializeField] Rigidbody2D playerRB;

        void FixedUpdate()
        {
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

        keyS.SetActive(keymovementY > 0);
        keyW.SetActive(keymovementY < 0);
        keyA.SetActive(keymovementX < 0);
        keyD.SetActive(keymovementX > 0);

        float keyX = leftpaddle.transform.position.x;
        if (keyX > -1) keymovementX = -1;

        playerRB.velocity = new Vector2(keymovementX,keymovementY);

        if (Input.GetKey(KeyCode.Escape)) settingsmenu();
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

    private void Start()
    {
        settingsmenu();
    }

    public void doExitGame()
    {
        Application.Quit();
    }
}
