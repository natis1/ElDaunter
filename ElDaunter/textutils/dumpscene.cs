using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace ElDaunter.textutils
{
    public class dumpscene : MonoBehaviour
    {
        private DebugDraw dd;
        private void Start()
        {
            _maxScene = SceneManager.sceneCountInBuildSettings;
            dd = gameObject.GetComponent<DebugDraw>();
        }

        private float _timer2 = 0f;
        private int _curScene = 0;
        private int _maxScene = 0;

        private void Update()
        {
            _timer2 -= Time.deltaTime;

            if (_timer2 < 0f && Keyboard.current.yKey.isPressed)
            {
                _timer2 = 3f;
                int i = 0;
                var gos = UnityEngine.SceneManagement.SceneManager.GetActiveScene().GetRootGameObjects();
                foreach (var go in gos)
                {
                    i++;
                    if (go.name == "Player Journal Fab")
                    {
                        continue; 
                    }
                    go.PrintSceneHierarchyTree("dump/gameobject-" + i + go.name + ".txt");
                }
            }
            
            if (_timer2 < 0f && Keyboard.current.uKey.isPressed)
            {
                _timer2 = 3f;
                
                string keys = "";
                string vals = "";
                foreach (var v in Resources.FindObjectsOfTypeAll<PlayerCardScript>())
                {
                    foreach (var m in v.ScenePossibilities)
                    {
                        keys += m.Title + "\n";
                        vals += m.Text + "\n";
                    }
                    Modding.Logger.Log("KEYS:\n\n\n"+ keys + "\nVALS:\n" + vals + "\n\n\nEND");
                }
            }
            
            if (_timer2 < 0f && Keyboard.current.nKey.isPressed)
            {
                // Only activate this effect every 4 seconds
                _timer2 = 4f;
                scanSceneDialogs(SceneManager.GetActiveScene().name);
            }
            
            if (_timer2 < 0f && Keyboard.current.jKey.isPressed)
            {
                // Only activate this effect every 4 seconds
                _timer2 = 0.3f;
                _curScene--;
                if (_curScene < 0)
                    _curScene = _maxScene - 1;
                string sceneName = System.IO.Path.GetFileNameWithoutExtension( UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex( _curScene ) );
                dd.drawString = sceneName + " - Press ; to warp";
            } else if (_timer2 < 0f && Keyboard.current.kKey.isPressed)
            {
                _timer2 = 0.3f;
                _curScene++;
                if (_curScene >= _maxScene)
                    _curScene = 0;
                
                string sceneName = System.IO.Path.GetFileNameWithoutExtension( UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex( _curScene ) );
                dd.drawString = sceneName + " - Press ; to warp";
            } else if (_timer2 < 0f && Keyboard.current.semicolonKey.isPressed)
            {
                _timer2 = 0.3f;
                StartCoroutine(switchScenes());
            }
        }

        private IEnumerator switchScenes()
        {
            string sceneName = System.IO.Path.GetFileNameWithoutExtension( UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex( _curScene ) );
            yield return SceneManager.LoadSceneAsync(sceneName);
            yield return null;
            yield return null;
            yield return null;
            yield return SceneManager.UnloadSceneAsync(sceneName);
        }
        
        private void scanSceneDialogs(string sceneName)
        {
            DialogScript[] objs = Resources.FindObjectsOfTypeAll<DialogScript>();
            JournalPerItemManagerScr[] objs2 = Resources.FindObjectsOfTypeAll<JournalPerItemManagerScr>();

            Modding.Logger.Log("JOURNAL ENTRIES BEGIN");
            foreach (var v in objs2)
            {
                Modding.Logger.Log(v.name + " IS ENTRY NAME");
                if (v.ObjectText == null)
                {
                    Modding.Logger.Log("ERROR. JOURNAL ENTRY IS NULL");
                }
                else
                {
                    Modding.Logger.Log(v.ObjectText.text);
                }

                //Modding.Logger.Log(v.TextObjt.GetComponent<Text>().text);
            }
            
            Modding.Logger.Log("JOURNAL ENTRIES END");


            foreach (var v in objs)
            {
                Modding.Logger.Log("Dialogs for Object: " + v.name + " in " + sceneName);
                String s = "";

                foreach (var k in v.Dialogs)
                {
                    s += (k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                          + "<br>" + k.Line1.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n") 
                          + "<br>" + k.Line2.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n") 
                          + "<br>" + k.Line3.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n") + "<nl>");
                }

                s += "\"";
                Modding.Logger.Log(s);
            }
        }
    }
}