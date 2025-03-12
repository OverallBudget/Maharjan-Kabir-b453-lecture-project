using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BillionBehavior : MonoBehaviour
{
    [SerializeField] string BillionColor;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject BillionImage;
    Rigidbody2D rb2d;
    List<GameObject> flagList;

    Vector3 objectPos;
    Vector3 testVector;
    FlagPlacing flagPlacing;
    string colorType;
    int HP = 100;

    UnityEvent debugDamageEvent = new UnityEvent();
    private void Start()
    {
        flagPlacing = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlagPlacing>();
        debugDamageEvent.AddListener(DamageDebug);
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MoveToFlag();
        DamageDebugInput();
    }

    void MoveToFlag()
    {
        flagList = flagPlacing.ColorCall(BillionColor);
        if(flagList.Count == 0)
        {
            return;
        }
        else if (flagList.Count == 1)
        {
            objectPos = flagList[0].transform.position;
        }
        else if (flagList.Count == 2)
        {
            float d1 = Vector3.Distance(transform.position, flagList[0].transform.position);
            float d2 = Vector3.Distance(transform.position, flagList[1].transform.position);
            if (d1 < d2)
            {
                objectPos = flagList[0].transform.position;
            }
            else
            {
                objectPos = flagList[1].transform.position;
            }
        }
        testVector = objectPos - transform.position;
        testVector.Normalize();
        //transform.position = Vector2.MoveTowards(transform.position, objectPos, moveSpeed*Time.deltaTime);
        float d = Vector3.Distance(objectPos, transform.position);
        if(rb2d.linearVelocity.magnitude < 3f && d > 3)
        {
            rb2d.AddForce(testVector * moveSpeed);
        }
    }

    void takeDamage(int damage)
    {
        if(BillionImage != null) // temp
        {
            HP -= damage;
            Debug.Log("Damage Taken. HP at " + HP + " now.");
            switch (HP)
            {
                case (> 75):
                    break; // 100% - 76% hp
                case (> 50):
                    BillionImage.transform.localScale *= 0.9f;
                    break; // 75% - 51%
                case (> 25):
                    BillionImage.transform.localScale *= 0.9f;
                    break; // 50% - 26%
                case (> 0):
                    BillionImage.transform.localScale *= 0.9f;
                    break; // 25% - 1%
                case (<= 0):
                    Destroy(gameObject);
                    break;
            }
        }
        
    }

    void DamageDebugInput()
    {
        if (Input.GetMouseButtonDown(2))
        {
            debugDamageEvent.Invoke();
        }
    }

    void DamageDebug()
    {
        takeDamage(25);
    }
}
