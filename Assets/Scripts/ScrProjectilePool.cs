using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrProjectilePool : MonoBehaviour
{
    public static ScrProjectilePool instance;

    private List<GameObject> projectilePool = new List<GameObject>();
    public int amountToPool;
    [SerializeField] private GameObject projectilePrefab;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        for(int i = 0; i<amountToPool; i++)
        {
            GameObject prj = Instantiate(projectilePrefab);
            prj.SetActive(false);
            projectilePool.Add(prj);
        }
    }

    // Update is called once per frame
    public GameObject GetPooledProjectile()
    {
        for(int i =0; i<projectilePool.Count; i++)
        {
            if (!projectilePool[i].activeInHierarchy)
            {
                return projectilePool[i];
            }
        }
        return null;
    }
}
