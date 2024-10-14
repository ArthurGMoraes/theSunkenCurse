using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public GameObject CoracaoPrefab;
    public PlayerHealth playerHealth;
    List<Heart> hearts = new List<Heart>();

    private void Start()
    {
        DrawHearts();
    }

    public void DrawHearts()
    {
        ClearHearts();
        int maxHealthRemainder = playerHealth.maxHealth % 2;
        int totalHearts = (playerHealth.maxHealth / 2 + maxHealthRemainder);

        for (int i = 0; i < totalHearts; i++)
        {
            CreateEmpty();
        }

        for (int i = 0; i < hearts.Count; i++)
        {
            int heartStatusRemainder = Mathf.Clamp(playerHealth.health - (i * 2), 0, 2);
            hearts[i].SetHeart((HeartStatus)heartStatusRemainder);
        }
    }

    public void CreateEmpty()
    {
        GameObject newHeart = Instantiate(CoracaoPrefab);
        newHeart.transform.SetParent(transform);

        Heart heartComponent = newHeart.GetComponent<Heart>();
        heartComponent.SetHeart(HeartStatus.Empty);
        hearts.Add(heartComponent);
    }

    public void ClearHearts()
    {
        foreach(Transform t in transform)
        {
            Destroy(t.gameObject); 
        }
        hearts = new List<Heart>();
    }

    private void OnEnable()
    {
        PlayerHealth.OnPlayerDamaged += DrawHearts;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDamaged -= DrawHearts;
    }


}
