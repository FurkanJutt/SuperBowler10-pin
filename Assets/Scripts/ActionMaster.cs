using System.Collections.Generic;
using UnityEngine;

public class ActionMaster
{
    public enum Action { Tidy, Reset, EndTurn, EndGame}

    private int[] bowls = new int[21];
    private int bowl = 1;

    public static Action NextAction(List<int> pinFalls)
    {
        ActionMaster actionMaster = new ActionMaster();
        Action currentAction = new Action();

        foreach (int pinfall in pinFalls)
        {
            currentAction = actionMaster.Bowl(pinfall);
        }
        return currentAction;
    }

    public Action Bowl(int pins)
    {
        if (pins < 0 || pins > 10) { throw new UnityException("Invalid Pins Count!"); }

        bowls[bowl - 1] = pins;

        if (bowl == 21) // Last Frame
        {
            return Action.EndGame;
        }
        // Handle last-frame Special cases
        if (bowl >= 19 && pins == 10)
        {
            bowl += 1;
            return Action.Reset;
        }
        else if(bowl == 20)
        {
            bowl += 1;
            if (bowls[19 - 1] == 10 && bowls[20 - 1] == 0)
            {
                return Action.Tidy;
            }
            else if (AreAllPinsKnockedDown())
            {
                return Action.Reset;
            }
            else if (Bowl21Awarded())
            {
                return Action.Tidy;
            }
            else
            {
                return Action.EndGame;
            }
        }

        if (bowl % 2 != 0) // First bowl of frame
        {
            if (pins == 10)
            {
                bowl += 2;
                return Action.EndTurn;
            }
            else
            {
                bowl += 1;
                return Action.Tidy;
            }
        }
        else if (bowl % 2 == 0) // Second bowl of frame
        {
            bowl += 1;
            return Action.EndTurn;
        }

        throw new UnityException("Not Sure What To Return!");
    }

    private bool AreAllPinsKnockedDown()
    {
        return ((bowls[19 - 1] + bowls[20 - 1]) % 10 == 0);
    }

    private bool Bowl21Awarded()
    {
        return (bowls[19-1] + bowls[20-1] >= 10);
    }
}
