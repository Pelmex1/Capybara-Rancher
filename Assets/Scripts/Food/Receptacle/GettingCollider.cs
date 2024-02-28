using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    [SerializeField] private Receptacle receptacleScript;
    private GettingCollider gettingCollider;

    private void Start()
    {
        gettingCollider = GetComponent<GettingCollider>();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FoodItem>().isGenerable)
        {
            int spawnIndex = collision.gameObject.GetComponent<FoodItem>().indexForSpawnFarm;
            receptacleScript.ChangeFarm(spawnIndex);
            Destroy(collision.gameObject);


            gettingCollider.enabled = false;
        }

    }

}
