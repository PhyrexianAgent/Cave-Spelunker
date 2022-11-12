using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatGroupController : MonoBehaviour
{
    public BatController[] bats;

    private bool awakening = false;

    private int[] indexes;
    private int currentIndex = 0;
    void Start()
    {
        indexes = GetIndexes();
        ShuffleIndexes();
    }

    // Update is called once per frame
    void Update()
    {
     /*   if (Input.GetKeyDown(KeyCode.T))
        {
            AwakenOthers();
        }*/
    }

    public void AwakenOthers()
    {
        awakening = true;
        AwakenBat();
    }

    private void AwakenBat()
    {
        if (bats[indexes[currentIndex]].CompareState(BatStates.Sleep) || bats[indexes[currentIndex]].CompareState(BatStates.Wakening))
        {
            bats[indexes[currentIndex]].SetState(BatStates.FlyStart);
        }
        currentIndex ++;
        if (currentIndex < bats.Length)
            Invoke("AwakenBat", 0.3f);
    }

    private void ShuffleIndexes()
    {
        int temp;
        int randNum;
        for (int i=0; i<indexes.Length; i++)
        {
            randNum = Random.Range(0, indexes.Length);
            temp = indexes[randNum];
            indexes[randNum] = indexes[i];
            indexes[i] = temp;
        }
        Debug.Log(indexes[2]);
    }

    private int[] GetIndexes()
    {
        int[] indexes = new int[bats.Length];
        for (int i=0; i<indexes.Length; i++)
        {
            indexes[i] = i;
        }
        return indexes;
    }

    public bool IsAwakening()
    {
        return awakening;
    }
}
