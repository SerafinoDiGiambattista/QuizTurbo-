using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System.Linq;

public class HealthGUI : MonoBehaviour
{
    private int numOfHearts = 0;
    private int initialNumOfHearts;
    public GameObject redHeart;
    public GameObject grayHeart;
    public GameObject playerObject;
    protected List<GameObject> instantiatedRedHearts = new List<GameObject>();
    protected List<GameObject> instantiatedGrayHearts = new List<GameObject>();
    protected FeatureManager featuremanager;
    private RoadManager roadManager;



    private void Awake()
    {
        featuremanager= playerObject.GetComponent<FeatureManager>();
        roadManager= playerObject.GetComponent<RoadManager>();
    }

    void Start()
    { 
        initialNumOfHearts = Mathf.CeilToInt(roadManager.GetInitialHealth);
        //Debug.Log("InitialNumOFHearts: " + initialNumOfHearts) ;
        InstantiateHearts();
    }

    private void FixedUpdate()
    {
        UpdateHealth();
    }

    public List<GameObject> GetInstantiatedHearts()
    {
        return instantiatedRedHearts;
    }
    
    private void InstantiateHearts()
    {
        for(int i=0; i<initialNumOfHearts; i++)
        {
            GameObject tempGray = Instantiate(grayHeart);
            tempGray.transform.SetParent(gameObject.transform);
            instantiatedGrayHearts.Add(tempGray);
            tempGray.SetActive(false);

            GameObject temp = Instantiate(redHeart);
            temp.transform.SetParent(gameObject.transform);
            instantiatedRedHearts.Add(temp);

        }
    }

    private void UpdateHealth()
    {
        numOfHearts = Mathf.CeilToInt(roadManager.GetHealth());
        //Debug.Log("HEARTCONTROLLER Health: " + numOfHearts);

        if (numOfHearts > 0)
        {
            for (int i = initialNumOfHearts - 1; i >= numOfHearts; i--)
            {
                if (instantiatedRedHearts[i].activeSelf == true)
                {
                    Vector3 pos = instantiatedRedHearts[i].transform.position;
                    instantiatedRedHearts[i].SetActive(false);
                    instantiatedGrayHearts[i].transform.position = pos;
                    instantiatedGrayHearts[i].SetActive(true);
                }
            }

            for (int i=0; i < numOfHearts; i++)
            {
                if (instantiatedRedHearts[i].activeSelf == false)
                {
                    instantiatedRedHearts[i].SetActive(true);
                    instantiatedGrayHearts[i].SetActive(false);
                }
            }

        }
        else if (numOfHearts <= 0)
        {
            foreach(GameObject h in instantiatedRedHearts)
                h.SetActive(false);
            SceneManager.LoadScene(3);
        }
    }
}
