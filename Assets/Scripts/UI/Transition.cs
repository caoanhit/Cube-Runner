using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    public static Transition Instance;
    public Image image;
    public float speed;
    public AnimationCurve curve;
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this) Destroy(this.gameObject);
    }
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += FadeOut;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= FadeOut;
    }
    public void FadeIn()
    {
        StopAllCoroutines();
        StartCoroutine(IFadeIn());
    }
    public void FadeOut()
    {
        StopAllCoroutines();
        StartCoroutine(IFadeOut());
    }
    public void FadeOut(Scene scene, LoadSceneMode mode)
    {
        StopAllCoroutines();
        StartCoroutine(IFadeOut());
    }
    IEnumerator IFadeOut()
    {
        float time = speed;
        while (time > 0)
        {
            SetTransparency(time / speed);
            time -= Time.unscaledDeltaTime;
            yield return null;
        }
        SetTransparency(curve.Evaluate(0));
        image.gameObject.SetActive(false);
    }
    IEnumerator IFadeIn()
    {
        image.gameObject.SetActive(true);
        float time = 0;
        while (time < speed)
        {
            SetTransparency(time / speed);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        SetTransparency(curve.Evaluate(1));
    }
    IEnumerator ILoadScene(string sceneName, LoadSceneMode mode)
    {
        float time = 0;
        while (time < speed)
        {
            SetTransparency(time / speed);
            time += Time.unscaledDeltaTime;
            yield return null;
        }
        SetTransparency(curve.Evaluate(1));
        SceneManager.LoadScene(sceneName, mode);
    }
    void SetTransparency(float transparency)
    {
        Color color = image.color;
        color.a = curve.Evaluate(transparency);
        image.color = color;
    }
    public void LoadScene(string sceneName, LoadSceneMode mode)
    {
        image.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ILoadScene(sceneName, mode));
    }
    public void ReloadScene()
    {
        image.gameObject.SetActive(true);
        StopAllCoroutines();
        StartCoroutine(ILoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single));
    }
}
