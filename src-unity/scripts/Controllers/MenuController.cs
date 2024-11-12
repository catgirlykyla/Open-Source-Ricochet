using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject sourcemenuremake;

    private void Start()
    {
        sourcemenuremake.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            sourcemenuremake.SetActive(!sourcemenuremake.activeSelf);
        }
    }
}
