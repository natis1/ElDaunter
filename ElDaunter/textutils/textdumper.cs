using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace ElDaunter.textutils
{
    public class textdumper : MonoBehaviour
    {
        private string codeOutput = "";
        

        public void Start()
        {
            
        }

        private float _timer2 = 0f;

        private void Update()
        {
            _timer2 -= Time.deltaTime;

            if (_timer2 < 0f && Keyboard.current.mKey.isPressed)
            {
                // Only activate this effect every 4 seconds
                _timer2 = 4f;
                StartCoroutine(runCursedCode());
            }
        }


        public IEnumerator runCursedCode()
        {
            codeOutput += "\nThere are " + SceneManager.sceneCountInBuildSettings + " scenes\n";
            GamePlayerPrefList.Database.RunningLists.StringsList[0].Text = "<hero>";
            printInitCode(); //UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings
            
            for (int i = 6; i < UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings; ++i)
            {
                yield return null;
                string sceneName = System.IO.Path.GetFileNameWithoutExtension( UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex( i ) );
                if (sceneName == "Main Menu")
                    continue;
                printNewSceneCode(sceneName);
                yield return SceneManager.LoadSceneAsync(sceneName);
                yield return null;
                yield return null;
                yield return null;
                GamePlayerPrefList.Database.RunningLists.StringsList[0].Text = "<hero>";
                scanSceneDialogs(sceneName);
                yield return SceneManager.UnloadSceneAsync(sceneName);
            }
            codeOutput += "        }\n\n\n----------------\nEND CODE...\n";
            Modding.Logger.Log(codeOutput);
            yield return SceneManager.LoadSceneAsync("Main Menu");
        }

        private void printInitCode()
        {
            codeOutput += "BEGIN CODE:\n----------------------\n";
            codeOutput += "        public static Dictionary<string, Dictionary<string, string>> languageStrings;\n\n        public static void SetupLanguageStrings() \n        {\n            languageStrings = new Dictionary<string, Dictionary<string, string>>();\n";
            codeOutput += "            if (!languageStrings.ContainsKey(\"journals\")) \n                languageStrings.Add(\"journals\", new Dictionary<string, string>());\n";
            codeOutput += "            if (!languageStrings.ContainsKey(\"titles\")) \n                languageStrings.Add(\"titles\", new Dictionary<string, string>());\n";
            codeOutput += "            if (!languageStrings.ContainsKey(\"boxscenes\")) \n                languageStrings.Add(\"boxscenes\", new Dictionary<string, string>());\n";
            codeOutput += "            if (!languageStrings.ContainsKey(\"zzpasswords\")) \n                languageStrings.Add(\"zzpasswords\", new Dictionary<string, string>());\n";

        }

        private void printNewSceneCode(string sceneName)
        {
            //codeOutput += "            if (!languageStrings.ContainsKey(\"" + sceneName + "\")) \n                languageStrings.Add(\"" +sceneName + "\", new Dictionary<string, string>());\n";
        }

        private void scanSceneDialogs(string sceneName)
        {
            int iasd = 0;
            foreach (var v in Resources.FindObjectsOfTypeAll<PlayerDialogSwitcher>())
            {
                if (v.name == "Elizabeth Dialog Switcher")
                {
                    iasd++;
                    if (iasd > 1)
                        autodump(v, v.Dialoggers, sceneName, sceneName + "/" +  v.name + " " + iasd);
                    else
                        autodump(v, v.Dialoggers, sceneName, sceneName + "/" + v.name);
                } else
                    autodump(v, v.Dialoggers, sceneName, sceneName + "/" + v.name);
            }

            return;
            
            foreach (var v in Resources.FindObjectsOfTypeAll<CreditsSceneScr>())
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + "Credits Names" + "\"] = \"";
                for (int i = 0; i < v.OurNames.Length; i++)
                {
                    codeOutput += v.OurNames[i].text;

                    if (i < v.OurNames.Length - 1)
                    {
                        codeOutput += "<br>";
                    }
                }

                codeOutput += "\";\n";
            }
            return;
            DialogScript[] objs = Resources.FindObjectsOfTypeAll<DialogScript>();
            QuizzManagerScr[] qobjs = Resources.FindObjectsOfTypeAll<QuizzManagerScr>();
            AutoDialogScript[] adobjs = Resources.FindObjectsOfTypeAll<AutoDialogScript>();
            PlayerDialogHolder[] pdhobjs = Resources.FindObjectsOfTypeAll<PlayerDialogHolder>();
            ItormonHelperScr[] itorwtfobjs = Resources.FindObjectsOfTypeAll<ItormonHelperScr>();
            BaseDialogScrObjtBuilder[] bdsobhotgarboojbs = Resources.FindObjectsOfTypeAll<BaseDialogScrObjtBuilder>();
            SpeechTalkingScr[] speechobjs = Resources.FindObjectsOfTypeAll<SpeechTalkingScr>();
            PlayerCardScript[] addPlayerNameObjs = Resources.FindObjectsOfTypeAll<PlayerCardScript>();
            Text[] textobjs = Resources.FindObjectsOfTypeAll<Text>();
            //QuizzManagerScr

            foreach (var v in Resources.FindObjectsOfTypeAll<RandomGossipScript>())
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.GetType().Name + "-"  + sceneName + "/" +
                              v.name + "\"] = \""
                              + v.Opt1Line1 + "<br>" + v.Opt1Line2 + "<br>" + v.Opt1Line3 + "<br>" + v.Opt1Line4 +
                              "<nl>"
                              + v.Opt2Line1 + "<br>" + v.Opt2Line2 + "<br>" + v.Opt2Line3 + "<br>" + v.Opt2Line4 +
                              "<nl>"
                              + v.Opt3Line1 + "<br>" + v.Opt3Line2 + "<br>" + v.Opt3Line3 + "<br>" + v.Opt3Line4 +
                              "\";\n";
            }

            foreach (var v in Resources.FindObjectsOfTypeAll<SaveBoothDialogManager>())
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.GetType().Name + "-" + sceneName + "/" +
                              v.name + "\"] = \""
                              + v.Opt1Line1 + "<br>" + v.Opt1Line2 + "<br>" + v.Opt1Line3 + "<br>" + v.Opt1Line4 +
                              "<br>"
                              + v.Opt2Line1 + "<br>" + v.Opt2Line2 + "<br>" + v.Opt2Line3 + "<br>" + v.Opt2Line4 +
                              "<br>"
                              + v.Opt3Line1 + "<br>" + v.Opt3Line2 + "<br>" + v.Opt3Line3 + "<br>" + v.Opt3Line4 +
                              "<pg>"
                              + v.GossipLine1 + "<br>" + v.GossipLine2 + "<br>" + v.GossipLine3 + "<br>" +
                              v.GossipLine4 + "<pg>"
                              + v.FailedLine1 + "<br>" + v.FailedLine2 + "\";\n";
            }

            foreach (var v in Resources.FindObjectsOfTypeAll<EpilogueTextManagerScr>())
            {
                for (int i = 0; i < v.TextBuilder.Count; i++)
                {
                    var m = v.TextBuilder[i];
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.GetType().Name + "-" +
                                  v.name + "\"] = \"";
                    codeOutput += m.Titler;
                    for (var k = 0; k < m.Liners.Count; k++)
                    {
                        var n = m.Liners[k];
                        for (var l = 0; l < n.Liners.Count; l++)
                        {
                            var p = n.Liners[l];
                            if (l < n.Liners.Count - 1)
                                codeOutput += "<br>";
                        }

                        if (k < m.Liners.Count - 1)
                            codeOutput += "<nl>";
                    }

                    codeOutput += "\";\n";
                }
            }

            foreach (var v in Resources.FindObjectsOfTypeAll<GuiCheckSaveScr>())
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.GetType().Name + "-" +
                              v.name + "\"] = \"" + v.Text + "\";\n";
            }

            foreach (var v in Resources.FindObjectsOfTypeAll<DemoProgScr>())
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.GetType().Name + "-" +
                              v.name + "\"] = \"";
                for (var i = 0; i < v.Sliders.Count; i++)
                {
                    var k = v.Sliders[i];

                    for (var o = 0; o < k.Texters.Count; o++)
                    {
                        var n = k.Texters[o];
                        for (var p = 0; p < n.TextLines.Length; p++)
                        {
                            var m = n.TextLines[p];
                            codeOutput += m;

                            if (p < n.TextLines.Length - 1)
                            {
                                codeOutput += "<br>";
                            }
                        }

                        if (o < k.Texters.Count - 1)
                        {
                            codeOutput += "<nl>";
                        }
                    }

                    if (i < v.Sliders.Count - 1)
                    {
                        codeOutput += "<pg>";
                    }
                }

                codeOutput += "\";\n";
            }


            foreach (var v in Resources.FindObjectsOfTypeAll<SaveAlbumPerTapeManagerScr>())
            {
                var m = v.TextObjt.GetComponent<Text>();
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.GetType().Name + "-" +
                              v.name + "\"] = \"" + m.text + "\";\n";
            }



        // Hot garbage

        /*
        foreach (var v in Resources.FindObjectsOfTypeAll<AngryMobManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ArenaCageManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ArenaFirstScript>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<BachaqueroManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<BoosFirstEncounterDialog>())
        {
            autodump(v, v.DialogsArray, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<BroBlowieManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ChilliNpcScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<DrinksBlowieScript>())
        {
                autodump(v, v.Dialogs, sceneName, v.name + "-1");
                autodump(v, v.Dialogs2, sceneName, v.name + "-2");
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ElTerrybleSalseroScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<EndsHallwayIntroManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FactoryBigDoorCutsceneScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FifthTutorialManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FactoryLightsFailScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FactoryFirstDoorCutsceneManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FirstTutorialManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FloodedTreeTalkPromptScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<FourthTutorialManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<GraveyardRaceManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<HandyMirrorCutSceneManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        foreach (var v in Resources.FindObjectsOfTypeAll<HeroesDropManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<IceStoneCutsceneManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<KitchenDrinksMixScript>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<LosTerryblesBossScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<MainLabFirstSceneManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<MainLabSecondSceneManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<MementoDialogsFabScr>())
        {
            DialogPrefChooserScript[] ms = new[] {v.FirstDialog, v.SecondDialog, v.ThirdDialog, v.FourthDialog};
            autodump(v, ms.ToList(), sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<MrStitchesFightManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<NeddleRoOmIntroManager>())
        {
            autodump(v, v.LordDialogs, sceneName, v.name + "-1"); 
            autodump(v, v.AminoDialogs, sceneName, v.name + "-2");

        }

        foreach (var v in Resources.FindObjectsOfTypeAll<NightCutscenemanagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<NightFightManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<NpcConversationScript>())
        {
            autodump(v, v.DialogsArray, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<PlayerDialogHolder>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        */

        /*
        int iasd = 0;
        foreach (var v in Resources.FindObjectsOfTypeAll<PlayerDialogSwitcher>())
        {
            if (v.name == "Elizabeth Dialog Switcher")
            {
                iasd++;
                if (iasd > 1)
                    autodump(v, v.Dialoggers, sceneName, sceneName + "/" +  v.name + " " + iasd);
                else
                    autodump(v, v.Dialoggers, sceneName, sceneName + "/" + v.name);
            } else
                autodump(v, v.Dialoggers, sceneName, sceneName + "/" + v.name);
        }
        
        */

        /*
        foreach (var v in Resources.FindObjectsOfTypeAll<PuzzleFightManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<PuzzlemamPuzzle1Script>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<SalsaMinigameDialogScr>())
        {
            autodump(v, v.DialogsArray, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<SalserissimoManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<SalseroFinalSceneManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<SalseroPuzzlemanScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<SecondTutorialManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ShieldIntroManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ShieldRaceManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<ShieldSceneGangCutsceneManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ShieldSceneIntroCutsceneManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<SideChilliNpcSCr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<SixthTutorialManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<SlingSceneCutsceneManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<StitchesFinalFightManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<StitchesRoomManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<StoryIceCompManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<TBurFightManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<TheChattersBossScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<TheEndsFightManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<TheGlitchedTransitionScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<ThirdTutorialManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<TreeHubEndCutsceneManager>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }
        
        foreach (var v in Resources.FindObjectsOfTypeAll<TreeHubSceneManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<TreeSwitchesEventManagerScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<VolcanoBillboardCutsceneScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<VonGregFightManager>())
        {
            autodump(v, v.Dialogs, sceneName, v.name);
        }

        foreach (var v in Resources.FindObjectsOfTypeAll<YanijoNpcScr>())
        {
                autodump(v, v.Dialogs, sceneName, v.name);
        }

        */






        return;

        foreach (var v in Resources.FindObjectsOfTypeAll<DemoProgScr>())
            {
                string skeys = "";
                string svals = "";
                for (var index = 0; index < v.Stringers.Count; index++)
                {
                    if (index != 0)
                    {
                        skeys += ",\n";
                        svals += ",\n";
                    }

                    skeys += "\"DemoProg String " + index + "\"";
                    svals += "\"" + v.Stringers[index] + "\"";
                }
                Modding.Logger.Log("Demoprog keys and vals " + skeys + "\n\n\n" + svals);

                skeys = "";
                svals = "";
                for (var index = 0; index < v.Sliders.Count; index++)
                {
                    var m = v.Sliders[index];
                    for (int j = 0; j < m.Texters.Count; j++)
                    {
                        skeys += "DemoProg Slider " + index + " Text " + j + "\n";
                        svals += string.Join("<br>", m.Texters[j].TextLines);
                    }
                }
                Modding.Logger.Log("Demoprog 2 keys and vals " + skeys + "\n\n\n" + svals);
            }


            GeneralBossTitleGuiScr[] bostitles = Resources.FindObjectsOfTypeAll<GeneralBossTitleGuiScr>();

            BoxSceneSwitcher[] bssobjs = Resources.FindObjectsOfTypeAll<BoxSceneSwitcher>();

            foreach (var v in bssobjs)
            {
                codeOutput += "            languageStrings[\"boxscenes\"][\"" + sceneName + "\"] = \"" + v._SpecialSceneName + "\";\n";
            }

            if (sceneName == "Castle Intro Hallway")
            {
                PlayerJournalManagerScr[] objs2 = Resources.FindObjectsOfTypeAll<PlayerJournalManagerScr>();
                foreach (var go in objs2)
                {
                        var v = go;
                        for (var index = 0; index < v.Pages.Length; index++)
                        {
                            var go2 = v.Pages[index];
                            JournalPerPageManagerScr jppms = go2.GetComponent<JournalPerPageManagerScr>();
                            codeOutput += "            languageStrings[\"journals\"][\"Journal Page " + index + "\"] = \"" + jppms.transform.Find("Page Title").GetComponent<Text>().text + "\";\n";

                            Modding.Logger.Log("Page " + index + " has " + jppms.Items.Length + " items");
                            foreach (Transform child in go2.transform)
                            {
                                if (child.gameObject.name == "Page Title")
                                    continue;
                                codeOutput += "            languageStrings[\"journals\"][\"Journal Page " + index + " " + child.gameObject.name + "\"] = \"" + child.Find("Item Text").gameObject.GetComponent<Text>().text.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>").Replace("\n", "\\n") + "\";\n";

                                
                            }
                        }
                    
                }

            }

            return;
            
            foreach (var v in bostitles)
            {
                codeOutput += "            languageStrings[\"titles\"][\"" + sceneName + " main\"] = \"" + v.MainLine + "\";\n";
                codeOutput += "            languageStrings[\"titles\"][\"" + sceneName + " sub\"] = \"" + v.SubTitle + "\";\n";
            }
            
            

            foreach (var v in addPlayerNameObjs)
            {
                v.TestString = "<hero>";
            }
            
            foreach (var v in objs)
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.name + "\"] = \"";

                for (var i = 0; i < v.Dialogs.Count; i++)
                {
                    var k = v.Dialogs[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>")
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>")
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.Dialogs.Count - 1)
                        codeOutput += "<nl>";
                }

                codeOutput += "\";\n";
            }

            foreach (var v in speechobjs)
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Speech" + v.name + "\"] = \"";

                for (var i = 0; i < v.Speech.Count; i++)
                {
                    var k = v.Speech[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"")
                                      .Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line2.Replace("\"", "\\\"")
                                      .Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line3.Replace("\"", "\\\"")
                                      .Replace(System.Environment.NewLine, "\\n");
                    if (i < v.Speech.Count - 1)
                        codeOutput += "<nl>";
                }
                codeOutput += "\";\n";
            }
            // Everything below THIS LINE is hot garbage. Sorry not sorry

            if (sceneName == "Castle Intro Hallway")
            {
                foreach (var v in textobjs)
                {
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"TEXTOBJ" + v.name + "\"] = \"" +
                                  v.text + "\";\n";
                }

                foreach (var m in pdhobjs)
                {

                    foreach (var v in m.BossDialogs)
                    {
                        codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + m.name + "BossDiags" +
                                      v.BossType.ToString() + "\"] = \"";

                        for (var i = 0; i < v.Dialogs.Count; i++)
                        {
                            var k = v.Dialogs[i];
                            if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                            {
                                k.Name = "<hero>";
                            }

                            codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                          + "<br>" + k.Line1.Replace("\"", "\\\"")
                                              .Replace("nombrejugador", "<hero>")
                                          + "<br>" + k.Line2.Replace("\"", "\\\"")
                                              .Replace("nombrejugador", "<hero>")
                                          + "<br>" + k.Line3.Replace("\"", "\\\"")
                                              .Replace("nombrejugador", "<hero>");
                            if (i < v.Dialogs.Count - 1)
                                codeOutput += "<nl>";
                        }

                        codeOutput += "\";\n";
                    }

                    foreach (var v in m.EnemDialogs)
                    {
                        codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + m.name + "EnemDiags" +
                                      v.EnemType.ToString() + "\"] = \"";

                        for (var i = 0; i < v.Dialogs.Count; i++)
                        {
                            var k = v.Dialogs[i];
                            if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                            {
                                k.Name = "<hero>";
                            }

                            codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                          + "<br>" + k.Line1.Replace("\"", "\\\"")
                                              .Replace("nombrejugador", "<hero>")
                                          + "<br>" + k.Line2.Replace("\"", "\\\"")
                                              .Replace("nombrejugador", "<hero>")
                                          + "<br>" + k.Line3.Replace("\"", "\\\"")
                                              .Replace("nombrejugador", "<hero>");
                            if (i < v.Dialogs.Count - 1)
                                codeOutput += "<nl>";
                        }

                        codeOutput += "\";\n";
                    }

                }
            }




            foreach (var a in bdsobhotgarboojbs)
            {
                foreach (var v in a.Dialogs)
                {
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"wtfobj" + a.name + "\"] = \"";

                    for (var i = 0; i < v.Dialogs.Count; i++)
                    {
                        var k = v.Dialogs[i];
                        if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                        {
                            k.Name = "<hero>";
                        }
                        codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line1.Replace("\"", "\\\"")
                                          .Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line2.Replace("\"", "\\\"")
                                          .Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line3.Replace("\"", "\\\"")
                                          .Replace(System.Environment.NewLine, "\\n");
                        if (i < v.Dialogs.Count - 1)
                            codeOutput += "<nl>";
                    }
                    codeOutput += "\";\n";
                }
            }

            
            foreach (var v in itorwtfobjs)
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.name + "Drowned\"] = \"";

                for (var i = 0; i < v.DrownedDialogs.Count; i++)
                {
                    var k = v.DrownedDialogs[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.DrownedDialogs.Count - 1)
                        codeOutput += "<nl>";
                }

                codeOutput += "\";\n";
                
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.name + "Normal\"] = \"";

                for (var i = 0; i < v.NormalDialogs.Count; i++)
                {
                    var k = v.NormalDialogs[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.NormalDialogs.Count - 1)
                        codeOutput += "<nl>";
                }

                codeOutput += "\";\n";
            }
            
            
            foreach (var v in adobjs)
            {
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"" + v.name + "\"] = \"";

                for (var i = 0; i < v.Dialogs.Count; i++)
                {
                    var k = v.Dialogs[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.Dialogs.Count - 1)
                        codeOutput += "<nl>";
                }

                codeOutput += "\";\n";
            }
            
            
            foreach (var v in qobjs)
            {
                
                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"IntroDialog\"] = \"";
                for (var i = 0; i < v.IntroDialog.Count; i++)
                {
                    var k = v.IntroDialog[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.IntroDialog.Count - 1)
                        codeOutput += "<nl>";
                }
                codeOutput += "\";\n";

                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"EndDialog\"] = \"";
                for (var i = 0; i < v.EndDialog.Count; i++)
                {
                    var k = v.EndDialog[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.EndDialog.Count - 1)
                        codeOutput += "<nl>";
                }
                codeOutput += "\";\n";

                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"WonCoinsDialog\"] = \"";
                for (var i = 0; i < v.WonCoinsDialog.Count; i++)
                {
                    var k = v.WonCoinsDialog[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.WonCoinsDialog.Count - 1)
                        codeOutput += "<nl>";
                }
                codeOutput += "\";\n";

                codeOutput += "            languageStrings[\"" + sceneName + "\"][\"NoCoinsDialog\"] = \"";
                for (var i = 0; i < v.NoCoinsDialog.Count; i++)
                {
                    var k = v.NoCoinsDialog[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }
                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < v.NoCoinsDialog.Count - 1)
                        codeOutput += "<nl>";
                }
                codeOutput += "\";\n";

                for (int j = 0; j < v.Questions.Count; j++)
                {
                    var m = v.Questions[j];
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "Text\"] = \"" + m.Title + "<br>" + m.QuestionLine1 + "<br>" + m.QuestionLine2 + "\";\n";

                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "A1\"] = \"" + m.AnswerA.AnswerString + "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "A2\"] = \"" + m.AnswerB.AnswerString + "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "A3\"] = \"" + m.AnswerC.AnswerString + "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "A4\"] = \"" + m.AnswerD.AnswerString + "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "Intro\"] = \"";
                    for (var i = 0; i < m.IntroDialog.Count; i++)
                    {
                        var k = m.IntroDialog[i];
                        if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                        {
                            k.Name = "<hero>";
                        }
                        codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                        if (i < m.IntroDialog.Count - 1)
                            codeOutput += "<nl>";
                    }
                    codeOutput += "\";\n";

                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "D1\"] = \"";
                    for (var i = 0; i < m.AnswerDialogA.Count; i++)
                    {
                        var k = m.AnswerDialogA[i];
                        if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                        {
                            k.Name = "<hero>";
                        }
                        codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                        if (i < m.AnswerDialogA.Count - 1)
                            codeOutput += "<nl>";
                    }
                    codeOutput += "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "D2\"] = \"";
                    for (var i = 0; i < m.AnswerDialogB.Count; i++)
                    {
                        var k = m.AnswerDialogB[i];
                        if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                        {
                            k.Name = "<hero>";
                        }
                        codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                        if (i < m.AnswerDialogB.Count - 1)
                            codeOutput += "<nl>";
                    }
                    
                    codeOutput += "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "D3\"] = \"";
                    for (var i = 0; i < m.AnswerDialogC.Count; i++)
                    {
                        var k = m.AnswerDialogC[i];
                        if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                        {
                            k.Name = "<hero>";
                        }
                        codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                        if (i < m.AnswerDialogC.Count - 1)
                            codeOutput += "<nl>";
                    }
                    
                    codeOutput += "\";\n";
                    codeOutput += "            languageStrings[\"" + sceneName + "\"][\"Question" + j + "D4\"] = \"";
                    for (var i = 0; i < m.AnswerDialogD.Count; i++)
                    {
                        var k = m.AnswerDialogD[i];
                        if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                        {
                            k.Name = "<hero>";
                        }
                        codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                      + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>") 
                                      + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                        if (i < m.AnswerDialogD.Count - 1)
                            codeOutput += "<nl>";
                    }
                    codeOutput += "\";\n";

                }
                
                
            }
        }
        
        private void autodump<T>(T v, List<DialogPrefChooserScript> m, string sceneName, string objName) {
            codeOutput += sceneName + "^" + v.GetType().Name + "-" + objName + "^";
            for (int j = 0; j < m.Count; j++)
            {
                var n = m[j];
                for (var i = 0; i < n.Dialogs.Count; i++)
                {
                    var k = n.Dialogs[i];
                    if (k.Name == "yo" || k.Name == "YO" || k.Name == "Yo")
                    {
                        k.Name = "<hero>";
                    }

                    codeOutput += k.Name.Replace("\"", "\\\"").Replace(System.Environment.NewLine, "\\n")
                                  + "<br>" + k.Line1.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>")
                                  + "<br>" + k.Line2.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>")
                                  + "<br>" + k.Line3.Replace("\"", "\\\"").Replace("nombrejugador", "<hero>");
                    if (i < n.Dialogs.Count - 1)
                        codeOutput += "<nl>";
                }
                if (j < m.Count - 1)
                    codeOutput += "<pg>";
            }
            codeOutput += "\n";
        }
    }
}