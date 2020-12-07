using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public UIController uiController;
    public GameObject spawner;
    public GameObject vehicle;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Terrain")
        {
            uiController.onDie();
        }
    }

    public void Respawn()
    {
        float randomValue = Random.Range(0.00001f, 0.3f);

        vehicle.transform.position = new Vector3(
            spawner.transform.position.x + randomValue + 5f,
            spawner.transform.position.y,
            vehicle.transform.position.z
        );
        vehicle.transform.rotation = new Quaternion(0, 0, randomValue, 0f);
    }
}
