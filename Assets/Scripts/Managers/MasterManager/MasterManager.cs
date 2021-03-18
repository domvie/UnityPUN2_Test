using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObject<MasterManager>
{

    [SerializeField] private GameSettings _gameSettings;

    [SerializeField] private List<NetworkedPrefab> _networkedPrefabs = new List<NetworkedPrefab>();
    public static GameSettings GameSettings { get { return Instance._gameSettings; } }

    public static GameObject NetworkInstantiate(GameObject gameObject, Vector3 position, Quaternion rotation)
    {
        Debug.LogError("GO: " + gameObject.name);
        foreach (NetworkedPrefab networkedPrefab in Instance._networkedPrefabs)
        {
            Debug.LogError("networkedPrefab: " + networkedPrefab.Path + ", " + networkedPrefab.Prefab.name);
            if (networkedPrefab.Prefab == gameObject)
            {
                if (networkedPrefab.Path != string.Empty)
                {
                    Debug.LogError("instantiating on photon: " + networkedPrefab.Path);
                    return PhotonNetwork.Instantiate(networkedPrefab.Path, position, rotation);
                }
                else
                {
                    Debug.LogError("Path is empty for gameObject name " + networkedPrefab.Prefab);
                    return null;
                }
            }
        }
        return null;
    }

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void PopulateNetworkedPrefabs()
    {
#if UNITY_EDITOR
        Instance._networkedPrefabs.Clear();

        GameObject[] results = Resources.LoadAll<GameObject>("");
        for (int i = 0; i < results.Length; i++)
        {
            if (results[i].GetComponent<PhotonView>() != null)
            {
                string path = AssetDatabase.GetAssetPath(results[i]);
                Instance._networkedPrefabs.Add(new NetworkedPrefab(results[i], path));
            }
        }
#endif
    }

}
