using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    [SerializeField] private Receptacle receptacleScript;

    private void OnCollisionEnter(Collision collision)
    {
        FoodItem food;
        collision.gameObject.TryGetComponent(out food);
        if (food)
            if (food.isGenerable)
            {
                int spawnIndex = food.indexForSpawnFarm;
                receptacleScript.ChangeFarm(spawnIndex);
                Destroy(collision.gameObject);

                GetComponent<GettingCollider>().enabled = false;
            }
    }

}
