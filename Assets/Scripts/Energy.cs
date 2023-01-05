using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Energy : MonoBehaviour
{
    private GameData _gameData;

    public GameObject EnergyCircle;

    public GameObject Energy1;
    public GameObject Energy2;
    public GameObject Energy3;
    public GameObject Energy4;
    public GameObject Energy5;

    private RectMask2D Energy1_Mask;
    private RectMask2D Energy2_Mask;
    private RectMask2D Energy3_Mask;
    private RectMask2D Energy4_Mask;
    private RectMask2D Energy5_Mask;


    // Start is called before the first frame update
    void Start()
    {
        Energy1_Mask = Energy1.GetComponent<RectMask2D>();
        Energy2_Mask = Energy2.GetComponent<RectMask2D>();
        Energy3_Mask = Energy3.GetComponent<RectMask2D>();
        Energy4_Mask = Energy4.GetComponent<RectMask2D>();
        Energy5_Mask = Energy5.GetComponent<RectMask2D>();
 
    }

    // Update is called once per frame
    void Update()
    {
        ReadData();
        

        if (_gameData._gameStatus.Equals("End"))
        {
            EnergyCircle.transform.position = new Vector3(0, 1000);
        }
        else
        {
            var Energy1_padding = Energy1_Mask.padding;
            var Energy2_padding = Energy2_Mask.padding;
            var Energy3_padding = Energy3_Mask.padding;
            var Energy4_padding = Energy4_Mask.padding;
            var Energy5_padding = Energy5_Mask.padding;

            //Vì Rect 2d không xoay theo ???c nên ? ?ây ch? padding mask theo y (bottom)
            //? ?ây s? tìm ra 2 biên, dùng công th?c:  biên trên - ?? cao / 100 * n?ng l??ng c?c b? (n?ng l??ng t?ng - m?c cánh)
            //For energy 1
            if (_gameData._energy >= 10)
            {
                Energy1_padding.x = 120 - 1.2f * (_gameData._energy);
                Energy1_padding.y = 30 - 0.9f * (_gameData._energy);
            }
            else
            {
                Energy1_padding.x = 120;
                Energy1_padding.y = 30;
            }

            //For energy 2
            if (_gameData._energy >= 100)
            {
                Energy2_padding.x = 100 - 0.9f * (_gameData._energy - 100);
                Energy2_padding.y = 100 - 1.35f * (_gameData._energy - 100);
            }
            else
            {
                Energy2_padding.x = 100;
                Energy2_padding.y = 100;
            }

            //For energy 3
            if (_gameData._energy >= 200)
            {
                Energy3_padding.y = 120 - 1.2f * (_gameData._energy - 200);
            }
            else
            {
                Energy3_padding.y = 120;
            }

            //For energy 4
            if (_gameData._energy >= 300)
            {
                Energy4_padding.z = 20 - 0.8f * (_gameData._energy - 300);
                Energy4_padding.y = 125 - 1.25f * (_gameData._energy - 300);
            }
            else
            {
                Energy4_padding.z = 20;
                Energy4_padding.y = 125;
            }

            //For energy 5
            if (_gameData._energy >= 400)
            {
                Energy5_padding.z = 0 - 1.2f * (_gameData._energy - 400);
                //Energy5_padding.y = 130 - 1.3f * (_gameData._energy - 400);
            }
            else
            {
                Energy5_padding.z = 0;
                //Energy5_padding.y = 100;
            }



            Energy1_Mask.padding = Energy1_padding;
            Energy2_Mask.padding = Energy2_padding;
            Energy3_Mask.padding = Energy3_padding;
            Energy4_Mask.padding = Energy4_padding;
            Energy5_Mask.padding = Energy5_padding;
        }

        
    }

    private void ReadData()
    {
        string jsonRead;
        try
        {
            jsonRead = System.IO.File.ReadAllText("data.txt");
            _gameData = JsonUtility.FromJson<GameData>(jsonRead);
        }
        catch (FileNotFoundException)
        {
            _gameData = new GameData();
            string json = JsonUtility.ToJson(_gameData);
            System.IO.File.WriteAllText("data.txt", json);
        }
    }

    private void SaveData()
    {
        string json = JsonUtility.ToJson(_gameData);
        //Debug.Log(json);
        System.IO.File.WriteAllText("data.txt", json);
    }
}
