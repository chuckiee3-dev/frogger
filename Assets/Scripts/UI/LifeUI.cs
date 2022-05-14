using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] private GameObject frogLifePrefab;

    private void OnEnable()
    {
        GameActions.onPlayerLifeUpdated += UpdateUI;
    }
    private void OnDisable()
    {
        GameActions.onPlayerLifeUpdated -= UpdateUI;
    }

    private void UpdateUI(int currentLives)
    {
        if(transform.childCount == currentLives) return;
        int lifeDifference = currentLives - transform.childCount;
        if (transform.childCount < currentLives)
        {
            for (int i = 0; i < lifeDifference; i++)
            {
                AddLifeImg();
            }
        }
        else
        {
            for (int i = lifeDifference; i < 0; i++)
            {
                RemoveLifeImg();
            }
        }
    }

    private void RemoveLifeImg()
    {
        if(transform.childCount == 0) return;
        Destroy(transform.GetChild(0).gameObject);
    }

    private void AddLifeImg()
    {
        Instantiate(frogLifePrefab, transform);
    }
}
