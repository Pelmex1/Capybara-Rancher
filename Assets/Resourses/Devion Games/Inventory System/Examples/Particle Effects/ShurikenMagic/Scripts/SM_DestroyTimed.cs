using UnityEngine;

public class SM_DestroyTimed : MonoBehaviour {
	public float destroyTime=5f;
	
	private void Start () {
		Destroy (gameObject, destroyTime);
	}
}
