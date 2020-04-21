using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_administrator : Singleton<Game_administrator>
{

    [Tooltip("Количество очков")]
    [SerializeField]
    int Score = 0;

    public void Add_score(int _score)//Добавить очки
    {
        Score += _score;
        Interface_player.Instance.Score_add(Score);
    }

}
