using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] int damage;

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

        if (other.CompareTag("Billion"))
        {
            Debug.Log("Found Billion");
            if (!other.GetComponent<BillionBehavior>().BillionColor.Equals(bulletColor))
            {
                Debug.Log(bulletColor + " colliding with " + other.GetComponent<BillionBehavior>().BillionColor);
                other.GetComponent<BillionBehavior>().takeDamage(damage);
                Destroy(gameObject);
            }
        }
    }
}
