using UnityEngine;

public class RotateAndRank : MonoBehaviour
{
    [SerializeField] GameObject RankShow;

    public int level;
    void Start()
    {
        level = GetComponentInParent<BillionBehavior>().level;
        SpawnRank();
    }
    void Update()
    {
        RotateRank();
    }
    void SpawnRank()
    {
        if (level > 1)
        {
            for (int i = 1; i < level; i++)
            {
                GameObject go = Instantiate(RankShow, transform, false);
                go.transform.Rotate(0, 0, 60 * i);
            }
        }
    }

    void RotateRank()
    {
        transform.Rotate(0, 0, 100 * Time.deltaTime);
    }
}
