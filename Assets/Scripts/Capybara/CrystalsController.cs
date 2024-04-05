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
    private CapybaraAudioController capybaraAudioController;
    private MovebleObject movebleObject;
    private FoodType whatEat1;
    private FoodType whatEat2;
    private string nameOfFavouriteFood1;
    private string nameOfFavouriteFood2;
    private bool hasTransformed { get; set; } = false;

    public bool isHungry {get; set;} = false;
    public GameObject newCrystal { get; set; }
    private void Start()
    {
        capybaraData = GetComponent<CapybaraItem>();
        mobsAi = GetComponent<MobsAi>();
        capybaraAudioController = GetComponent<CapybaraAudioController>();

        whatEat1 = capybaraData.whatEat;
        nameOfFavouriteFood1 = capybaraData.nameOfFavouriteFood;

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

        capybaraAudioController.SetHappyStatus();
        capybaraAudioController.Eating();

        yield return new WaitForSecondsRealtime(delayBeforeCrystalSpawn);

        int crystalCount = isFavouriteFood ? 2 : 1;
        for (int i = 0; i < crystalCount; i++)
        {
            Instantiate(capybaraData.crystalPrefab, RandomVector3(), Quaternion.identity);

            if (hasTransformed)
            {
                Instantiate(newCrystal, RandomVector3(), Quaternion.identity);
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
        {
            isAngry = true;
            capybaraAudioController.SetAngryStatus();
        }
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("movebleObject"))
        {
            if (collision.gameObject.GetComponent<FoodItem>() != null && collision.gameObject.GetComponent<Rigidbody>().isKinematic == false && isHungry)
            {
                string nameOfFood = collision.gameObject.GetComponent<MovebleObject>().data.name;
                FoodType typeOfFood = collision.gameObject.GetComponent<FoodItem>().type;
                if (nameOfFavouriteFood1 == nameOfFood || nameOfFavouriteFood2 == nameOfFood)
                {
                    StartCoroutine(GenerateCrystals(true));
                    Destroy(collision.gameObject);
                }
                else if ((whatEat1 == FoodType.All || whatEat1 == typeOfFood) || (whatEat2 == FoodType.All || whatEat2 == typeOfFood))
                {
                    StartCoroutine(GenerateCrystals(false));
                    Destroy(collision.gameObject);
                }
            }
            else if (collision.gameObject.GetComponent<CrystalItem>() != null)
            {
                CrystalItem dataCr = collision.gameObject.GetComponent<CrystalItem>();
                InventoryItem dataIn = collision.gameObject.GetComponent<MovebleObject>().data;
                if (dataCr.price != 0 && (capybaraData.crystalPrefab != dataIn.prefab && newCrystal != dataIn.prefab) && !hasTransformed)
                {
                    TransformationToAnotherCapybara(dataIn.prefab, dataCr.nextCapibara, dataCr.nameOfFavouriteFoodThisType, dataCr.whatEatThisType);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject _newCrystal, GameObject modification, string nameOfSecondFavouriteFood, FoodType whatEatSecond)
    {
        transform.localScale *= 1.5f;
        newCrystal = _newCrystal;
        nameOfFavouriteFood2 = nameOfSecondFavouriteFood;
        whatEat2 = whatEatSecond;
        Instantiate(modification, transform);
        movebleObject.enabled = false;
        tag = "Untagged";
        hasTransformed = true;
    }
}
