using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Interface_player : Singleton<Interface_player>
{

    [Tooltip("Картинка заполненности")]
    [SerializeField]
    Image Progress_jump_image = null;

    [Tooltip("Показатель количества очков")]
    [SerializeField]
    Text Score_text = null;

    public void Progress_bar_jump (float _progress)//Отображение заполненности шкалы прыжка
    {
        Progress_jump_image.fillAmount = _progress;
    }

    public void Score_add(int _value)//Отображение количество очков
    {
        Score_text.text = _value.ToString();
    }

}
