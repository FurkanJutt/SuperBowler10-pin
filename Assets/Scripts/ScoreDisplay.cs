using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    public TextMeshProUGUI[] rollText, frameText;
    [SerializeField] TextMeshProUGUI finalScoreText;
    public string finalScore = "";

    public void FillRolls(List<int> rolls)
    {
        string scoresString = FormatRolls(rolls);
        for (int i = 0; i < scoresString.Length; i++)
        {
            rollText[i].text = scoresString[i].ToString();
        }
    }

    public void FillFrames(List<int> frames)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frameText[i].text = frames[i].ToString();
            finalScoreText.text = frameText[i].text;
            finalScore = finalScoreText.text;
        }
    }

    public static string FormatRolls(List<int> rolls)
    {
        string output = "";

        for (int i = 0; i < rolls.Count; i++)
        {
            int scoreBox = output.Length + 1; // Score box 1 to 21

            if (rolls[i] == 0) // always enter 0 as -
            {
                output += "-";
            }
            else if ((scoreBox % 2 == 0 || scoreBox == 21) && rolls[i - 1] + rolls[i] == 10) // SPARE
            {
                output += "/";
            }
            else if (scoreBox >= 19 && rolls[i] == 10) // Strike in frame 10
            {
                output += "X";
            }
            else if (rolls[i] == 10) // STRIKE in frame 1-9
            {
                output += "X ";
            }
            else // add the pinfalls count to previous output
            {
                output += rolls[i].ToString();
            }
        }

        return output;
    }
}
