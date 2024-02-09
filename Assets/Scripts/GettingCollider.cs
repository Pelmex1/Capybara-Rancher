using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    [SerializeField] private Receptacle receptacleScript;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MovebleObject>().data.isGenerable)
        {
            int spawnIndex = collision.gameObject.GetComponent<MovebleObject>().data.indexForSpawnFarm;
            receptacleScript.ChangeFarm(spawnIndex);

            GetComponent<GettingCollider>().enabled = false; // Одноразовая функция
        }
    }
}
