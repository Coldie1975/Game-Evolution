using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{
    float speed = 5f;
    float speedincrease = 0.5f;
    float scoringscore = 50;

    [SerializeField] TextMeshProUGUI leftscoretext;
    [SerializeField] TextMeshProUGUI rightscoretext;
    [SerializeField] TextMeshProUGUI winnername;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] Rigidbody2D ball;
    [SerializeField] AudioSource playhit;
    [SerializeField] AudioClip playclip;
    //UI
    [SerializeField] GameObject endgame;
    [SerializeField] TextMeshProUGUI endtext;

    Vector2 direction = new Vector2(1f, 1f);
    [SerializeField] Vector2 lastVelocity;

    float lastcollide;
    bool timerActive;
    float timer;
    float leftscore = 0;
    float rightscore = 0;

    private float currentspeed;
    private string previospaddlename = "";
    private int scorefactor = 1;
    // Start is called before the first frame update
    void Start()
    {
        lastcollide = Time.time;
        //direction = new Vector2(-1,-1);
        ball.AddForce(new Vector2(100f, 100f));
        currentspeed = speed;
        //Time.timeScale = 0;
    }

    private void FixedUpdate()
    {
        if (timerActive)
        {
            timerText.text = "" + Mathf.FloorToInt(10 - (Time.time - timer));
        }
        var newVelocity = ball.velocity;
        if (newVelocity.y < 2 && newVelocity.y > -2)
        {
            if (newVelocity.y < 0)
            {
                newVelocity.y = -3;
            }
            else
            {
                newVelocity.y = 3;
            }
        }
        if (newVelocity.x < 2 && newVelocity.x > -2)
        {
            if (newVelocity.x < 0)
            {
                newVelocity.x = -3;
            }
            else
            {
                newVelocity.x = 3;
            }
        }
        ball.velocity = newVelocity;
        ball.velocity = ball.velocity.normalized * currentspeed;

        //ball.velocity = direction * currentspeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {


        if (Time.time - lastcollide < 0.2f && previospaddlename.Equals(collision.gameObject.name))
        {
            //Debug.Log("collide timer");
            return;
        }
        lastcollide = Time.time;
        if (collision.gameObject.CompareTag("Wall"))
        {
            currentspeed += speedincrease;
        }

        if (collision.gameObject.CompareTag("Player")) {
            collision.rigidbody.velocity.Set(0, 0);
        }

        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AI"))
        {
            playhit.PlayOneShot(playclip);
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
            if (scorefactor >= 5) scorefactor = 5;
            previospaddlename = collision.gameObject.name;

            currentspeed += speedincrease;
            if (collision.gameObject.transform.position.x > 0)
            //right side
            {
                float scoreincrease = Mathf.Floor(9 - collision.gameObject.transform.position.x);
                scoreincrease *= scorefactor;
                rightscore += scoreincrease;
                rightscoretext.text = "Computer: " + rightscore;
            }
            else
            {
                float scoreincrease = Mathf.Floor(9 + collision.gameObject.transform.position.x);
                scoreincrease *= scorefactor;
                leftscore += scoreincrease;
                leftscoretext.text = "Player:" + leftscore;
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
        ball.AddForce(new Vector2(100f, 100f));
        currentspeed = speed;
        if (!left)
        {
            leftscore += scoringscore;
            leftscoretext.text = "Player: " + leftscore;
            if (rightscore >= 500 || leftscore >= 500) win(true);
        }
        else
        {
            rightscore += scoringscore;
            rightscoretext.text = "Computer: " + rightscore;
            if (rightscore >= 500 || leftscore >= 500) win(false);
        }
    }

    void win(bool playerwon)
    {
        if (timerActive)
        {
            return;
        }
        //Time.timeScale = 0;
        endgame.SetActive(true);
        if (!playerwon)
        {
            if (leftscore <= 300)
            {
                endtext.text = "You have died <br>  wow that was the worst score I have ever seen <br> I hope you do better in the next world";
            }
            else
            {
                endtext.text = "You have died <br>  not too bad a score I guess <br> I will send you to another world";
            }
        }
        else
        {
            endtext.text = "I can not believe you won <br> my champion was meant to be the best <br> I am sending you to another world";
        }
        ball.velocity = new Vector2(0, 0);
        Invoke("restart", 10f);
        timerActive = true;
        timer = Time.time;
    }
    void restart()
    {
        SceneManager.LoadScene("Breakout", LoadSceneMode.Single);
    }
}