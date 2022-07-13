using System.Collections;
using System.Collections.Generic;

public static class ScoreMaster
{
    // returns a list of cumulative scores, like a normal score card.
    public static List<int> ScoreCumulative(List<int> rolls)
    {
        List<int> comulativeScores = new List<int>();
        int runningTotal = 0;

        foreach (int frameScore in ScoreFrames(rolls))
        {
            runningTotal += frameScore;
            comulativeScores.Add(runningTotal);
        }

        return comulativeScores;
    }

    // returns a list of individual scores, NOT cumulative.
    public static List<int> ScoreFrames(List<int> rolls)
    {
        List<int> frames = new List<int>();

        for (int bowl = 1; bowl < rolls.Count; bowl += 2)
        {
            if (frames.Count == 10) // prevents 11th frame score 
            {
                break;
            }
            if (rolls[bowl - 1] + rolls[bowl] < 10) // Normal OPEN Frame
            {
                frames.Add(rolls[bowl - 1] + rolls[bowl]);
            }
            if (rolls.Count - bowl <= 1) // Insufficient look-ahead, ensure atleast 1 look-ahead is available
            {
                break;
            }
            if (rolls[bowl - 1] == 10) // STRIKE
            {
                bowl--; // deduct 1 bowl cuz STRIKE frame has 1 bowl
                frames.Add(10 + rolls[bowl + 1] + rolls[bowl + 2]);
            }
            else if (rolls[bowl - 1] + rolls[bowl] == 10) // Calculate SPARE Bonus
            {
                frames.Add(10 + rolls[bowl + 1]);
            }
        }

        return frames;
    }
}
