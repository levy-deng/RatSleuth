using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainBuilder : MonoBehaviour
{
    public GameObject before;
    public GameObject after;

    public bool isTalkable;
    public bool disableFirst;
    private PlotMaster plt;
    public int myCheckpoint;

    private void Start() {
        if (disableFirst) {
            before.SetActive(false);
        }
        else {
            before.SetActive(true);
        }
        plt = PlotMaster.Instance;
        after.SetActive(false);
    }
    public void Phase() {
        if (plt.check(myCheckpoint)) {
            before.SetActive(false);
            after.SetActive(true);
        }
    }
}
