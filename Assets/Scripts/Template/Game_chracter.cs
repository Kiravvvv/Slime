//Набор параметров для персонажей
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Game_chracter : Game_object
{
    [Tooltip("Скорость")]
    [SerializeField]
    protected float Speed = 1f;

    [Tooltip("Скорость поворота")]
    [SerializeField]
    protected float Speed_rotation = 1f;

    [Tooltip("Физика")]
    [SerializeField]
    protected Rigidbody Body = null;

    [Tooltip("Аниматор")]
    [SerializeField]
    protected Animator Anim = null;

    protected Transform My_transform = null;//Трансформ объекта 

    protected override void Start()
    {
        My_transform = transform;

        if(GetComponent<Rigidbody>())
        Body = GetComponent<Rigidbody>();
        base.Start();
    }
}
