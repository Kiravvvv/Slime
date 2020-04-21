//Скрипт для назначения клавиш управления
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Input_setting : MonoBehaviour
{
    [Tooltip("Текст кнопки")]
    [SerializeField]
    private Text Button_text = null;

    [Tooltip("Ключ при сохранение данных")]
    [SerializeField]
    private string Save_key_name = null;

    [Tooltip("Клавиша по умолчанию")]
    [SerializeField]
    private KeyCode Default_key_code;

    private IEnumerator Coroutine;//Работающая сейчас карутина


    private void Start()
    {
        Load_key();
    }

    void Load_key()//Загрузить кнопку из памяти
    {
        if (PlayerPrefs.HasKey(Save_key_name))
        {
            string key_name = PlayerPrefs.GetString(Save_key_name);
            Default_key_code = (KeyCode)System.Enum.Parse(typeof(KeyCode), key_name);
            Button_text.text = PlayerPrefs.GetString(Save_key_name);
        }
        else
        {
            PlayerPrefs.SetString(Save_key_name, Default_key_code.ToString());
            Button_text.text = Default_key_code.ToString();
        }
    }

    public void ButtonSetKey() // событие кнопки, для перехода в режим ожидания
    {
        if (Coroutine == null)
        {
            Button_text.text = "???";
            Coroutine = Wait();//Заносит в переменную карутину
            StartCoroutine(Coroutine);//Запускает карутину
        }
    }

    // Ждем, когда игрок нажмет какую-нибудь клавишу, для привязки
    // Если будет нажата клавиша 'Escape', то отмена
    IEnumerator Wait()
    {
        while (true)
        {
            yield return null;

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                StopCoroutine(Coroutine);
                Coroutine = null;
                Load_key();
            }

            else
            {
                foreach (KeyCode _key in KeyCode.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(_key))
                    {
                        Default_key_code = _key;
                        Button_text.text = _key.ToString();
                        PlayerPrefs.SetString(Save_key_name, _key.ToString());

                        if (FindObjectOfType<Setting_menu>())
                            Setting_menu.Instance.Input_key_control();//Высылает всем заинтересованным в изменение управления, через другой скрипт

                        StopCoroutine(Coroutine);
                        Coroutine = null;
                    }
                }
            }

        }
    }
}
