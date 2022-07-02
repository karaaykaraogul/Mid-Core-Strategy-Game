using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UnitFactoryStatic
{
    public abstract class Unit : MonoBehaviour
    {
        public abstract string Name {get; }
        //need implementation of producer building id
        //define id's for buildings
        public abstract string PrefabName{get; }
        public abstract int Health{get; }
        public abstract int Speed{get; }
        public virtual void Process()
        {
            CreateUnit();
            Debug.Log(this.Name);
        }
        
        public virtual void CreateUnit()
        {
            var unitGameObject = Resources.Load(PrefabName) as GameObject;
            //BuildingClient.instance.InitializeBuilding(unitGameObject, this);
        }
        public virtual string GetUnitName()
        {
            return Name;
        }
    }

    public static class UnitFactory
    {
        private static Dictionary<string, Type> unitsByName;
        
        private static bool IsInitialized => unitsByName != null;

        private static void InitializeFactory()
        {
            if(IsInitialized)
            {
                return;
            }

            var unitsByTypes = Assembly.GetAssembly(typeof(Unit)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Unit)));

            unitsByName = new Dictionary<string, Type>();

            foreach(var type in unitsByTypes)
            {
                var effect = Activator.CreateInstance(type) as Unit;
                unitsByName.Add(effect.Name, type);
            }
        }
        
        public static Unit GetBuilding(string buildingType)
        {
            InitializeFactory();

            if(unitsByName.ContainsKey(buildingType))
            {
                Type type = unitsByName[buildingType];
                var unit = Activator.CreateInstance(type) as Unit;
                return unit;
            }

            return null;
        }

        public static void GetUnitsByProducer()
        {
            InitializeFactory();
            var unitsByTypes = Assembly.GetAssembly(typeof(Unit)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Unit)));
            foreach(var type in unitsByTypes)
            {
                Debug.Log("type: " + type);
            }
            //Debug.Log(unitsByProducer.First().Name);
        }

        public static IEnumerable<string> GetBuildingNames()
        {
            UnityEngine.Debug.Log("test");
            InitializeFactory();
            return unitsByName.Keys;
        }
    }

}

