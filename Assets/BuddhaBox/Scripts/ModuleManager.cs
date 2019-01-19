using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{

    public ModuleBase[] modules;

    // Start is called before the first frame update
    void Awake()
    {
        modules = GetComponentsInChildren<ModuleBase>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var module in modules)
        {
            if (module.IsActive())
            {
                module.DoUpdate();
            }
        }
    }

    public T Get<T>()
    {
        foreach (var m in modules)
        {
            if (m is T)
            {
                return (T)System.Convert.ChangeType(m, typeof(T));
            }
        }
        Debug.Log("Could not find " + typeof(T));
        return default(T);
    }
}
