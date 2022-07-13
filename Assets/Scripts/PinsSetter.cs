using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PinsSetter : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI pinsCountText;
    [SerializeField] GameObject pinsSet;
    
    float DistToRaise = 70f;
    bool ballEntered = false;
    int lastStandingCount = -1;
    float lastChangeTime;
    int lastSettledCount = 10;
    List<int> rollsAsPinfall;

    private const string TIDY_TRIGGER = "TidyTrigger";
    private const string RESET_TRIGGER = "ResetTrigger";

    private Ball _ball;
    private ActionMaster _actionMaster = new ActionMaster();
    private Animator _animator;
    private ScoreDisplay _scoreDisplay;

    [SerializeField] GameObject loseCanvas;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        _ball = FindObjectOfType<Ball>();
        _scoreDisplay = FindObjectOfType<ScoreDisplay>();
        _animator = GetComponent<Animator>();
        rollsAsPinfall = new List<int>();
        pinsCountText.text = lastSettledCount.ToString();
        loseCanvas.SetActive(false);
    }

    int CountStandingPins()
    {
        int standingPinsCount = 0;
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin.IsStanding())
                standingPinsCount++;
        }
        return standingPinsCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(CountStandingPins());
        if (ballEntered)
        {
            pinsCountText.text = CountStandingPins().ToString();
            CheckIfStanding();
        }
    }

    private void CheckIfStanding()
    {
        int currentlyStanding = CountStandingPins();
        if (currentlyStanding != lastStandingCount)
        {
            lastChangeTime = Time.time;
            lastStandingCount = currentlyStanding;
            return;
        }

        float settleTime = 3f;
        if ((Time.time - lastChangeTime) > settleTime)
        {
            PinsHaveSettled();
        }
    }
    
    private void PinsHaveSettled()
    {
        int pinFall = lastSettledCount - CountStandingPins();
        lastSettledCount = CountStandingPins();
        ActionMaster.Action action = _actionMaster.Bowl(pinFall);
        rollsAsPinfall.Add(pinFall);
        _scoreDisplay.FillRolls(rollsAsPinfall);
        _scoreDisplay.FillFrames(ScoreMaster.ScoreCumulative(rollsAsPinfall));
        Debug.Log("PinFall: " + pinFall+", Action: "+action);

        if (action == ActionMaster.Action.Tidy)
        {
            _animator.SetTrigger(TIDY_TRIGGER);
        }
        else if (action == ActionMaster.Action.EndTurn || action == ActionMaster.Action.Reset)
        {
            _animator.SetTrigger(RESET_TRIGGER);
            lastSettledCount = 10;
        }
        else if (action == ActionMaster.Action.EndGame)
        {
            Time.timeScale = 0;
            loseCanvas.SetActive(true);
        }

        pinsCountText.color = Color.green;
        ballEntered = false;
        lastStandingCount = -1;
        _ball.ResetToStart();
    }

    public void RaisePins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if(pin)
                pin.Raise();
        }
    }

    public void LowerPins()
    {
        foreach (Pin pin in FindObjectsOfType<Pin>())
        {
            if (pin)
                pin.Lower();
        }
    }

    public void RenewPins()
    {
        Instantiate(pinsSet, new Vector3(0f, DistToRaise, 1829f), Quaternion.identity);
        pinsCountText.color = Color.white;
        pinsCountText.text = lastSettledCount.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Ball")
        {
            ballEntered = true;
            pinsCountText.color = Color.red;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Pin>())
            Destroy(other.gameObject);
        if (other.gameObject.GetComponent<Ball>())
            _ball.StopRollingSound();
    }
}
