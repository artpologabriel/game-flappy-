using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Card : IComparable<Card>
{
    
    public string name;
    public int value1;
    public int value2;
    public Sprite sprite;

    
    public Card(string newName, int newValue1,int newValue2 , Sprite newSprite ){

        name = newName;
        value1 = newValue1;
        value2 = newValue2;
        sprite = newSprite;
        

    }
    
    

    public int CompareTo(Card other){

        if (other == null) {
            return 1;
        }
        return  value1 - other.value1;
    }
    
    /*
    public Card(string newName, int newValue1,int newValue2 , Texture TextureName ){

        name = newName;
        value1 = newValue1;
        value2 = newValue2;
        texture = TextureName;

    }
    */


}
