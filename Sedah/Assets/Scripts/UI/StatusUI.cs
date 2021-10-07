using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusUI : MonoBehaviour
{
    [SerializeField] private GameObject statusPrefab;
    private Dictionary<int, GameObject> statusImages = new Dictionary<int, GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void CreateIcon(Status status)
    {
        GameObject statusImage = Instantiate(statusPrefab, transform);
        statusImage.GetComponent<Image>().sprite = status.Image;
        statusImages.Add(status.id, statusImage);
    }

    public void RemoveIcon(int i)
    {
        GameObject statusImage = statusImages[i];
        statusImages.Remove(i);
        Destroy(statusImage);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
