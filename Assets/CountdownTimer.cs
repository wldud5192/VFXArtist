using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownTimer : MonoBehaviour
{
    public SphereProperties sphereManager;
    public float currentTime;
    float startingTime = 30f;
    Text countdownText;

    public int level;
    public Text levelText;

    public int clientState = 4;
    public Text clientStateText;

    public bool loadFinished = false;

    public GameObject successUI;
    public GameObject failedUI;
    public GameObject gameOverUI;

    // Start is called before the first frame update
    void Awake()
    {
        sphereManager = GameObject.FindGameObjectWithTag("SphereManager").GetComponent<SphereProperties>();

        countdownText = GetComponent<Text>();
        level = 1;
        resetTimer();
        levelText.text = level.ToString("0");
    }

    void resetTimer()
    {
        currentTime = startingTime;
        countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow10", false);
        countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow5", false);

    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        countdownText.text = currentTime.ToString("0");
                
        if (currentTime < 11)
        {
            countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow10", true);
        }
        if (currentTime < 5)
        {
            countdownText.gameObject.GetComponent<Animator>().SetBool("IsBelow5", true);
        }

        if (currentTime <= 0)
        {
            currentTime = 0;
            if (!loadFinished)
            {
                StartCoroutine(TimeOver());
            }

        }

        if (sphereManager.matchSimilarityNum >= 4)
        { 
                if (!loadFinished)
                {
                Debug.Log("Loading Success...");
                    StartCoroutine(Success());
                }
        }

        if (sphereManager == null)
        {
            sphereManager = GameObject.FindGameObjectWithTag("SphereManager").GetComponent<SphereProperties>();
        }
    }
  


    IEnumerator TimeOver()
    {
        loadFinished = true;
        Debug.Log("Time over!");

        failedUI.GetComponent<Text>().enabled = true;
        failedUI.GetComponent<AudioSource>().Play();

        clientState--;
        if(clientState == 3)
        {
            //Annoyed
            clientStateText.text = "Annoyed";
        }
        if (clientState == 2)
        {
            //Angry
            clientStateText.text = "Angry";
        }
        if (clientState == 1)
        {
            //About to explode...
            clientStateText.text = "...";
        } if (clientState == 0)
        {
            StartCoroutine(GameOver());
        }   


        yield return new WaitForSeconds(1.5f);

        failedUI.GetComponent<Text>().enabled = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        resetTimer();
        loadFinished = false;
        yield break;
    }

    IEnumerator GameOver()
    {
        Debug.Log("Game Over!");
        gameOverUI.GetComponent<Text>().enabled = true;
        gameOverUI.GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(1.5f);
        Application.Quit();

    }

    IEnumerator Success()
    {
        loadFinished = true;
        successUI.GetComponent<Text>().enabled = true;
        successUI.GetComponent<AudioSource>().Play();
        Debug.Log("Success!");
        level++;
        levelText.text = level.ToString("0");
        sphereManager.matchSimilarityNum = 0;
        startingTime -= 2;

        if (level == 15)
        {
            Debug.Log("Win!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {

            yield return new WaitForSeconds(1.5f);
            successUI.GetComponent<Text>().enabled = false;
            resetTimer();
            loadFinished = false;
            sphereManager.matchSimilarityNum = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            yield break;
        }
    }

}
