using UnityEngine;

public class BillionBase : MonoBehaviour
{
    [SerializeField] GameObject BillionType;
    [SerializeField] public int maxHp;
    [SerializeField] public int Hp;
    [SerializeField] public string color;
    Vector3 location;
    BaseHealthBar bhb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bhb = GetComponentInChildren<BaseHealthBar>();
        Debug.Log("Start");
        InvokeRepeating("SpawnBillion",1,2);
        Debug.Log("End");
        location = this.transform.position;
    }

    void SpawnBillion()
    {
        Vector3 random = new Vector3(Random.Range(-0.5f, 0.5f), (Random.Range(-0.5f, 0.5f)),0 );
        Debug.Log("Spawning.");
        Instantiate(BillionType, location + random, Quaternion.identity);
    }

    public void takeDamage(int damage)
    {
        Hp -= damage;
        bhb.setHealth();
        if(Hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
