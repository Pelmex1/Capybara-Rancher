using CustomEventBus;

public class RoborMoveble : MovebleObject
{
    private bool CanMoving = false;
    protected override void Update()
    {
        if (CanMoving)
        {
            base.Update();
        }
    }
    private void OnEnable()
    {
        EventBus.OnMovebleObject += OnObject;
    }

    private void OnDisable()
    {
        EventBus.OnMovebleObject -= OnObject;
    }

    private void OnObject(string NameObject)
    {
        if (gameObject.name == NameObject)
        {
            CanMoving = true;
        }
    }
}
