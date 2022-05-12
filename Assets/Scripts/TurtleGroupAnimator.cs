using System.Collections;
using UnityEngine;

public class TurtleGroupAnimator : MonoBehaviour
{

    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Sprite[] animSprites;
    private SpriteRenderer[] _turtles;
    public float AnimationDuration => animationDuration;

    private WaitForSeconds _stepWait;
    private void Awake()
    {
        _turtles = GetComponentsInChildren<SpriteRenderer>();

        if (animSprites == null || animSprites.Length < 2)
        {
            Debug.LogError("Animating turtles requires at least 2 sprites!");
            return;
        }
        _stepWait = new WaitForSeconds(animationDuration / animSprites.Length);

        ResetTurtles();
    }

    private void ResetTurtles()
    {
        if(animSprites == null || animSprites.Length == 0) return;
        SetSpritesToIndex(0);
    }

    private void SetSpritesToIndex(int i)
    {
        foreach (var turtle in _turtles)
        {
            turtle.sprite = animSprites[i];
        }
    }

    public void StartGoingDown()
    {
        if(animSprites == null || animSprites.Length == 0) return;
        StartCoroutine(AnimateGoingDown());
    }

    private IEnumerator AnimateGoingDown()
    {
        for (int i = 0; i < animSprites.Length; i++)
        {
            yield return _stepWait;
            SetSpritesToIndex(i);
        }
    }
    public void StartGoingUp()
    {
        if(animSprites == null || animSprites.Length == 0) return;
        StartCoroutine(AnimateGoingUp());
    }

    private IEnumerator AnimateGoingUp()
    {
        for (int i = animSprites.Length - 1; i >= 0; i--)
        {
            SetSpritesToIndex(i);
            yield return _stepWait;
        }
    }
}
