using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckMatching : MonoBehaviour
{

    public static int CountTrue(bool[] array, bool flag)
    {
        int value = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == flag)
            {
                value++;
            }
        }

        return value;
    }
}
