using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    [SerializeField] private float delayBeforeCrystalSpawn = 10f;
    [SerializeField] private float delayBeforeStarving = 60f;
    private CapybaraItem capybaraData;
    private MobsAi mobsAi;
    public bool isStarve {get; set;} = true;
    public bool hasTransformed {get; set;} = false;
    public GameObject newCrystal {get; set;}
    void Start()
    {
        capybaraData = GetComponent<CapybaraItem>();
        mobsAi = GetComponent<MobsAi>();
    }
    public IEnumerator GenerateCrystals(bool isFavouriteFood){
        isStarve = false;
        mobsAi.isfoodfound = false;
        yield return new WaitForSecondsRealtime(delayBeforeCrystalSpawn);
        int crystalCount = isFavouriteFood ? 2 : 1;
        for (int i = 0; i < crystalCount; i++)
        {
            GameObject crystal1 = Instantiate(capybaraData.crystalPrefab, RandomVector3(), Quaternion.identity);

            if (hasTransformed)
            {
                GameObject crystal2 = Instantiate(newCrystal, RandomVector3(), Quaternion.identity);
            }
        }

    }
    private Vector3 RandomVector3(){
        float radius = 1f;
        float posx = Random.Range(transform.position.x + radius, transform.position.x - radius);
        float posZ = Random.Range(transform.position.z + radius, transform.position.z - radius);
        Vector3 pos = new (posx, transform.position.y + 0.5f, posZ);
        return pos;
    }
    private IEnumerator LoopToStarving(){
        yield return new WaitForSecondsRealtime(delayBeforeStarving);
        isStarve = true;
    }
}
