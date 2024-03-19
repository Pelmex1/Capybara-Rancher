using System.Collections;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    [SerializeField] private float delayBeforeCrystalSpawn = 10f;
    [SerializeField] private float delayBeforeStarving = 60f;
    [SerializeField] private GameObject wellfedParticle;
    [SerializeField] private GameObject hungryParticle;
    [SerializeField] private GameObject angryParticle;
    private CapybaraItem capybaraData;
    private MobsAi mobsAi;
    private bool isAngry = false;
    public bool isHungry {get; set;} = false;
    public bool hasTransformed {get; set;} = false;
    public GameObject newCrystal {get; set;}
    private void Start()
    {
        capybaraData = GetComponent<CapybaraItem>();
        mobsAi = GetComponent<MobsAi>();
        StartCoroutine(LoopToStarving());
    }
    private void Update()
    {
        UpdateStats();
    }
    public IEnumerator GenerateCrystals(bool isFavouriteFood)
    {
        isHungry = false;
        isAngry = false;
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
        StartCoroutine(LoopToStarving());
    }
    private Vector3 RandomVector3()
    {
        float radius = 1f;
        float posx = Random.Range(transform.position.x + radius, transform.position.x - radius);
        float posZ = Random.Range(transform.position.z + radius, transform.position.z - radius);
        Vector3 pos = new (posx, transform.position.y + 0.5f, posZ);
        return pos;
    }
    private IEnumerator LoopToStarving()
    {
        yield return new WaitForSecondsRealtime(delayBeforeStarving);
        isHungry = true;
        StartCoroutine(LoopToAnger());
    }
    private IEnumerator LoopToAnger()
    {
        yield return new WaitForSecondsRealtime(delayBeforeStarving);
        if (isHungry)
            isAngry = true;
    }
    private void UpdateStats()
    {
        if (isAngry)
        {
            angryParticle.SetActive(true);
            wellfedParticle.SetActive(false);
            hungryParticle.SetActive(false);
        }
        else
        {
            angryParticle.SetActive(false);
            if (isHungry)
            {
                hungryParticle.SetActive(true);
                wellfedParticle.SetActive(false);
            }
            else
            {
                hungryParticle.SetActive(false);
                wellfedParticle.SetActive(true);
            }
        }
    }
}
