using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class UnitTest1 : MonoBehaviour
{
    public Transform Prefab;

    [UnityTest]
    public IEnumerator Test_Movement()
    {
        String aux;
        float result,expected;
        GameObject gameGameObject = GameObject.Find("Entity");
        
        for (int i = 0; i < 16; i++)
        {
            expected = 0f;
            aux = ToBinary(i);
            if (aux[0] == '1')
            {
                gameGameObject.GetComponent<EntityManager>().MoveBackward();
                expected += -1.0f;
            }
            if (aux[1] == '1')
            {
                gameGameObject.GetComponent<EntityManager>().MoveForward();
                expected += +1.0f;
            }
            if (aux[2] == '1')
            {
                gameGameObject.GetComponent<EntityManager>().MoveLeft();
                expected += -10.0f;
            }
            if (aux[3] == '1')
            {
                gameGameObject.GetComponent<EntityManager>().MoveRight();
                expected += +10.0f;
            }
            result = gameGameObject.GetComponent<EntityManager>().MoveStatus();
            Assert.AreEqual(result, expected,aux);
            gameGameObject.GetComponent<EntityManager>().MoveReset();

        }//aux = 0101
        yield return null;
    }
    
    
    
    public static string ToBinary(int myValue)
    {
        string binVal = Convert.ToString(myValue, 2);
        int bits = 0;
        int bitblock = 4;

        for (int i = 0; i < binVal.Length; i = i + bitblock)
        { bits += bitblock; }

        return binVal.PadLeft(bits, '0');
    }
    
}


