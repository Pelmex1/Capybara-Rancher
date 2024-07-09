using UnityEngine;
namespace CapybaraRancher.Save
{
    using CapybaraRancher.CustomStructures;
    using CapybaraRancher.EventBus;

    public class MobsSave : MonoBehaviour
    {
        private const string KEY_TO_SAVE = "Pen";

        private bool _isOnApplicationQuit = false;
        private bool _toSave = false;
        private CapybaraItem capybaraItem;
        private InventoryItem inventoryItem;
        private void OnEnable()
        {
            try
            {
                EventBus.RemoveFromDisable($"{transform.parent?.name}_{name}");
            }
            catch { }
            EventBus.GlobalSave += Save;
            capybaraItem = GetComponent<CapybaraItem>();
            inventoryItem = GetComponent<CapybaraMovebleObject>().Data;
        }
        private void OnDisable()
        {
            if (!_isOnApplicationQuit)
                EventBus.AddInDisable($"{transform.parent?.name}_{name}");
            EventBus.GlobalSave -= Save;
            try
            {
                SavedMobsManager.Instance?.RemoveMobToSave(new SaveCapybara(
                gameObject.name,
                gameObject.transform.position,
                capybaraItem.Data1,
                capybaraItem.Data2,
                capybaraItem,
                inventoryItem));
            }
            catch { }
        }
        private void OnApplicationQuit()
        {
            Save();
            EventBus.GlobalSave -= Save;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(KEY_TO_SAVE) && !_toSave)
            {
                _toSave = true;
                try
                {
                    SavedMobsManager.Instance.AddMobToSave(new SaveCapybara(
                    gameObject.name,
                    gameObject.transform.position,
                    capybaraItem.Data1,
                    capybaraItem.Data2,
                    capybaraItem,
                    inventoryItem));
                }
                catch { }
        }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.CompareTag(KEY_TO_SAVE) && _toSave)
            {
                _toSave = false;
                try
                {
                    SavedMobsManager.Instance.RemoveMobToSave(new SaveCapybara(
                    gameObject.name,
                    gameObject.transform.position,
                    capybaraItem.Data1,
                    capybaraItem.Data2,
                    capybaraItem,
                    inventoryItem));
                }
                catch { }
            }
        }
        private void Save()
        {
            if (_toSave)
            {
                _isOnApplicationQuit = true;
            }
        }
        public void Transformation()
        {
            try
            {
                SavedMobsManager.Instance.RemoveMobToSave(new SaveCapybara(
                    gameObject.name,
                    gameObject.transform.position,
                    capybaraItem.Data1,
                    capybaraItem.Data2,
                    capybaraItem,
                    inventoryItem));
                SavedMobsManager.Instance.AddMobToSave(new SaveCapybara(
                        gameObject.name,
                        gameObject.transform.position,
                        capybaraItem.Data1,
                        capybaraItem.Data2,
                        capybaraItem,
                        inventoryItem));
            }
            catch { }
        }
    }
}