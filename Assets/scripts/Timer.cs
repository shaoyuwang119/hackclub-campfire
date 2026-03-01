using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField]
    TMP_Text time;

    public static float currentTime = 0;

    void Update()
    {
        currentTime += Time.deltaTime;
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);
        string sec_space = seconds < 10 ? "0" : "";
        string min_space = minutes < 10 ? "0" : "";
        time.text = $"{min_space}{minutes}:{sec_space}{seconds}";
    }
}
