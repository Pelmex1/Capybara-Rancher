using System.Collections;
using UnityEngine;
using CapybaraRancher.Enums;
using CapybaraRancher.Interfaces;
using CapybaraRancher.EventBus;
using CapybaraRancher.Save;

public class CrystalsController : MonoBehaviour
{
    private const string MOVEBLE_OBJECT_TAG = "movebleObject";
    private const float Y_BOOST_FOR_CRYSTAL = 0.5f;

    [SerializeField] private float _delayBeforeCrystalSpawn = 10f;
    [SerializeField] private float _delayBeforeStarving = 60f;
    [SerializeField] private GameObject _wellfedParticle;
    [SerializeField] private GameObject _hungryParticle;
    [SerializeField] private GameObject _angryParticle;

    private int _startCrystalPool = 50;
    private Vector3 _scaleforeat = new Vector3(1f, 1f, 1f);
    private bool _isAngry = false;
    private ICapybaraAudioController _audioController;
    private IMobsAi _mobsAi;
    private ICapybaraItem _capybaraItem;
    private MobsSave _mobsSave;
    private bool _isHungry = false;
    private GameObject _newCrystal;

    public bool HasTransformed = false;

    private void Start()
    {
        _audioController = GetComponent<ICapybaraAudioController>();
        _mobsAi = GetComponent<IMobsAi>();
        _capybaraItem = GetComponent<ICapybaraItem>();
        _mobsSave = GetComponent<MobsSave>();

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
            if (eatObj.GetComponent<IFoodItem>() != null && eatObj.transform.localScale == _scaleforeat || eatObj.transform.localScale == 0.5f*_scaleforeat && _isHungry)
            {
                string nameOfFood = localMovebleObject.Data.name;
                FoodType typeOfFood = eatObj.GetComponent<IFoodItem>().Type;
                if (_capybaraItem.Data1.FavouriteFood == nameOfFood || _capybaraItem.Data2?.FavouriteFood == nameOfFood)
                {
                    StartCoroutine(GenerateCrystals(true));
                    eatObj.SetActive(false);
                    EventBus.AddInPool(gameObject, localMovebleObject.Data.TypeGameObject);
                }
                else if ((_capybaraItem.Data1.WhatEat == FoodType.All || _capybaraItem.Data1.WhatEat == typeOfFood) ||
                    (_capybaraItem.Data2?.WhatEat == FoodType.All || _capybaraItem.Data2?.WhatEat == typeOfFood))
                {
                    StartCoroutine(GenerateCrystals(false));
                    eatObj.SetActive(false);
                    EventBus.AddInPool(gameObject, localMovebleObject.Data.TypeGameObject);
                }
            }
            else if (eatObj.TryGetComponent(out dataCr))
            {
                InventoryItem dataIn = localMovebleObject.Data;
                if (dataCr.Price != 0 && (_capybaraItem.Data1.CrystalPrefab != dataIn.Prefab &&
                    _newCrystal != dataIn.Prefab) && !HasTransformed)
                {
                    TransformationToAnotherCapybara(dataCr.TypeData);
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
        _isHungry = false;
        _isAngry = false;
        _mobsAi.SetFoodFound(false);

        _audioController.SetHappyStatus();
        _audioController.Eating();

        yield return new WaitForSecondsRealtime(_delayBeforeCrystalSpawn);
        int crystalCount = isFavouriteFood ? 2 : 1;
        for (int i = 0; i < crystalCount; i++)
        {
            GameObject crystal1 = EventBus.RemoveFromThePool(_capybaraItem.Data1.CrystalType);
            crystal1.SetActive(true);
            crystal1.transform.rotation = Quaternion.identity;
            yield return new WaitForSecondsRealtime(0.1f);
            crystal1.transform.position = RandomVector3();

            if (HasTransformed)
            {
                GameObject crystal2 = EventBus.RemoveFromThePool(_capybaraItem.Data2.CrystalType);
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
        _isHungry = true;
        StartCoroutine(LoopToAnger());
    }
    private IEnumerator LoopToAnger()
    {
        yield return new WaitForSecondsRealtime(_delayBeforeStarving);
        if (_isHungry)
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
            if (_isHungry)
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
    private void TransformationToAnotherCapybara(CapybaraData newData)
    {
        _capybaraItem.Data2 = newData;
        _capybaraItem.Transformation();
        _mobsSave.Transformation();
    }
    private void InstantiateCrystals()
    {
        for (int i = 0; i < _startCrystalPool; i++)
        {
            GameObject spawnedObject = Instantiate(_capybaraItem.Data1.CrystalPrefab);
            spawnedObject.SetActive(true);
            spawnedObject.SetActive(false);
        }
    }
}