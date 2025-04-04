using NUnit.Framework;
using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;

public class BillionBehavior : MonoBehaviour
{
    [SerializeField] public string BillionColor;
    [SerializeField] float moveSpeed;
    [SerializeField] GameObject BillionImage;
    [SerializeField] GameObject Bullet;
    Rigidbody2D rb2d;
    List<GameObject> flagList;

    Vector3 objectPos;
    Vector3 testVector;
    FlagPlacing flagPlacing;
    int HP = 100;
    public Vector3 target;

    UnityEvent debugDamageEvent = new UnityEvent();
    UnityEvent shootTime = new UnityEvent();

    void Start()
    {
        flagPlacing = GameObject.FindGameObjectWithTag("GameController").GetComponent<FlagPlacing>();
        debugDamageEvent.AddListener(DamageDebug);
        shootTime.AddListener(ShootBullet);
        rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        MoveToFlag();
        DamageDebugInput();
        FindNearestTarget();
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

    void FindNearestTarget()
    {
        // largely found from https://stackoverflow.com/questions/33145365/what-is-the-most-effective-way-to-get-closest-target and
        // https://discussions.unity.com/t/how-do-i-rotate-a-2d-object-to-face-another-object/187072
        GameObject[] billions = GameObject.FindGameObjectsWithTag("Billion");
        GameObject closest = null;
        float d = Mathf.Infinity;
        foreach(GameObject billion in billions)
        {
            BillionBehavior bb = billion.GetComponent<BillionBehavior>();
            if (!bb.BillionColor.Equals(BillionColor)) // hopefully this translates to it ignoring billions of the same color
            {
                Vector3 diff = billion.transform.position - transform.position;
                float curDistance = diff.sqrMagnitude;
                if (curDistance < d)
                {
                    closest = billion;
                    d = curDistance;
                }
            } 
        }

        target = closest.transform.position;
        target.x -= transform.position.x;
        target.y -= transform.position.y;
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle-90)); // idk why adding -90 makes it work but it does
        // revert in order to make shoot work
        target.x += transform.position.x;
        target.y += transform.position.y;
    }

    public void takeDamage(int damage)
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

    public void ShootBullet()
    {
        Vector3 diff = transform.position - target;
        float curDiff = diff.sqrMagnitude;
        Debug.Log(BillionColor + " " + curDiff);
        if (curDiff < 10f)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
            bullet.GetComponent<BulletBehavior>().bC = BillionColor;
        }
    }
}
