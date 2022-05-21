using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] float speed = 4f;
    [SerializeField] float speedincrease = 0.1f;
    [SerializeField] float scoringscore = 100;

    [SerializeField] TextMeshProUGUI leftscoretext;
    [SerializeField] TextMeshProUGUI rightscoretext;
    [SerializeField] TextMeshProUGUI winnername;

    [SerializeField] Rigidbody ball;

    //UI
    [SerializeField] GameObject settings;
    [SerializeField] GameObject start;
    [SerializeField] GameObject rules;
    [SerializeField] GameObject endgame;

    Vector2 direction;

    float leftscore = 0;
    float rightscore = 0;

    private float currentspeed;
    private string previospaddlename = "";
    private int scorefactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        direction = Vector2.one.normalized;
        currentspeed = speed;
        Time.timeScale = 0;
    }

    private void FixedUpdate()
    {
        ball.velocity = direction * currentspeed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            direction.y = -direction.y;
        }

        if (collision.gameObject.CompareTag("Paddle"))
        {
            if (previospaddlename == "")
            {
                previospaddlename = collision.gameObject.name;
            }
            else
            {
                if (previospaddlename.Equals(collision.gameObject.name))
                {
                    scorefactor += 1;
                }
                else
                {
                    scorefactor = 1;
                }
            }
            previospaddlename = collision.gameObject.name;
            //direction = Vector3.Reflect(rb.velocity,collision.contacts[0].normal);
            direction.x = -direction.x;


            currentspeed += speedincrease;
            if (collision.gameObject.transform.position.x > 0)
            //right side
            {
                float scoreincrease = Mathf.Floor(16 - collision.gameObject.transform.position.x);
                scoreincrease *= scorefactor;
                rightscore += scoreincrease;
                rightscoretext.text = "" + rightscore;
            }
            else
            {
                float scoreincrease = Mathf.Floor(16 + collision.gameObject.transform.position.x);
                scoreincrease *= scorefactor;
                leftscore += scoreincrease;
                leftscoretext.text = "" + leftscore;
            }

        }
        if (collision.gameObject.CompareTag("left wall"))
        {

            score(true);
        }
        if (collision.gameObject.CompareTag("right wall"))
        {
            score(false);
        }
    }


    void score(bool left)
    {
        previospaddlename = "";
        this.transform.position = new Vector2(0, 0);
        currentspeed = speed;
        if (!left)
        {
            leftscore += scoringscore;
            leftscoretext.text = "" + leftscore;
            if (rightscore >= 500 || leftscore >= 500) win();
        }
        else
        {
            rightscore += scoringscore;
            rightscoretext.text = "" + rightscore;
            if (rightscore >= 500 || leftscore >= 500) win();
        }
    }

    void win()
    {
        Time.timeScale = 0;
        endgame.SetActive(true);
        if (leftscore > rightscore)
        {
            //left wins
            winnername.text = "Player 1";
        }
        else
        {
            //right wins
            winnername.text = "Player 2";
        }
    }

    public void startgame()
    {
        endgame.SetActive(false);
        start.SetActive(false);
        settings.SetActive(false);
        rules.SetActive(false);

        Time.timeScale = 1;
        rightscore = 0;
        leftscore = 0;
        rightscoretext.text = "0";
        leftscoretext.text = "0";
    }
}