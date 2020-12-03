﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CanvasGroup ballTypePanel;
    //public CanvasGroup bowlingPanel;

    public CanvasGroup gridPanel;

    public CanvasGroup runsPanel;

    public Button batButton;
    public Button ballButton;

    public Button spinButton;
    public Button fastButton;

    public Text ballGridText;

    public Text scoreText;
    public Text oversText;

    public int ballsLeft = 30;

    public int wickets = 0;

    public int runs = 0;

    public int target = 60;

    private string cBallType = "spin";

    public GameObject ball;

    public GameObject pitchCamera;

    //public List<Button> gridButtons = new List<Button>();

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        HidePanel(gridPanel);
        HidePanel(runsPanel);
        ShowPanel(ballTypePanel);
        //HidePanel(bowlingPanel);
        
        batButton.onClick.AddListener(ChooseBat);
        ballButton.onClick.AddListener(ChooseBall);
        spinButton.onClick.AddListener(delegate { BallTypeChoose(true); });
        fastButton.onClick.AddListener(delegate { BallTypeChoose(false); });
        ballsLeft = 30;
        runs = 0;
        wickets = 0;
    }

    public void GridSelected(int value)
    {
        Debug.Log(value);
        //ShowPanel(bowlingPanel);
        ballGridText.text = cBallType + " ball " + value;
        StartCoroutine(MovePiece(ball, new Vector3(-4.5f, 3f, 17f), 1f));
        StartCoroutine(HideBallDisplaySlow());
        //BallOver();
    }

    IEnumerator HideBallDisplaySlow()
    {
        yield return new WaitForSeconds(1f);
        //HidePanel(bowlingPanel);
        
        ballGridText.text = "";
        HidePanel(gridPanel);
        ShowPanel(runsPanel);
    }

    void BallTypeChoose(bool spin)
    {
        if (spin)
        {
            cBallType = "SPIN";
        }
        else
        {
            cBallType = "FAST";
        }
        HidePanel(ballTypePanel);
        ShowPanel(gridPanel);
        //ShowPanel(bowlingPanel);
    }

    public void BatRunsSelected(int value,int prob,Vector3 dest)
    {

        float random = Random.Range(0f, 100f);
        if (random < prob)
        {
            pitchCamera.SetActive(false);
            StartCoroutine(MovePiece(ball, dest, 1f));
            ballGridText.text = value + " RUNS";
            runs = runs + value;
        }
        else
        {
            int r2 = Random.Range(0, 2);
            if (r2 == 0)
            {
                ballGridText.text = "OUT";
                wickets++;
            }
            else
            {
                ballGridText.text="MISS";
            }
        }
        HidePanel(runsPanel);
        StartCoroutine(HideBatDisplaySlow());
        
    }

    IEnumerator HideBatDisplaySlow()
    {
        yield return new WaitForSeconds(1f);
        //HidePanel(bowlingPanel);
        ballGridText.text = "";
        
        ShowPanel(gridPanel);
        BallOver();
    }

    void BallOver()
    {
        ballsLeft--;
        if (wickets == 5)
        {
            HidePanel(gridPanel);
            HidePanel(runsPanel);
            ballGridText.text = "Bowlers Win";
        }
        if (runs >= target)
        {
            HidePanel(gridPanel);
            HidePanel(runsPanel);
            ballGridText.text = "Batsmen Win";
        }
        if (ballsLeft > 0)
        {
            if (ballsLeft % 6 == 0)
            {
                HidePanel(gridPanel);
                //HidePanel(bowlingPanel);
                ShowPanel(ballTypePanel);
            }
            else
            {

            }
        }
        else
        {
            HidePanel(gridPanel);
            HidePanel(runsPanel);
            ballGridText.text = "Bowlers Win";
        }
        ball.transform.position = new Vector3(-4.5f, 3f, -17f);
        pitchCamera.SetActive(true);
    }
    
    void ChooseBat()
    {
        
        ballsLeft = 30;
    }

    void ChooseBall()
    {
        Debug.Log("Ball Choose");
        
        ballsLeft = 30;
       // HidePanel(batballPanel);
        ShowPanel(ballTypePanel);
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "SCORE: "+runs + "/" + wickets;
        int overs = (30 - ballsLeft) / 6;
        int balls = (30 - ballsLeft) % 6;
        oversText.text ="OVERS: "+ overs + "." + balls + "(5)";
    }

    public IEnumerator MovePiece(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
    }

    void ShowPanel(CanvasGroup x)
    {
        x.alpha = 1;
        x.blocksRaycasts = true;
        
    }

    void HidePanel(CanvasGroup x)
    {
        x.alpha = 0;
        x.blocksRaycasts = false;
    }
}
