using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class BaseBehavior : MonoBehaviour
{
    [SerializeField] public string BaseColor;
    [SerializeField] GameObject Bullet;
    [SerializeField] float rotationSpeed;

    public Vector3 target;
    

    UnityEvent shootTime = new UnityEvent();

    private void Start()
    {
        shootTime.AddListener(ShootLaser);
    }
    private void Update()
    {
        FindNearestTarget();
    }
    void FindNearestTarget()
    {
        // largely found from https://stackoverflow.com/questions/33145365/what-is-the-most-effective-way-to-get-closest-target and
        // https://discussions.unity.com/t/how-do-i-rotate-a-2d-object-to-face-another-object/187072
        GameObject[] billions = GameObject.FindGameObjectsWithTag("Billion");
        GameObject closest = null;

        if(billions.Length != 0)
        {
            //Debug.Log(billions.Length);
            float d = Mathf.Infinity;
            foreach (GameObject billion in billions)
            {
                BillionBehavior bb = billion.GetComponent<BillionBehavior>();
                if (!bb.BillionColor.Equals(BaseColor)) // hopefully this translates to it ignoring billions of the same color
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
            // transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // idk why adding -90 makes it work but it does
            // revert in order to make shoot work
            Quaternion endRotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
            transform.rotation = Quaternion.Lerp(transform.rotation, endRotation, rotationSpeed * Time.deltaTime);

            //why is the error showing here, but does nothing
            target.x += transform.position.x;
            target.y += transform.position.y;
        }
        
    }

    public void ShootLaser()
    {

        //Debug.Log("Shooting Laser");
        Vector3 diff = transform.position - target;
        float curDiff = diff.sqrMagnitude;
        //Debug.Log(BaseColor + " " + curDiff);
        if (curDiff < 10f)
        {
            GameObject bullet = Instantiate(Bullet, transform.position, transform.rotation);
            bullet.GetComponent<BulletBehavior>().bC = BaseColor;
        }
    }

}
