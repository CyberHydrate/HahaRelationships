using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcController
{
    private static readonly NpcController mInstance = new NpcController();

    public static NpcController Instance => mInstance;

    private NpcController() { }//构造函数私有化，确保外部无法实例化

    int positiveValue=1;
    int normalValue=1;
    int extremeValue = 1;
    int chaosValue = 1;
    int conservativeValue = 1;

    public Action GetChoice(BlockEvent blockEvent)
    {
        int i = blockEvent.choiceCount;
        int sum = 0;
        int[] value = { 0, 0, 0, 0, 0 };
        for (int j = 0; j < i; j++)
        {
            switch (blockEvent.choices[j].choiceType)
            {
                case E_ChoiceType.positive:
                    value[j] = positiveValue;
                    sum += positiveValue;
                    break;
                case E_ChoiceType.normal:
                    value[j] = normalValue;
                    sum += normalValue;
                    break;
                case E_ChoiceType.extreme:
                    value[j] = extremeValue;
                    sum += extremeValue;
                    break;
                case E_ChoiceType.chaos:
                    value[j] = chaosValue;
                    sum += chaosValue;
                    break;
                case E_ChoiceType.conservative:
                    value[j] = conservativeValue;
                    sum += conservativeValue;
                    break;
                default:
                    break;
            }
        }
        Debug.Log("sum" + sum.ToString());
        int random = UnityEngine.Random.Range(0, sum);
        if (random < value[0])
        {

            if(value[0] == 0)
            {
                return null;
            }
            return blockEvent.choices[0].choiceFunc;
        }
        else if (random < value[0] + value[1])
        {

            if (value[1]==0) 
            { 
                return null; 
            }
            return blockEvent.choices[1].choiceFunc;
        }
        else if(random < value[0]  + value[1] + value[2])
        {

            if (value[2] == 0)
            {
                return null;
            }
            return blockEvent.choices[2].choiceFunc;
        }
        else if(random<value[0] + value[1] + value[2] + value[3])
        {

            if (value[3] == 0)
            {
                return null;
            }
            return blockEvent.choices[3].choiceFunc;
        }
        else
        {

            if (value[4] == 0)
            {
                return null;
            }
            return blockEvent.choices[4].choiceFunc;
        }
    }
}
