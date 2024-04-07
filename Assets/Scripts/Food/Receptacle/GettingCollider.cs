using CustomEventBus;
using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        IFoodItem food;
        if (collision.gameObject.TryGetComponent(out food))
            if (food.IsGenerable)
            {
                int spawnIndex = food.IndexForSpawnFarm;
                EventBus.WasChangeFarm.Invoke(spawnIndex);
                Destroy(collision.gameObject);
                GetComponent<GettingCollider>().enabled = false;
            }
    }
}
