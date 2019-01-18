using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleBase : MonoBehaviour
{
  
    public virtual bool IsActive()
    {
        return true;
    }
    
    public virtual void DoUpdate()
    {
        
    }
}
