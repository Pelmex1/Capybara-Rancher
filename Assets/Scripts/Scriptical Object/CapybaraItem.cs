using UnityEngine;
using CapybaraRancher.Interfaces;
using CapybaraRancher.Consts;

public class CapybaraItem : MonoBehaviour, ICapybaraItem
{


    private CrystalsController crController;

    [SerializeField] private CapybaraData _data1;
    [SerializeField] private CapybaraData _data2;

    private void Start()
    {
        crController = GetComponent<CrystalsController>();
        if (_data1 != null)
            Instantiate(_data1.Mod, transform);
        if (_data2 != null)
            Transformation();
    }
    public void Transformation()
    {
        Instantiate(_data2.Mod, transform);
        tag = "Untagged";
        crController.HasTransformed = true;
        transform.localScale *= Constants.SIZE_BOOST_AFTER_TRANSFORMATION;
        GetComponent<MovebleObject>().enabled = false;
    }

    public CapybaraData Data1
    {
        get { return _data1; }
        set { _data1 = value; }
    }

    public CapybaraData Data2
    {
        get { return _data2; }
        set { _data2 = value; }
    }
}
