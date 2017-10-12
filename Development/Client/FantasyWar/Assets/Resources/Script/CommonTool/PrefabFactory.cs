using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PrefabFactory : UnitySingleton<PrefabFactory>
{
    [SerializeField]
    private string resourcePrefabFolderRootPath = @"Prefab/";
    [SerializeField]
    private string[] resourcePrefabNames = { @"RTSGameUnitSelectionBottomCircle" };
    //
    [SerializeField]
    private string[] resourcePrefabPaths;
    private Dictionary<string, GameObject> _templates;
    private Dictionary<string, GameObject> Templates
    {
        get
        {
            if (_templates == null)
            {
                _templates = new Dictionary<string, GameObject>();
            }
            return _templates;
        }
    }

    /// <summary>
    /// 根据文本信息，生成路径信息数组
    /// </summary>
    private void resourcePrefabPathsInitialization()
    {
        resourcePrefabPaths = new string[resourcePrefabNames.Length];
        for (int i = 0; i < resourcePrefabNames.Length; i++)
        {
            resourcePrefabPaths[i] = resourcePrefabFolderRootPath + resourcePrefabNames[i];
        }
    }

    /// <summary>
    /// Creates the templates.根据路径信息数组，创建模板
    /// </summary>
    /// <returns>The templates.</returns>
    private IEnumerator createTemplates()
    {
        if (resourcePrefabNames != null)
        {
            foreach (string path in resourcePrefabPaths)
            {
                GameObject template = createTemplateCoroutine(path);
                yield return null;
            }
        }
    }
    //
    public GameObject createClone(string templateResourcePath, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (templateResourcePath == null) { return null; }
        GameObject clone = null;
        GameObject template = null;
        //
            template = createTemplateCoroutine(templateResourcePath);
        clone = createClone(template, position, rotation, parent);
        //
        return clone;
    }
    //
    public T createClone<T>(string templateResourcePath, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
    {
        if (templateResourcePath == null) { return null; }
        
        GameObject template = null;
        //
            template = createTemplateCoroutine(templateResourcePath);
        //
        return createClone<T>(template, position, rotation, parent);
    }

    public T createClone<T>(GameObject template, Vector3 position, Quaternion rotation, Transform parent = null) where T : MonoBehaviour
    {
        if (template == null)
        {
            return null;
        }
        GameObject clone = cloneInstantiate(template, position, rotation, parent);
        if (clone != null)
        {
            return clone.AddComponent<T>();
        }
        return null;
    }

    public GameObject createClone(GameObject template, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (template == null)
        {
            return null;
        }
        GameObject clone = cloneInstantiate(template, position, rotation, parent);
        return clone;
    }

    private GameObject cloneInstantiate(GameObject template, Vector3 position, Quaternion rotation, Transform parent = null)
    {
        if (template == null)
        {
            return null;
        }
        GameObject clone = GameObject.Instantiate(template, position, rotation);
        if (parent != null && clone != null)
        {
            clone.transform.parent = parent;
        }
        clone.SetActive(true);
        clone.transform.LookAt(transform.forward);
        return clone;
    }

    /// <summary>
    /// Creates the template sync.同步创建模板
    /// </summary>
    /// <returns>The template sync.</returns>
    /// <param name="resourcePath">Resource path.</param>
    private GameObject createTemplateCoroutine(string resourcePath)
    {
        GameObject template = null;
        //
        if (Templates.ContainsKey(resourcePath))
        {
            template = Templates[resourcePath];
            if (template != null)
            {
                return template;
            }
        }

        //
        template = (GameObject)Resources.Load(resourcePath);
        if (template != null)
        {
            GameObject.DontDestroyOnLoad(template);
            template.SetActive(false);
            lock (Templates)
            {
                Templates.Add(resourcePath, template);
            }
        }
        //
        return template;
    }

    private void Awake()
    {
        resourcePrefabPathsInitialization();
        StartCoroutine(createTemplates());
    }
}
