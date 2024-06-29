using System.Collections;
using UnityEngine;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;
using CapybaraRancher.EventBus;

public class CrystalsController : MonoBehaviour
{
    private const string MOVEBLE_OBJECT_TAG = "movebleObject";
    private const float Y_BOOST_FOR_CRYSTAL = 0.5f;
    private const float SIZE_BOOST_AFTER_TRANSFORMATION = 1.5f;

    [SerializeField] private float _delayBeforeCrystalSpawn = 10f;
    [SerializeField] private float _delayBeforeStarving = 60f;
    [SerializeField] private int _startCrystalPool = 20;
    [SerializeField] private GameObject _wellfedParticle;
    [SerializeField] private GameObject _hungryParticle;
    [SerializeField] private GameObject _angryParticle;

    private Vector3 _scaleforeat = new Vector3(1f, 1f, 1f);
    private bool _isAngry = false;
    private FoodType _whatEat1;
    private FoodType _whatEat2;
    private string _favouriteFoodName1;
    private string _favouriteFoodName2;
    private bool _hasTransformed = false;
    private ICapybaraAudioController _audioController;
    private IMobsAi _mobsAi;
    private ICapybaraItem _capybaraData;
    private TypeGameObject _crystal1;
    private TypeGameObject _crystal2;

    public bool IsHungry { get; set; } = false;
    public GameObject NewCrystal { get; set; }
    private void Start()
    {
        _audioController = GetComponent<ICapybaraAudioController>();
        _mobsAi = GetComponent<IMobsAi>();
        _capybaraData = GetComponent<ICapybaraItem>();

        _whatEat1 = _capybaraData.WhatEat;
        _favouriteFoodName1 = _capybaraData.NameOfFavouriteFood;
        _crystal1 = _capybaraData.CrystalPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;

        InstantiateCrystals();
        StartCoroutine(LoopToStarving());
    }
    private void Update()
    {
        UpdateStats();
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject eatObj = collision.gameObject;
        if (eatObj.CompareTag(MOVEBLE_OBJECT_TAG))
        {
            IMovebleObject localMovebleObject = eatObj.GetComponent<IMovebleObject>();
            ICrystalItem dataCr;
            if (eatObj.GetComponent<IFoodItem>() != null && eatObj.transform.localScale == _scaleforeat || eatObj.transform.localScale == 0.5f*_scaleforeat && IsHungry)
            {
                string nameOfFood = localMovebleObject.Data.name;
                FoodType typeOfFood = eatObj.GetComponent<IFoodItem>().Type;
                if (_favouriteFoodName1 == nameOfFood || _favouriteFoodName2 == nameOfFood)
                {
                    StartCoroutine(GenerateCrystals(true));
                    eatObj.SetActive(false);
                    EventBus.AddInPool(gameObject, localMovebleObject.Data.TypeGameObject);
                }
                else if ((_whatEat1 == FoodType.All || _whatEat1 == typeOfFood) ||
                    (_whatEat2 == FoodType.All || _whatEat2 == typeOfFood))
                {
                    StartCoroutine(GenerateCrystals(false));
                    eatObj.SetActive(false);
                    EventBus.AddInPool(gameObject, localMovebleObject.Data.TypeGameObject);
                }
            }
            else if (eatObj.TryGetComponent(out dataCr))
            {
                InventoryItem dataIn = localMovebleObject.Data;
                if (dataCr.Price != 0 && (_capybaraData.CrystalPrefab != dataIn.Prefab &&
                    NewCrystal != dataIn.Prefab) && !_hasTransformed)
                {
                    TransformationToAnotherCapybara(dataIn.Prefab, dataCr.NextCapibara,
                        dataCr.FavouriteFoodName, dataCr.WhatEatThisType);
                    localMovebleObject.Localgameobject.SetActive(false);
                    eatObj.SetActive(false);
                    EventBus.AddInPool(gameObject, localMovebleObject.Data.TypeGameObject);
                    EventBus.TransformationTutorial.Invoke();
                }
            }
        }
    }
    public IEnumerator GenerateCrystals(bool isFavouriteFood)
    {
        EventBus.FeedTutorial?.Invoke();
        IsHungry = false;
        _isAngry = false;
        _mobsAi.SetFoodFound(false);

        _audioController.SetHappyStatus();
        _audioController.Eating();

        yield return new WaitForSecondsRealtime(_delayBeforeCrystalSpawn);
        int crystalCount = isFavouriteFood ? 2 : 1;
        for (int i = 0; i < crystalCount; i++)
        {
            GameObject crystal1 = EventBus.RemoveFromThePool(_crystal1);
            crystal1.SetActive(true);
            crystal1.transform.rotation = Quaternion.identity;
            yield return new WaitForSecondsRealtime(0.1f);
            crystal1.transform.position = RandomVector3();

            if (_hasTransformed)
            {
                GameObject crystal2 = EventBus.RemoveFromThePool(_crystal2);
                crystal2.SetActive(true);
                crystal2.transform.rotation = Quaternion.identity;
                yield return new WaitForSecondsRealtime(0.1f);
                crystal2.transform.position = RandomVector3();
            }
        }
        StartCoroutine(LoopToStarving());
    }
    private Vector3 RandomVector3()
    {
        float radius = 1f;
        float posx = Random.Range(transform.position.x + radius, transform.position.x - radius);
        float posZ = Random.Range(transform.position.z + radius, transform.position.z - radius);
        Vector3 pos = new(posx, transform.position.y + Y_BOOST_FOR_CRYSTAL, posZ);
        return pos;
    }
    private IEnumerator LoopToStarving()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeStarving);
        IsHungry = true;
        StartCoroutine(LoopToAnger());
    }
    private IEnumerator LoopToAnger()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeStarving);
        if (IsHungry)
        {
            _isAngry = true;
            _audioController.SetAngryStatus();
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
            if (IsHungry)
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
        transform.localScale *= SIZE_BOOST_AFTER_TRANSFORMATION;
        NewCrystal = _newCrystal;
        _crystal2 = _capybaraData.CrystalPrefab.GetComponent<IMovebleObject>().Data.TypeGameObject;
        _favouriteFoodName2 = nameOfSecondFavouriteFood;
        _whatEat2 = whatEatSecond;
        Instantiate(modification, transform);
        tag = "Untagged";
        _hasTransformed = true;
        GetComponent<MovebleObject>().enabled = false;
    }
    private void InstantiateCrystals()
    {
        for (int i = 0; i < _startCrystalPool; i++)
        {
            GameObject spawnedObject = Instantiate(_capybaraData.CrystalPrefab);
            spawnedObject.SetActive(true);
            spawnedObject.SetActive(false);
        }
    }
}
