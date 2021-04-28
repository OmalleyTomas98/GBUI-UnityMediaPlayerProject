using System.Collections;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Windows.Speech;

namespace Scripts.MenuSystem

{
public class MainMenu : MonoBehaviour
{


  // GrammarRecognizer and string for Voice Commands.
        private GrammarRecognizer _grammarRecognizer;
        private string _outAction = "";

    // Enable Cursor, set time & volume and start Grammar Recognizer.
        private void Awake()
        {
            // turn cursor, volume and time back on from pause menu interaction
            Cursor.visible = true;
            Time.timeScale = 1f;

            // reset out action
            _outAction = "";
            // Load in grammar xml file.
            _grammarRecognizer = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath,
                "MainMenuControls.xml"), ConfidenceLevel.Low);
            // Start grammar recogniser.
            _grammarRecognizer.OnPhraseRecognized += GR_OnPhraseRecognised;
            _grammarRecognizer.Start();
            print("[INFO] Menu Voice Controls loaded...");
        }


        // Gets phases from MainMenuControls.xml and matches them to User input.
                private void GR_OnPhraseRecognised(PhraseRecognizedEventArgs args)
                {
                    var message = new StringBuilder();
                    // Read the semantic meanings from the args passed in.
                    var meanings = args.semanticMeanings;
                    // For each to get all the meanings.
                    foreach (var meaning in meanings)
                    {
                        // Get the items for xml file.
                        var item = meaning.values[0].Trim();
                        message.Append("Out Action: " + item);
                        // For calling in Update.
                        _outAction = item;
                    }

                    // print out action detected
                    print(message);
                }


    // Update is called once per frame
    void Update()
    {

          switch (_outAction)
            {
                // start rule
                case "start the mediaplayer":
                    StartMediaPlayer();
                    break;
                // exit rule
                case "exit the mediaplayer":
                    ExitMediaPlayer();
                    break;
            }
        
    }


      // Menu buttons/commands for menu controls.
        public void StartMediaPlayer()
        {
            StartCoroutine(PlayGame());
        }

        public void Controls()
        {
            StartCoroutine(NextScene());
        }

        public void ExitMediaPlayer()
        {
            Application.Quit();
        }


        private static IEnumerator NextScene()
        {
            yield return new WaitForSeconds(1);
        }

        private static IEnumerator PlayGame()
        {
            yield return new WaitForSeconds(1);
        }

        // Stop the Grammar Recognizer if there is no input.
        private void OnApplicationQuit()
        {
            if (_grammarRecognizer == null || !_grammarRecognizer.IsRunning) return;
            _grammarRecognizer.OnPhraseRecognized -= GR_OnPhraseRecognised;
            _grammarRecognizer.Stop();
        }
    }
}
