using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickInstantiate : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;


    private void Awake()
    {
        Vector2 offSet = Random.insideUnitCircle * 3f;
        Vector3 position = new Vector3(transform.position.x + offSet.x, transform.position.y + offSet.y, transform.position.z);
        Debug.Log("instantiating " + _prefab.name);
        MasterManager.NetworkInstantiate(_prefab, position, Quaternion.identity);
    }
}
