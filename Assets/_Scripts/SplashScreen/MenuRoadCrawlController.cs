using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRoadCrawlController : MonoBehaviour
{
    [SerializeField] GameObject roadSegment;

    public void spawnNextSegment() => Instantiate(roadSegment, transform.parent);

    public void destroySegment() => Destroy(this.gameObject);
}
