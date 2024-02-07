using UnityEngine;

public class GettingCollider : MonoBehaviour
{
    [SerializeField] private Receptacle receptacleScript;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ResourseData>().type == ResourseType.food)
        {
            int spawnIndex = collision.gameObject.GetComponent<ResourseData>().indexForSpawnFarm;
            receptacleScript.ChangeFarm(spawnIndex);

            GetComponent<GettingCollider>().enabled = false; // Одноразовая функция
        }
    }
}
