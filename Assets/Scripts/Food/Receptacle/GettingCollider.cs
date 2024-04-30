using CapybaraRancher.EventBus;
using CapybaraRancher.Interfaces;
using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out IFoodItem food))
            if (food.IsGenerable)
            {
                int spawnIndex = food.IndexForSpawnFarm;
                EventBus.WasChangeFarm.Invoke(spawnIndex);
                Destroy(collision.gameObject);
                enabled = false;
            }
    }
}
