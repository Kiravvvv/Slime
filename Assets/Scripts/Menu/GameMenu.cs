using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : Singleton<GameMenu> {

	[Tooltip("Чёрный фон для затемнения")]
	[SerializeField]
	GameObject Fon_black = null;

    [Header("Ссылка на игровое меню")]
    [SerializeField]
    GameObject Canvas_game_menu = null;

    [Header("Номер сцены которая ведёт в главное меню")]
    [SerializeField]
    int Scene_main_menu_number_ID = 1;

    [Header("Клавиша вкл/выкл меню ")]
    [SerializeField]
    KeyCode Exit_key = KeyCode.Escape;

    [Header("Ссылки на игровые вкладки")]
    [SerializeField]
    List<GameObject> Bookmark = new List<GameObject>();

    [Header("Будет влиять на курсор во время включения и выключения меню")]
    [SerializeField]
    bool Cursor_active = false;

    private void Awake()
    {
        if (Fon_black.activeSelf == false)
            Fon_black.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(Exit_key))
        {
            Enter_button();
        }

    }

    void Enter_button()//Включение и отключение игрового меню
    {
        if (!Canvas_game_menu.activeSelf)
        {
            Canvas_game_menu.SetActive(true);
            if (Cursor_active)
                Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            bool null_bookmark = true;

            for (int x = 0; x < Bookmark.Count; x++)
            {
                if (Bookmark[x].activeSelf)
                {
                    null_bookmark = false;
                    Bookmark[x].SetActive(false);
                }
            }
            if (null_bookmark)
            {
                Canvas_game_menu.SetActive(false);
                if (Cursor_active)
                    Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    //Продолжить дальше (выключить меню)
    public void Continue()
    {
        Canvas_game_menu.SetActive(false);
        Time.timeScale = 1;
    }

    //Перезагрузить сцену (уровень)
    public void Restart_scene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    //Выход в главное меню (загрузить сцену с главным меню)
    public void Load_main_menu(){
		Fon_black.transform.Find("Fon_black_end").gameObject.SetActive (true);
        Fon_black.transform.Find("Fon_black_end").GetComponent<Load_scene>().Scene_number = Scene_main_menu_number_ID;
        Time.timeScale = 1;
        Canvas_game_menu.SetActive (false);
	}

    public void Load_scene(int _id_scene)//Загрузить указанную сцену (уровень)
    {
        Fon_black.transform.Find("Fon_black_end").gameObject.SetActive(true);
        Fon_black.transform.Find("Fon_black_end").GetComponent<Load_scene>().Scene_number = _id_scene;
        Time.timeScale = 1;
        Canvas_game_menu.SetActive(false);
    }

    public void Exit_game()//Выключить игру
    {
        Application.Quit();
    }
		
}
