using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScreen : MonoBehaviour
{
    [SerializeField] private Animator crossFadeAnim;
    [SerializeField] float waitDelay;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(waitDelay);
        crossFadeAnim.SetTrigger("Transition");

        // while scene is fading, load scene right after it turns fully dark
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
