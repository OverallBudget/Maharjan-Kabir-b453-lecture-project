using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;

public class ShootTimer : MonoBehaviour
{
    public static ShootTimer instance;

    float timer = 0f;
    float otherTimer = 0f;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }
    void Update()
    {
        TimerThing();
        SlowTimerThing();
    }

    void TimerThing()
    {
        timer += Time.deltaTime;
        if(timer > 1f)
        {
            //Debug.Log("reset time");
            timer = 0f;
            GameObject[] go = GameObject.FindGameObjectsWithTag("Billion");
            foreach(GameObject bill in go)
            {
                bill.GetComponent<BillionBehavior>().ShootBullet();
            }
        }
    }

    void SlowTimerThing()
    {
        Debug.Log("Test");
        otherTimer += Time.deltaTime;
        if (otherTimer > 3f)
        {
            //Debug.Log("reset slowtime");
            otherTimer = 0f;
            GameObject[] go = GameObject.FindGameObjectsWithTag("Base");
            
            foreach (GameObject b in go)
            {
                b.GetComponentInChildren<BaseBehavior>().ShootLaser();
            }
        }
    }
}
