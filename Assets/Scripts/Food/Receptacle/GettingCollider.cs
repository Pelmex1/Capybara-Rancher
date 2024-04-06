using CustomEventBus;
using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        FoodItem food;
        collision.gameObject.TryGetComponent(out food);
        if (food)
            if (food.isGenerable)
            {
                int spawnIndex = food.indexForSpawnFarm;
                EventBus.WasChangeFarm.Invoke(spawnIndex);
                Destroy(collision.gameObject);
                GetComponent<GettingCollider>().enabled = false;
            }
    }

}
