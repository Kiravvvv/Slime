//Скрипт для портала
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_trigger : MonoBehaviour
{

    [Tooltip("Скрипт персонажа игрока")]
    [SerializeField]
    Player_control Player_control_ = null;

    [Tooltip("Меню конца игры")]
    [SerializeField]
    Canvas End_game = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            End_game.gameObject.SetActive(true);
            Player_control_.enabled = false;
        }
    }

}
