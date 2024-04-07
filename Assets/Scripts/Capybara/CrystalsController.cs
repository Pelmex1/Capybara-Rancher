using System.Collections;
using UnityEngine;

public class CrystalsController : MonoBehaviour
{
    private const string MOVEBLEOBJECTTAG = "movebleObject";
    private const float YBOOSTFORCRYSTAL = 0.5f;
    private const float SIZEBOOSTAFTERTRANSFORMATION = 1.5f;

    [SerializeField] private float _delayBeforeCrystalSpawn = 10f;
    [SerializeField] private float _delayBeforeStarving = 60f;
    [SerializeField] private GameObject _wellfedParticle;
    [SerializeField] private GameObject _hungryParticle;
    [SerializeField] private GameObject _angryParticle;

    private CapybaraItem _capybaraData;
    private bool _isAngry = false;
    private MovebleObject _movebleObject;
    private FoodType _whatEat1;
    private FoodType _whatEat2;
    private string _nameOfFavouriteFood1;
    private string _nameOfFavouriteFood2;
    private CapybaraAudioController _audioController;
    private MobsAi _mobsAi;
    private bool _hasTransformed { get; set; } = false;

    public bool isHungry {get; set;} = false;
    public GameObject NewCrystal { get; set; }
    private void Start()
    {
        _audioController = GetComponent<CapybaraAudioController>();
        _mobsAi = GetComponent<MobsAi>();
        _capybaraData = GetComponent<CapybaraItem>();

        _whatEat1 = _capybaraData.WhatEat;
        _nameOfFavouriteFood1 = _capybaraData.NameOfFavouriteFood;

        StartCoroutine(LoopToStarving());
    }
    private void Update()
    {
        UpdateStats();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(MOVEBLEOBJECTTAG))
        {
            if (collision.gameObject.GetComponent<FoodItem>() != null && collision.gameObject.GetComponent<Rigidbody>().isKinematic == false && isHungry)
            {
                string nameOfFood = collision.gameObject.GetComponent<MovebleObject>().data.name;
                FoodType typeOfFood = collision.gameObject.GetComponent<FoodItem>().Type;
                if (_nameOfFavouriteFood1 == nameOfFood || _nameOfFavouriteFood2 == nameOfFood)
                {
                    StartCoroutine(GenerateCrystals(true));
                    Destroy(collision.gameObject);
                }
                else if ((_whatEat1 == FoodType.All || _whatEat1 == typeOfFood) || (_whatEat2 == FoodType.All || _whatEat2 == typeOfFood))
                {
                    StartCoroutine(GenerateCrystals(false));
                    Destroy(collision.gameObject);
                }
            }
            else if (collision.gameObject.GetComponent<CrystalItem>() != null)
            {
                CrystalItem dataCr = collision.gameObject.GetComponent<CrystalItem>();
                InventoryItem dataIn = collision.gameObject.GetComponent<MovebleObject>().data;
                if (dataCr.Price != 0 && (_capybaraData.CrystalPrefab != dataIn.prefab && NewCrystal != dataIn.prefab) && !_hasTransformed)
                {
                    TransformationToAnotherCapybara(dataIn.prefab, dataCr.NextCapibara, dataCr.NameOfFavouriteFoodThisType, dataCr.WhatEatThisType);
                    Destroy(collision.gameObject);
                }
            }
        }
    }
    public IEnumerator GenerateCrystals(bool isFavouriteFood)
    {
        isHungry = false;
        _isAngry = false;
        _mobsAi.SetFoodFound(false);

        _audioController.SetHappyStatus();
        _audioController.Eating();

        yield return new WaitForSecondsRealtime(_delayBeforeCrystalSpawn);

        int crystalCount = isFavouriteFood ? 2 : 1;
        for (int i = 0; i < crystalCount; i++)
        {
            Instantiate(_capybaraData.CrystalPrefab, RandomVector3(), Quaternion.identity);

            if (_hasTransformed)
            {
                Instantiate(NewCrystal, RandomVector3(), Quaternion.identity);
            }
        }
        StartCoroutine(LoopToStarving());
    }
    private Vector3 RandomVector3()
    {
        float radius = 1f;
        float posx = Random.Range(transform.position.x + radius, transform.position.x - radius);
        float posZ = Random.Range(transform.position.z + radius, transform.position.z - radius);
        Vector3 pos = new (posx, transform.position.y + YBOOSTFORCRYSTAL, posZ);
        return pos;
    }
    private IEnumerator LoopToStarving()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeStarving);
        isHungry = true;
        StartCoroutine(LoopToAnger());
    }
    private IEnumerator LoopToAnger()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeStarving);
        if (isHungry)
        {
            _isAngry = true;
            _audioController.SetAngryStatus(); // i dont know why it errors
        }
    }
    private void UpdateStats()
    {
        if (_isAngry)
        {
            _angryParticle.SetActive(true);
            _wellfedParticle.SetActive(false);
            _hungryParticle.SetActive(false);
        }
        else
        {
            _angryParticle.SetActive(false);
            if (isHungry)
            {
                _hungryParticle.SetActive(true);
                _wellfedParticle.SetActive(false);
            }
            else
            {
                _hungryParticle.SetActive(false);
                _wellfedParticle.SetActive(true);
            }
        }
    }
    private void TransformationToAnotherCapybara(GameObject _newCrystal, GameObject modification, string nameOfSecondFavouriteFood, FoodType whatEatSecond)
    {
        transform.localScale *= SIZEBOOSTAFTERTRANSFORMATION;
        NewCrystal = _newCrystal;
        _nameOfFavouriteFood2 = nameOfSecondFavouriteFood;
        _whatEat2 = whatEatSecond;
        Instantiate(modification, transform);
        _movebleObject.enabled = false;
        tag = "Untagged";
        _hasTransformed = true;
    }
}
