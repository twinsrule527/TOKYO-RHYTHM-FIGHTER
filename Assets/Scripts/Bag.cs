using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bag<T>
{

    /*
        List wrapper w/ utility functions 
        bosses pull attack patterns from a Bag
        made generic in case it comes up later 

    */

    List<T> list = new List<T>();

    T lastDrawn;

    //The default lineup for this bag. 
    //With refillFromLineup=true, when the bag empties, it will refill itself with the
    //default lineup declared here 
    //ex. the tetris lineup is 2 of each piece 
    List<T> lineup = new List<T>();

    public bool refill = true;


    //Add something to the default lineup, which is what refills the bag when empty.
    public void AddToLineup(T item) {
        lineup.Add(item);
    }

    //Add a number of something to the default lineup, which is what refills the bag. 
    public void AddToLineup(T item, int num) {
        for(int i = 0; i < num; i++) {
            lineup.Add(item);
        }
    }

    //Set a whole list as the lineup. 
    public void SetLineup(List<T> li) {
        lineup = new List<T>(li);
    }

    //gives a count of elements in the lineup
    public int CountLineup {
        get  {
            return lineup.Count;
        }
    }

    //Add something to the bag. 
    public void Add(T item) {
        list.Add(item);
    }

    //Add a number of something to the bag.
    public void Add(T item, int num) {
        for(int i = 0; i < num; i++) {
            list.Add(item);
        }
    }

    //gives a count of elements currently in the bag
    public int Count {
        get  {
            return list.Count;
        }
    }

    //Set the whole bag as a list.
    public void SetBag(List<T> li) {
        list = new List<T>(li);
    }

    //Clear the bag. 
    public void Clear() {
        list.Clear();
    }

    //Draw a random item from the bag, without replacement. (the item is removed)
    public T Draw() {
        int rand = Random.Range(0, list.Count);
        lastDrawn = list[rand];
        list.RemoveAt(rand);
        RefillIfNeeded();
        return lastDrawn;
    }

    //Draw a random item from the bag, with replacement. (the item is not removed)
    public T DrawWithoutReplacement() {
        int rand = Random.Range(0, list.Count);
        lastDrawn = list[rand];
        return list[rand];
    }

    //draw a random item from the bag, ensuring it's not the same item that was drawn last.
    //the item is removed 
    public T DrawNoRepeat() {
        while(true) {
            int rand = Random.Range(0, list.Count);
            if(!lastDrawn.Equals(list[rand])) {
                lastDrawn = list[rand];
                list.RemoveAt(rand);
                RefillIfNeeded();
                return lastDrawn;
            }
        } 
    }

    //TODO if needed, a "draw no repeat with replace" 


    //set the bag to what's in the lineup.
    //mostly just wanted to take advantage of making a copy 
    public void ClearAndRefill() {
        list = new List<T>(lineup);
    }

    //Refill the bag from the lineup without clearing it first. 
    public void Refill() {
        for(int i = 0; i < lineup.Count; i++) {
            list.Add(lineup[i]);
        }
    }

    public void RefillIfNeeded() {
        if(refill && list.Count == 0) {
            ClearAndRefill();
        }
    }


    //TODO 
    //checking to prevent too many in a row at "seams" of bag 
    //ie last 2 then first 2 means 4 total 

}
