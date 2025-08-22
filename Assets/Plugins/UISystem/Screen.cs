using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

 namespace UISystem {
    
    public class Screen : BaseUI {
        private Module currentModule;

        
        public GameObject meshObject;

        public virtual void Back () 
        {
        }
        public void ChangeModule(Module targetModule)
        {
            if(currentModule!=null)
                currentModule.Hide();
            currentModule=targetModule;
            currentModule.Show();
        }
        public void HideModule(params Module[] modules)
        {
            for(int indexOfModule=0;indexOfModule<modules.Length;indexOfModule++)
            {
                modules[indexOfModule].Disable();
            }
        }
    }
}