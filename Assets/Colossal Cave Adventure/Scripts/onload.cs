using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.LoadScene("Pong", LoadSceneMode.Single);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}