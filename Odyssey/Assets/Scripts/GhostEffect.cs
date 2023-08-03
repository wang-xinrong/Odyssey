using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostEffect : MonoBehaviour
{
    public GameObject GhostPrefab;
    [SerializeField]
    private float RefreshRate = 0.025f;
    private float ActiveTime = 0;
    [SerializeField]
    private float GhostFadeAwayTime = 0.25f;
    private bool TrailOn = false;
    private GameObject ghostManager;

    // Start is called before the first frame update
    void Start()
    {
        ghostManager = GameObject.Find("GhostManager");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTrail(float duration)
    {
        TrailOn = true;
        StartCoroutine(ActivateTrail(duration));
    }

    public void TerminateTrail()
    {
        TrailOn = false;
    }

    private IEnumerator ActivateTrail(float duration)
    {
        ActiveTime = duration;

        while (ActiveTime > 0 && TrailOn)
        {
            ActiveTime -= RefreshRate;
            GameObject currentGhost = Instantiate(GhostPrefab, transform.position, transform.rotation, ghostManager.transform);
            Sprite currentSprite = GetComponent<SpriteRenderer>().sprite;
            currentGhost.GetComponent<SpriteRenderer>().sprite = currentSprite;
            yield return new WaitForSeconds(RefreshRate);
            Destroy(currentGhost, GhostFadeAwayTime);
        }
    }
}