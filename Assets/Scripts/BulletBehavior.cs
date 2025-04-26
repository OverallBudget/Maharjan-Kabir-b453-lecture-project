using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;
    [SerializeField] public int level;

    [SerializeField] string bulletColor;
    public string bC
    {
        get
        {
            return bulletColor;
        }
        set
        {
            bulletColor = value;
        }
    }

    void Start()
    {
        damage = damage + 15 * level;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        transform.position += transform.up * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (other.CompareTag("Base"))
        {
            if (!other.GetComponent<BillionBase>().color.Equals(bulletColor))
            {
                //Debug.Log(bulletColor + " colliding with " + other.GetComponent<BillionBehavior>().BillionColor);
                other.GetComponent<BillionBase>().takeDamage(damage);
                Destroy(gameObject);
            }
        }

        if (other.CompareTag("Billion"))
        {
            Debug.Log("Found Billion");
            if (!other.GetComponent<BillionBehavior>().BillionColor.Equals(bulletColor))
            {
                
                //Debug.Log(bulletColor + " colliding with " + other.GetComponent<BillionBehavior>().BillionColor);
                other.GetComponent<BillionBehavior>().takeDamage(damage);
                other.GetComponent<BillionBehavior>().deathCause(bulletColor);
                Destroy(gameObject);
            }
        }
    }
}
