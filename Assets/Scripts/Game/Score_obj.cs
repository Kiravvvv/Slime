//Скрипт подбираемых очков
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score_obj : MonoBehaviour
{
    [Tooltip("Сколько очков добавляет")]
    [SerializeField]
    int Score_add = 1;

    [Tooltip("Частицы после подбирания объекта")]
    [SerializeField]
    ParticleSystem Picking_up_particle = null;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Game_administrator.Instance.Add_score(Score_add);

            if (Picking_up_particle)
                Instantiate(Picking_up_particle, transform.position, Picking_up_particle.transform.rotation);

            Destroy(gameObject);
        }
    }
}
