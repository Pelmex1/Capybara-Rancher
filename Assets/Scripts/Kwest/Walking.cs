using System.Collections;
using System.Collections.Generic;
using CapybaraRancher.EventBus;
using UnityEngine;
using UnityEngine.AI;

public class Walking : MonoBehaviour
{
    [SerializeField] private List<Transform> list;
    private LinkedList<Transform> touches = new();
    private NavMeshAgent _agent;
    LinkedListNode<Transform> touch;
    private void Awake() {
        for(int i = list.Count; i > 0; i--){
            touches.AddFirst(list[i-1]);
        }
        _agent = GetComponent<NavMeshAgent>();
        EventBus.StartKvest = StartWalking;
    }
    private void StartWalking(){
        touch = touches.First;
        gameObject.SetActive(true);
        _agent.SetDestination(touch.Value.position);
    }
    private IEnumerator CkechPosition(){
        if(Vector3.Distance(transform.position, touch.Value.position) < 5)
        {
            touch = touch.Next;
            _agent.SetDestination(touch.Value.position);
        }
        yield return new WaitForSeconds(2);
        StartCoroutine(CkechPosition());
    }
}
