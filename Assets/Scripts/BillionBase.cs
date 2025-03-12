using UnityEngine;

public class BillionBase : MonoBehaviour
{
    [SerializeField] GameObject BillionType;
    Vector3 location;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Debug.Log("Start");
        InvokeRepeating("SpawnBillion",1,2);
        Debug.Log("End");
        location = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBillion()
    {
        Vector3 random = new Vector3(Random.Range(-0.5f, 0.5f), (Random.Range(-0.5f, 0.5f)),0 );
        Debug.Log("Spawning.");
        Instantiate(BillionType, location + random, Quaternion.identity);
    }
}
