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


    //Add something to the bag. 
    public void Add(T item) {
        list.Add(item);
    }

    //Add a number of something to the bag.
    public void Add(T item, int num) {
        for(int i = 0; i < num; i++) {
            Add(item);
        }
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
                return lastDrawn;
            }
        } 
    }

    //TODO if needed, a "draw no repeat with replace" 


}
