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

    public Action GetChoice(int eventId)
    {
        int i = ExcelReader.eventData[eventId].choiceCount;
        int sum = 0;
        int[] value = { 0, 0, 0, 0, 0 };
        for (int j = 0; j < i; j++)
        {
            switch (ExcelReader.eventData[eventId].choices[j].choiceType)
            {
                case E_ChoiceType.积极:
                    value[j] = positiveValue;
                    sum += positiveValue;
                    break;
                case E_ChoiceType.正常:
                    value[j] = normalValue;
                    sum += normalValue;
                    break;
                case E_ChoiceType.极端:
                    value[j] = extremeValue;
                    sum += extremeValue;
                    break;
                case E_ChoiceType.混沌:
                    value[j] = chaosValue;
                    sum += chaosValue;
                    break;
                case E_ChoiceType.保守:
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
            return Events.events[eventId].choices[0];
        }
        else if (random < value[0] + value[1])
        {

            if (value[1]==0) 
            { 
                return null; 
            }
            return Events.events[eventId].choices[1];
        }
        else if(random < value[0]  + value[1] + value[2])
        {

            if (value[2] == 0)
            {
                return null;
            }
            return Events.events[eventId].choices[2];
        }
        else if(random<value[0] + value[1] + value[2] + value[3])
        {

            if (value[3] == 0)
            {
                return null;
            }
            return Events.events[eventId].choices[3];
        }
        else
        {

            if (value[4] == 0)
            {
                return null;
            }
            return Events.events[eventId].choices[4];
        }
    }
}
