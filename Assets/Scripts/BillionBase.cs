using UnityEngine;

public class BillionBase : MonoBehaviour
{
    [SerializeField] GameObject BillionType;
    [SerializeField] public int maxHp;
    [SerializeField] public int Hp;
    [SerializeField] public int maxExp;
    [SerializeField] public int exp;
    [SerializeField] public int level;
    [SerializeField] public string color;

    Vector3 location;
    BaseHealthBar bhb;
    BaseExpBar beb;

    void Start()
    {
        bhb = GetComponentInChildren<BaseHealthBar>();
        beb = GetComponentInChildren<BaseExpBar>();
        Debug.Log("Start");
        InvokeRepeating("SpawnBillion",1,2);
        Debug.Log("End");
        location = this.transform.position;
        exp = 0;
        maxExp = 8;
        level = 1;
    }

    void SpawnBillion()
    {
        Vector3 random = new Vector3(Random.Range(-0.5f, 0.5f), (Random.Range(-0.5f, 0.5f)),0 );
        Debug.Log("Spawning.");
        GameObject billion = Instantiate(BillionType, location + random, Quaternion.identity);
        billion.GetComponent<BillionBehavior>().level = level;
    }

    public void takeDamage(int damage)
    {
        Hp -= damage;
        bhb.setHealth();
        if(Hp <= 0)
        {
            Destroy(gameObject);
            Destroy(beb.expText);
        }
    }

    public void expUp()
    {
        exp++;
        if (exp >= maxExp)
        {
            level++;
            maxExp *= 2;
            exp = 0;
        }
        beb.setExp(exp);
    }
}
