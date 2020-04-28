using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsValue {

    public int score;
    public string winningType;
    public int highCardValue;


    public CardsValue(int newScore, string newWinType, int newHighCardValue){

        score = newScore;
        winningType = newWinType;
        highCardValue = newHighCardValue;

    }

}
