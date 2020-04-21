//Скрипт для управления персонажем игрока
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_control : Game_chracter
{
    [Tooltip("Камера")]
    [SerializeField]
    Transform Camera_transform = null;

    [Tooltip("Скорость заряда силы прыжка")]
    [SerializeField]
    float Add_power_jump = 10f;

    [Tooltip("Скорость заряда силы прыжка")]
    [SerializeField]
    float Max_power_jump = 10f;

    [Tooltip("Частицы при призимление")]
    [SerializeField]
    ParticleSystem Landing_particle = null;

    KeyCode Jump_key = KeyCode.Space;//Кнопка отвечающая за прыжок

    float Power_jump = 0;//Накопленная энергия для прыжка

    bool Grounded = true;//Являеться ли персонаж на земле

    protected override void Start()
    {
        base.Start();

        if (PlayerPrefs.HasKey("Key_jump"))
        {
            string key_name = PlayerPrefs.GetString("Key_jump");
            Jump_key = (KeyCode)System.Enum.Parse(typeof(KeyCode), key_name);
        }
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Ground" && !Grounded)
        {
            Grounded = true;
            Anim.SetBool("Jump", false);
            Anim.Play("Grounded");
            Sound_control_.Sound_play_2();
            Instantiate(Landing_particle, transform.position, Landing_particle.transform.rotation);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Ground")
            Grounded = false;
    }

    void Update()
    {
        Rotation();



        if (Grounded)
        {
            if (Input.GetKeyUp(Jump_key))
                Jump(Power_jump);

            if (Input.GetKey(Jump_key) && Power_jump < Max_power_jump)
                Jump_charge();
        }
    }

    void Jump(float _power)//Прыжок
    {
        Body.AddForce((transform.forward*0.5f + Vector3.up) * _power, ForceMode.Impulse);
        Power_jump = 0;
        Interface_player.Instance.Progress_bar_jump(0);

        Anim.SetBool("Jump", true);
        Anim.SetFloat("Jump_power", 0);
        Sound_control_.Sound_play_1();
    }

    void Jump_charge()//Наполнение силы прыжка
    {
        Power_jump += Add_power_jump;

        float index_power = Power_jump / Max_power_jump;

        Anim.SetFloat("Jump_power", index_power);

        Interface_player.Instance.Progress_bar_jump(index_power);
    }

    void Rotation()//Поворот камеры
    {
        Camera_transform.Rotate(new Vector3(Input.GetAxis("Mouse Y") * Speed_rotation, 0, 0));
        My_transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * Speed_rotation, 0));

    }
}
