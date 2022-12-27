using System;
using System.IO;
using UnityEngine;

namespace FileHandler
{
    public class FileHandler
    {
        public static void saveSpeed(float speed)
        {
            string strOutput = JsonUtility.ToJson(speed);
            //File.WriteAllText(Application.dataPath + "/data/speed.txt", strOutput);
        }

        public static void saveHighestScore(int highest)
        {
            string json = JsonUtility.ToJson(highest);
            Debug.Log(json);
        }
    }
}