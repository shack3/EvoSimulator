using System;
using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class IntegrationTests_1_0
    {
        public Transform prefab;
        
        public delegate void OnDeathAux();
        public delegate Boolean DeathByOldAux(float dTime, int random);
        public delegate void GetOlderAux(OnDeathAux ODA,DeathByOldAux DBOA,float dTime,int random);
        public delegate void EatAux(float dTime);
        public delegate void LearningAux();
        public delegate void NewNeuralNetworkResultAux();
        public delegate void MoveResetAux();
        public delegate void MoveForwardAux();
        public delegate void MoveBackwardAux();
        public delegate void MoveLeftAux();
        public delegate void MoveRightAux();
        public delegate void MitosisAux();

        public void OnDeath(float dTime)
        {
        }

        public Boolean GetODeathByOld(OnDeathAux ODA,DeathByOldAux DBOA,float dTime,int random)
        {
            return false;
        }
        
        public void GetOlder(EntityManager.OnDeathAux oda,EntityManager.DeathByOldAux dboa,float dTime,int random)
        {
        }

        public void Eat(float dTime)
        {
        }

        public void Learning()
        {
        }

        public void NewNeuralNetworkResult()
        {
        }
        
        public void  MoveReset()
        {
        }
        
        public void MoveForward()
        {
        }
        
        public void MoveBackward()
        {
        }
        
        public void MoveLeft()
        {
        }
        
        public void MoveRight()
        {
        }
        
        public void Mitosis()
        {
        }

        [UnityTest]
        public IEnumerator TestUpdate()
        {
            var LocalEntityManager = GameObject.Find("Entity").GetComponent<EntityManager>();
            //preconditions
            //LocalEntityManager.MyGenomeManager = new GenomeManager();
            LocalEntityManager.InitNeuralBias();
            LocalEntityManager.MyGenomeManager = new GenomeManager();
            LocalEntityManager.MyGenomeManager.InitNeuralNetwork();
            
            
            LocalEntityManager.ToExecuteInUpdate(
                LocalEntityManager.GetOlder,
                LocalEntityManager.NewNeuralNetworkResult,
                LocalEntityManager.Eat,
                LocalEntityManager.Learning,
                LocalEntityManager.MoveReset,
                LocalEntityManager.MoveForward,
                LocalEntityManager.MoveBackward,
                LocalEntityManager.MoveLeft,
                LocalEntityManager.MoveRight,
                LocalEntityManager.Mitosis
            );
            
        
            yield return null;
        }
    }
}