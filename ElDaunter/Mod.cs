using ElDaunter.textutils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ElDaunter
{
    public class Mod : Modding.Mod
    {
        private int myiint = 0;
        public override void Initialize()
        {
            _modName = "El Daunter";
            _modVersion = "Early alpha";
            Log("Does logging work?");
            GameObject go = new GameObject("textdumperultraextreme");
            go.AddComponent<textdumper>();
            Object.DontDestroyOnLoad(go);
            go.AddComponent<dumpscene>();

            //On.DialogBuilder.ctor += DialogBuilderOnctor;
            
            // Put this component on a gameobject that is never destroyed.
            
            /*
            GameObject go = new GameObject("jqwiuneiouasdf");
            
            go.AddComponent<DebugDraw>();
            go.AddComponent<dumpscene>();
            Object.DontDestroyOnLoad(go);
            
            */

            //SceneManager.LoadSceneAsync("Mansion Lower Left Hallway");
        }
    }
}