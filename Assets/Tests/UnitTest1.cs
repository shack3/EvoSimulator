using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class UnitTest1
    {
        public Transform prefab;
        [UnityTest]
        public IEnumerator TestMovement()
        {
            var gameGameObject = GameObject.Find("Entity");
        
            for (var i = 0; i < 16; i++)
            {
                var expected = 0f;
                var aux = ToBinary(i);
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
                var result = gameGameObject.GetComponent<EntityManager>().MoveStatus();
                Assert.AreEqual(result, expected,aux);
                gameGameObject.GetComponent<EntityManager>().MoveReset();

            }//aux = 0101
            yield return null;
        }

        private static string ToBinary(int myValue)
        {
            var binVal = Convert.ToString(myValue, 2);
            var bits = 0;
            var bitblock = 4;

            for (var i = 0; i < binVal.Length; i += bitblock)
            { bits += bitblock; }

            return binVal.PadLeft(bits, '0');
        }
    
    }
    
}


