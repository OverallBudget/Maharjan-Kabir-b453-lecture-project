using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        float moveInput2 = Input.GetAxis("Vertical");
        transform.Translate(moveInput * Vector2.right * 10 * Time.deltaTime);
        transform.Translate(moveInput2 * Vector2.up * 10 * Time.deltaTime);
    }

}
