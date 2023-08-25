using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkullPlatform : Activatable
{
    public GameObject[] conditions;
    public int nextSceneNUM, thisSceneNUM;
    [SerializeField] bool activated;
    GameObject player;
    GameObject HUD;
    public GameObject respawn, dust;

    // Start is called before the first frame update
    void Start()
    {
        HUD = GameObject.FindWithTag("HUD");
    }
    override public  void Activate()
    {
        activated = true;
        // play some sort of animation? glow effects? something to indicate you need to step on this thing.
        dust.SetActive(true);
    }
    override public  void CheckCondition()
    {
        foreach (var item in conditions)
        {
            if (!item.GetComponent<Torch>().isLit)
            {
                return;
            }
        }
        Activate();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (activated && other.CompareTag("Player"))
        {
            player = other.gameObject;
            print("End of lvl1\nLoading lvl2");
            //end level
            StartCoroutine(LoadAsyncScene());
        }
    }
    IEnumerator LoadAsyncScene()
    {
        Destroy(respawn);
        SceneManager.LoadScene(nextSceneNUM, LoadSceneMode.Additive);
        Scene NextScene = SceneManager.GetSceneAt(1);
        SceneManager.MoveGameObjectToScene(player, NextScene);
        SceneManager.MoveGameObjectToScene(HUD, NextScene);
        player.GetComponent<Player>().GetLvlRespawn();
        player.GetComponent<Player>().Respawn();
        player.GetComponent<Inventory>().RemoveAllKeys();
        yield return null;
        StartCoroutine(UnloadScene());
    }
    IEnumerator UnloadScene()
    {
        AsyncOperation unload = SceneManager.UnloadSceneAsync(thisSceneNUM);
        while (!unload.isDone)
        {
            yield return null;
        }
    }
}
