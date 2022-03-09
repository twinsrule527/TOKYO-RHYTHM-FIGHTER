using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class DataCollector : MonoBehaviour
{
    StringBuilder dataStringBuilder = new StringBuilder();

    // Start is called before the first frame update
    void Start()
    {
        dataStringBuilder.AppendLine("Beat,Fraction,Abs,Accuracy,Key");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
            dataStringBuilder.AppendLine(BeatController.GetBeat() + "," + BeatController.GetDistanceFromBeat(1) + ",\"" + BeatController.GetAbsDistanceFromBeat(1) + "\"," + BeatController.GetAccuracy(1).name + ",\"" + Input.inputString + "\"");
        }
    }

    void OnApplicationQuit() {

        System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + @"DATA\DATA " + System.DateTime.Now.ToString("MM.dd.y ddd h.mm tt") + ".csv", dataStringBuilder.ToString());

    }
}
