using UnityEngine;

public class CandyDestroyer : MonoBehaviour
{
    public CandyManager candyManager;
    public int reward;
    public GameObject effectPrefab;
    public Vector3 effectRotation;
    void OnTriggerEnter(Collider other)
    {    //CompareTag のほうがいい
        if (other.gameObject.tag == "Candy")
        {
            candyManager.AddCandy(reward);
            Destroy(other.gameObject);

            if (effectPrefab != null)
            {
                Instantiate(
                    effectPrefab,
                    other.transform.position,
                    Quaternion.Euler(effectRotation)
                );
            }
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
