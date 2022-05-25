using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BreakoutBall : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI targetsText;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] Rigidbody2D ball;

    [SerializeField] GameObject[] targetObjects;
    [SerializeField] GameObject endgame;
    [SerializeField] GameObject player;
    [SerializeField] TextMeshProUGUI endtext;
    [SerializeField] AudioSource playhit;
    [SerializeField] AudioClip playclip;
    [SerializeField] GameObject targetParent;
    [SerializeField] Material red;
    [SerializeField] Material blue;

    bool timerActive;
    float timer;

    float speed = 5f;
    int targets = 0;
    int lives = 5;

    int score = 0;
    float targetdifficulty = 1.2f;
    void Start()
    {
        ball.AddForce(new Vector2(100f, 100f));
        foreach(GameObject obj in targetObjects)
        {
            if(obj.transform.position.y < targetdifficulty)
            {
                obj.GetComponent<SpriteRenderer>().material = blue;
            }
            else
            {
                obj.GetComponent<SpriteRenderer>().material = red;
            }
        }
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
        ball.velocity = ball.velocity.normalized * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Score"))
        {
            playhit.PlayOneShot(playclip);
            collision.gameObject.SetActive(false);
            targets += 1;
            var scales = player.transform.localScale;
            if(collision.gameObject.transform.position.y < targetdifficulty)
            {
                scales.x += 0.1f;
                score += (1 * lives);
            }
            else
            {
                scales.x -= 0.1f;
                score +=( 2 * lives);
            }
            scoreText.text = "Score: " + score;
            if (scales.x <= 0.5f)
            {
                scales.x = 0.5f;
            }

            if (scales.x >= 3f)
            {
                scales.x = 3f;
            }
            player.transform.localScale = scales;
        }


        targetsText.text = "Targets Remaining: " + (112 - targets);
        if (collision.gameObject.CompareTag("Finish"))
        {
            lostLife();

        }

        if (collision.gameObject.CompareTag("Player"))
        {
            speed += 0.1f;
            if(collision.gameObject.transform.position.x > this.transform.position.x)
            {
                if(collision.gameObject.transform.position.x - this.transform.position.x >= 0.1)
                {
                    var newVelocity = ball.velocity;
                    newVelocity.x -= 5;
                    ball.velocity = newVelocity;
                }
            }

            if (collision.gameObject.transform.position.x < this.transform.position.x)
            {
                if (this.transform.position.x - collision.gameObject.transform.position.x >= 0.1)
                {
                    var newVelocity = ball.velocity;
                    newVelocity.x += 5;
                    ball.velocity = newVelocity;
                }
            }

        }

        if(targets >= 112)
        {
            //level complete
            levelComplete(true);
        }
    }


    void levelComplete(bool win)
    {
        if (timerActive)
        {
            return;
        } 

        if (win)
        {

            targetdifficulty -= 0.4f;
            endtext.text = "I can not believe you won. <br> Your score was "+score+" <br> I am sending you to another world.";
            foreach (GameObject obj in targetObjects)
            {
                obj.SetActive(true);
                if (obj.transform.position.y < targetdifficulty)
                {
                    obj.GetComponent<SpriteRenderer>().material = blue;
                }
                else
                {
                    obj.GetComponent<SpriteRenderer>().material = red;
                }
            }
            targets = 0;
            lives += 1;
            ball.velocity = new Vector2(0, 0);
            ball.transform.position = new Vector3(0,-3.46f,0);
            ball.AddForce(new Vector2(100f, 100f));
        }
        else
        {
            endtext.text = "You have died. <br> Your score was " + score + "<br> I hope you do better in the next world.";
            endgame.SetActive(true);
            Invoke("restart", 10f);
            timerActive = true;
            timer = Time.time;
            ball.velocity = new Vector2(0, 0);
            ball.transform.position = new Vector3(0, -3.46f, 0);
        }

    }

    void lostLife()
    {
        lives -= 1;
        livesText.text = "Lives Left: " + lives;
        if (lives <= 0)
        {
            levelComplete(false);
        }
        speed = 5f;
    }

    void restart()
    {
        SceneManager.LoadScene("Pong", LoadSceneMode.Single);

    }
}
