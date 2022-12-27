using Assets.Scripts;
using System.IO;
using UnityEngine;

public class CheckRedStone : MonoBehaviour
{
    private GameData _gameData;
    private bool _spawnRedstone;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ReadData();
        if (collision.gameObject.CompareTag("Obstancle"))
        {
            //Debug.Log("Check contact");
            _gameData._spawnRedstone = true;
            
            SaveData();

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
