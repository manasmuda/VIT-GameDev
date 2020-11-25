using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public CanvasGroup batballPanel;
    public CanvasGroup ballTypePanel;
    public CanvasGroup bowlingPanel;

    public CanvasGroup gridPanel;

    public Button batButton;
    public Button ballButton;

    public Button spinButton;
    public Button fastButton;

    public Text ballGridText;

    public bool currentBat = false;

    public int ballsLeft = 30;

    private string cBallType = "spin";

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
        HidePanel(ballTypePanel);
        HidePanel(bowlingPanel);
        ShowPanel(batballPanel);
        batButton.onClick.AddListener(ChooseBat);
        ballButton.onClick.AddListener(ChooseBall);
        spinButton.onClick.AddListener(delegate { BallTypeChoose(true); });
        fastButton.onClick.AddListener(delegate { BallTypeChoose(false); });
        Debug.Log("Start Finished");
    }

    public void GridSelected(int value)
    {
        Debug.Log(value);
        ShowPanel(bowlingPanel);
        ballGridText.text = cBallType + " ball " + value;
        StartCoroutine(HideDisplaySlow());
        BallOver();
    }

    IEnumerator HideDisplaySlow()
    {
        yield return new WaitForSeconds(1f);
        HidePanel(bowlingPanel);
        ballGridText.text = "";
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

    void BallOver()
    {
        ballsLeft--;
        if (ballsLeft > 0)
        {
            if (ballsLeft % 6 == 0)
            {
                HidePanel(gridPanel);
                HidePanel(bowlingPanel);
                ShowPanel(ballTypePanel);
            }
            else
            {

            }
        }
        else
        {

        }
    }
    
    void ChooseBat()
    {
        currentBat = true;
        ballsLeft = 30;
    }

    void ChooseBall()
    {
        Debug.Log("Ball Choose");
        currentBat = false;
        ballsLeft = 30;
        HidePanel(batballPanel);
        ShowPanel(ballTypePanel);
    }

    // Update is called once per frame
    void Update()
    {
        
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
