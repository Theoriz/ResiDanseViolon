using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MrSceneManager : MonoBehaviour
{
    //public bool PreLoadNextScene;
    public bool AutoSwitchScene;
    public float AutoSwitchTimer;
    public bool Loop;
    private float _sceneStartTime;

    public List<GameObject> ObjectsToKeepBetweenScenes;
    public string sceneLayer;
    public int sceneCount { get; set; }

    public List<string> scenesName;
    public string activeScene;

    public int currSceneIndex;
    public static bool loadingScene { get; set; }
    public static bool launchingIntro { get; set; }
    public static bool launchingOutro { get; set; }
    public static bool playingScene { get; set; }

    public bool debug;
    private bool _nextScenePreloaded = false, _isPreloadingNextScene;
    private AsyncOperation asyncLoad;
    private MrScene currMrScene;
    private Dictionary<string, int> scenesInBuild;

    void Awake()
    {
        scenesName = new List<string>();
        scenesInBuild = new Dictionary<string, int>();

        DontDestroyOnLoad(this);
        foreach (var item in ObjectsToKeepBetweenScenes)
        {
            DontDestroyOnLoad(item);
        }
    
        for (int i = 1; i < SceneManager.sceneCountInBuildSettings; i++)   //Starts at 1 to remove Core which is 0
        {
            var sceneName =
                System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility
                    .GetScenePathByBuildIndex(i));
            scenesName.Add(sceneName);
            scenesInBuild.Add(sceneName, i);
        }
    }

    private void OnEnable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged += OnNewActiveScene;
    }

    // Use this for initialization
    void Start()
    {
        // get scene count
        sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings - 1;


        // init variables
        loadingScene = false;
        launchingIntro = false;
        launchingOutro = false;
        playingScene = false;

        // load first scene
        currSceneIndex = 1;

        var lastIndex = PlayerPrefs.GetInt("lastScene", -1);
        if (lastIndex == 0) lastIndex = -1;
        if (lastIndex == -1)
        {
            PlayerPrefs.SetInt("lastScene", currSceneIndex);
        }
        else
        {
            if(debug)
                Debug.Log("Count build : " + scenesInBuild.Count + " | Last scene opened : " + lastIndex);
            if(lastIndex <= scenesInBuild.Count)
                currSceneIndex = lastIndex;
        }
        StartCoroutine(loadScene(lastIndex));
        //SceneManager.LoadScene(currSceneIndex);

        //if (PreLoadNextScene)
        //{
        //    StartCoroutine(LoadYourAsyncScene(currSceneIndex + 1));
        //}
    }

    void Update()
    {
        //LoadingScene = loadingScene;
        if (AutoSwitchScene)
        {
            if(Time.time - _sceneStartTime > AutoSwitchTimer )
            {
                Next();
            }
        }
        else
        {
            _sceneStartTime = Time.time;
        }

        if (activeScene != SceneManager.GetActiveScene().name && !loadingScene)
        {
           LoadSceneWithName(activeScene);
        }
    }

    public void Next()
    {
        if(!loadingScene)
            StartCoroutine(loadScene(currSceneIndex + 1));
    }

    public void Previous()
    {
        if (!loadingScene)
            StartCoroutine(loadScene(currSceneIndex - 1));
    }

    public void Reload()
    {
        if (!loadingScene)
            StartCoroutine(loadScene(currSceneIndex));
    }

    public void LoadSceneWithName(string name)
    {
        if (!loadingScene)
        {
            if(debug)
                Debug.Log("Build index : " + scenesInBuild[name] + " | Current index : " + currSceneIndex);
            loadingScene = true;
            StartCoroutine(loadScene(scenesInBuild[name]));
        }
    }

    public IEnumerator loadScene(int nextSceneIndex)
    {

        if (nextSceneIndex < 1)
        {
            yield break;
        }
        if (nextSceneIndex > sceneCount)
        {
            if (Loop)
                nextSceneIndex = 1;
            else
            {
                yield break;
            }
        }
        loadingScene = true;
        // a new scene is loading
        _sceneStartTime = Time.time;

        if (currMrScene != null)
        {
;            // launch scene outro
            StartCoroutine(currMrScene.LaunchOutro());
            yield return new WaitForSeconds(currMrScene.outroDuration);
        }
        //if (PreLoadNextScene)
        //{
        //    while (!_nextScenePreloaded)
        //    {
        //        if (!_isPreloadingNextScene)
        //            StartCoroutine(LoadYourAsyncScene(nextSceneIndex));
        //        yield return new WaitForFixedUpdate();
        //    }

        //    Debug.Log("4");
        //    while (!asyncLoad.isDone)
        //    {
        //        Debug.Log("Preloading next scene ...");
        //        yield return new WaitForFixedUpdate();
        //    }
        //    SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(nextSceneIndex));
        //    _nextScenePreloaded = false;
        //    _isPreloadingNextScene = false;
        //    StartCoroutine(LoadYourAsyncScene(currSceneIndex + 1));
        //}
        //else
        //{

        //if (currMrScene != null)
        //{
        //    // wait appropriate amount of time
        //        yield return new WaitForSeconds(currMrScene.outroDuration);
        //}
        if (debug)
            Debug.Log("Loading scene index " + nextSceneIndex);
        UnityEngine.SceneManagement.SceneManager.LoadScene(nextSceneIndex);
    }

    void OnNewActiveScene(Scene current, Scene next)
    {
        currSceneIndex = next.buildIndex;

        if (current.name == "Core" || next.name == "Core")
            return;
        PlayerPrefs.SetInt("lastScene", currSceneIndex);
        //    SceneManager.UnloadSceneAsync(current.buildIndex);

        // next.GetRootGameObjects()[0].SetActive(true);
        bool foundMrScene = false;
        var rootObjects = next.GetRootGameObjects();
        foreach (GameObject go in rootObjects)
        {
            if (go.GetComponent<MrScene>() != null)
            {
                currMrScene = go.GetComponent<MrScene>();
                foundMrScene = true;
                break;
            }
            else if(go.GetComponentInChildren<MrScene>() != null)
            {
                currMrScene = go.GetComponentInChildren<MrScene>();
                foundMrScene = true;
                break;
            }
        }
        if (!foundMrScene)
        {
            Debug.LogWarning("Couldn't find MrScene component in scene.");
        }
        else
        {
            // launch intro of loaded scene
            StartCoroutine(currMrScene.LaunchIntro());
        }
        ControllableMaster.LoadEveryPresets();

        //Debug.Log("New active scene : current : " + current.name + " next : " + next.name);
        activeScene = next.name;
    }

    void OnDisable()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
        UnityEngine.SceneManagement.SceneManager.activeSceneChanged -= OnNewActiveScene;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (debug)
            Debug.Log("Scene : " + scene.name + " loaded, initializing ...");
        loadingScene = false;
        // do not process Core scene
        if (scene.name == "Core") return;

        
        //if (PreLoadNextScene && scene.buildIndex == currSceneIndex + 1) //preloaded scene
        //{
        //    scene.GetRootGameObjects()[0].SetActive(false);
        //    _nextScenePreloaded = true;
        //    return;
        //}

        SceneManager.SetActiveScene(scene);
        

        if (debug)
            Debug.Log("Level " + scene.name + " ["+ scene.buildIndex + "]" + " is initialized, active scene is " + currSceneIndex);

        // scene has succesfully loaded
        loadingScene = false;
        // launch preloading of next scene
        //if (PreLoadNextScene && !_nextScenePreloaded)
        //{
        //    _isPreloadingNextScene = true;
        //    StartCoroutine(LoadYourAsyncScene(currSceneIndex + 1));
        //}
    }

    //Asynchronous scene loading is not really possible now, a asynchronous loading of scene implies an activation of gameobjects in it, even for a frame, if we could prevent this we could use it
    IEnumerator LoadYourAsyncScene(int nextSceneIndex)
    {
        _isPreloadingNextScene = true;
        // The Application loads the Scene in the background at the same time as the current Scene.
        //This is particularly good for creating loading screens. You could also load the Scene by build //number.

        if (nextSceneIndex < 1)
        {
            yield break;
        }
        if (nextSceneIndex > sceneCount)
        {
            if (Loop)
                nextSceneIndex = 1;
            else
            {
                yield break;
            }
        }
        asyncLoad = SceneManager.LoadSceneAsync(nextSceneIndex, LoadSceneMode.Additive);//, LoadSceneMode.Additive);

        //Wait until the last operation fully loads to return anything
        while (asyncLoad.progress <= 0.89f)
        {
            yield return 0;
        }
        SceneManager.GetSceneByBuildIndex(nextSceneIndex).GetRootGameObjects()[0].SetActive(false);

        _isPreloadingNextScene = false;
        _nextScenePreloaded = true;
        yield return 0;
    }

}
