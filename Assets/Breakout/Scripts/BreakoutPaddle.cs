using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakoutPaddle : MonoBehaviour
{
    [SerializeField] GameObject Player;
    float speed = 10f;

    [SerializeField] GameObject settings;

    [SerializeField] GameObject keyA;
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



        keyA.SetActive(keymovementX < 0);
        keyD.SetActive(keymovementX > 0);

        playerRB.velocity = new Vector2(keymovementX, keymovementY);

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
