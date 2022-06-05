
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtility
{
    public static float RoundDecimals(float value, int decimalAmount)
        => (float)System.Math.Round(value, decimalAmount);

    public static int ConvertToMiliseconds(float seconds)
        => (int)(seconds * 0.001f);

    public static bool IsValueBetween(float value, float min, float max, bool inclusive = false)
    {
        if (inclusive == false)
        {
            if (value >= min && value <= max)
                return true;
            else
                return false;
        }

        else
        {
            if (value > min && value < max)
                return true;
            else
                return false;
        }
    }
    public static bool IsValueBetween(int value, int min, int max, bool inclusive = false)
    {
        if (inclusive == false)
        {
            if (value >= min && value <= max)
                return true;
            else
                return false;
        }

        else
        {
            if (value > min && value < max)
                return true;
            else
                return false;
        }
    }

    public static int GetIntegerFromFloat(float value)
        => (int)System.MathF.Truncate(value);

    public static void SplitFloat(float value, ref int integer, ref float remainder)
    {
        integer = GetIntegerFromFloat(value);
        remainder = Mathf.Abs(integer - value);
    }
}