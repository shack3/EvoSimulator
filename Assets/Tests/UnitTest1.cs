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
                Assert.AreEqual(result, expected, aux);
                gameGameObject.GetComponent<EntityManager>().MoveReset();

            } //aux = 0101

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestInitNeuralNetwork()
        {
            var LocalEntityManager = GameObject.Find("Entity").GetComponent<EntityManager>();
            var LocalNeuralNetworkBias = LocalEntityManager.NeuralNetworkBias;
            float value = 0f;
            for (int pos = 0; pos < LocalNeuralNetworkBias.Length; pos++)
            {
                LocalNeuralNetworkBias[pos] = new float[100];
                for (int pos2 = 0; pos2 < LocalNeuralNetworkBias[pos].Length; pos2++)
                    Assert.AreEqual(LocalNeuralNetworkBias[pos][pos2], 0f);
            }

            LocalEntityManager.InitNeuralBias();
            for (int pos = 0; pos < LocalNeuralNetworkBias.Length; pos++)
            {
                LocalNeuralNetworkBias[pos] = new float[100];
                for (int pos2 = 0; pos2 < LocalNeuralNetworkBias[pos].Length; pos2++)
                    value = LocalNeuralNetworkBias[pos][pos2];
                Assert.GreaterOrEqual(value, -1f);
                Assert.LessOrEqual(value, 1f);
            }

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestEat()
        {
            var LocalEntityManager = GameObject.Find("Entity").GetComponent<EntityManager>();
            LocalEntityManager.imOnSun = true;
            LocalEntityManager.energy = 0;
            LocalEntityManager.bulk = 1f;
            LocalEntityManager.MyGenomeManager = new GenomeManager();
            LocalEntityManager.MyGenomeManager.PhotosynthesisEfficiency = 1f;
            Assert.AreEqual(LocalEntityManager.energy, 0f);
            LocalEntityManager.Eat(1f);
            Assert.Greater(LocalEntityManager.energy, 0f);

            yield return null;
        }

        [UnityTest]
        public IEnumerator TestDeadByOld()
        {
            var LocalEntityManager = GameObject.Find("Entity").GetComponent<EntityManager>();
            LocalEntityManager.cicleTime = 30;
            Assert.False(LocalEntityManager.DeadByOld(25, 1000));
            LocalEntityManager.age = 1;
            Assert.False(LocalEntityManager.DeadByOld(25, 1));
            LocalEntityManager.age = 100;
            Assert.True(LocalEntityManager.DeadByOld(25, 10));
            yield return null;
        }

        [UnityTest]
        public IEnumerator TestGetOlder()
        {
            var LocalEntityManager = GameObject.Find("Entity").GetComponent<EntityManager>();
            LocalEntityManager.GetOlder(AuxMethod_ODA, AuxMethod_DBOA, 1f, 2);

            yield return null;
        }

        private Boolean AuxMethod_DBOA(float aux1, int aux2)
        {
            Assert.AreEqual(aux1, 1f);
            Assert.AreEqual(aux2, 2);
            return true;
        }

        private void AuxMethod_ODA()
        {

        }

        [UnityTest]
        public IEnumerator TestEnergyConsumption()
        {
            var LocalEntityManager = GameObject.Find("Entity").GetComponent<EntityManager>();
            LocalEntityManager.energy = 100;
            LocalEntityManager.bulk = 1;
            float bulk = LocalEntityManager.bulk;
            float OldEnergy = LocalEntityManager.energy;

            for (int aux = 0; aux < LocalEntityManager.energy; aux++)
            {
                LocalEntityManager.EnergyConsumption(aux);
                Assert.AreEqual(LocalEntityManager.energy, OldEnergy - (aux * bulk));
                LocalEntityManager.energy = OldEnergy;
            }

            yield return null;
        }

        private static string ToBinary(int myValue)
        {
            var binVal = Convert.ToString(myValue, 2);
            var bits = 0;
            var bitblock = 4;

            for (var i = 0; i < binVal.Length; i += bitblock)
            {
                bits += bitblock;
            }

            return binVal.PadLeft(bits, '0');
        }

    }
}
//949 - 40- tell
//930 - 43
//701 - 43
