using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    internal class GameData
    {
        public float _speed = 0;
        public bool _isRunning = false;
        public int _score = 0;
        public int _highestScore = 0;
        public bool _spawnRedstone = true;
        public bool _spawnGround = true;
        public bool _spawnHorn = false;
        public string _gameStatus = "Pause";
        public float _energy = 500;
    }
}
