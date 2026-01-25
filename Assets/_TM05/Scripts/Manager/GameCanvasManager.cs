using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Pixelplacement;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Initialization))]
public class GameCanvasManager : Singleton<GameCanvasManager>
{
        // [Header("Canvas Main Menu")] 
        // public CanvasPlayerController CanvasPlayerController;
        //
        //  public CanvasInventory CanvasInventory;
        //
        //  [SerializeField] private CanvasBlueprint CanvasBlueprint;
        // [SerializeField] private CanvasCardCollection CanvasCardCollection;
        //
        // [Header("Canvas Lobby")]
        // [SerializeField] private CanvasCreateRoom CanvasCreateRoom;
        // [SerializeField] private CanvasJoinRoom CanvasJoinRoom;
        //
        // [Header("Canvas In Game")] 
        // [SerializeField] private CanvasHUD CanvasHUD;
        // [SerializeField] private CanvasGameOver CanvasGameOver;
        // [SerializeField] private CanvasGamePause CanvasGamePause;
        // [SerializeField] private CanvasSettings CanvasSettings;
        // [SerializeField] private CanvasLeaderboard CanvasLeaderboard;
        // [SerializeField] private CanvasGameStore CanvasGameStore;
        //
        // [Header("Canvas Load Game")]
        // [SerializeField] private CanvasClickToContinue CanvasClickToContinue;
        
        
        
        //public Dictionary<string, CanvasBase> CanvasList = new Dictionary<string, CanvasBase>();
        
        public enum CurrentScene
        {
            MAIN_MENU,
            GAME_PLAY
        }
        
        [Header("Something else")]
        public CurrentScene currentScene;
        public GameObject currentCanvas;
        
        
        void Start()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            
            HideChildren();
            StartCoroutine(AddCanvasToDict());
        }
        

        private void HideChildren()
        {
            //CanvasInventory.gameObject.SetActive(false);
            foreach(Transform child in transform)
            {
                child.gameObject.SetActive(false);  
            }
        }
        

        IEnumerator AddCanvasToDict()
         {
        yield return new WaitForSeconds(0.02f);
        // CanvasList.Add(Define.CANVAS_INVENTORY, CanvasInventory);
        // CanvasList.Add(Define.CANVAS_BLUEPRINT, CanvasBlueprint);


        //     CanvasList.Add(DefineValue.CANVAS_CREDITS, CanvasCredits);
        //     CanvasList.Add(DefineValue.CANVAS_HUD, CanvasHUD);
        //     CanvasList.Add(DefineValue.CANVAS_GAME_OVER, CanvasGameOver);
        //     CanvasList.Add(DefineValue.CANVAS_GAME_PAUSE, CanvasGamePause);
        //     CanvasList.Add(DefineValue.CANVAS_SETTINGS, CanvasSettings);
        //     CanvasList.Add(DefineValue.CANVAS_LEADERBOARD, CanvasLeaderboard);
        //     CanvasList.Add(DefineValue.CANVAS_CARD_COLLECTION, CanvasCardCollection);
        //     CanvasList.Add(DefineValue.CANVAS_CREAT_ROOM, CanvasCreateRoom);
        //     CanvasList.Add(DefineValue.CANVAS_JOIN_ROOM, CanvasJoinRoom);
        //     
        //     CanvasList.Add(DefineValue.CANVAS_GAME_STORE, CanvasGameStore);
        //     CanvasList.Add(DefineValue.CANVAS_CLICK_TO_CONTINUE, CanvasClickToContinue);
        //     // CanvasList[DefineValue.CANVAS_EXIT].Show();
        }
        
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            HideChildren();
            string sceneName = scene.name;  // Get the name of the loaded scene

            // Choose and play the appropriate music based on sceneName
            switch (sceneName)
            {
                case "MainMenu":
                    // AudioManager.Instance.StopAll(Audio.Type.Music);
                    // AudioManager.Instance.Play(AudioEnum.MainMenu_SplashOfHope);
                    currentScene = CurrentScene.MAIN_MENU;
                    
                    break;
                case "GamePlay":
                    // AudioManager.Instance.StopAll(Audio.Type.Music);
                    // AudioManager.Instance.Play(AudioEnum.GameTheme_WigglyAmbition);
                    currentScene = CurrentScene.GAME_PLAY;
                    //CanvasList[DefineValue.CANVAS_HUD].Show();
                    break;
            }
        }

        // public void Handle(Message message)
        // {
        //     
        // }
}
