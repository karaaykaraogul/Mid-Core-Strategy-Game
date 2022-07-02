using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingFactoryStatic
{
    public abstract class Building : MonoBehaviour
    {
        public abstract string Name {get; }
        public abstract string PrefabName{get; }
        public abstract int Width{get; }
        public abstract int Height{get; }
        public virtual void Process()
        {
            CreateBuilding();
            Debug.Log(this.Name);
        }
        
        public virtual void CreateBuilding()
        {
            var buildingGameObject = Resources.Load(PrefabName) as GameObject;
            BuildingClient.instance.InitializeBuilding(buildingGameObject, this);
        }
        public virtual string GetBuildingName()
        {
            return Name;
        }
    }

    public static class BuildingFactory
    {
        private static Dictionary<string, Type> buildingsByName;
        private static bool IsInitialized => buildingsByName != null;

        private static void InitializeFactory()
        {
            if(IsInitialized)
            {
                return;
            }

            var buildingsByTypes = Assembly.GetAssembly(typeof(Building)).GetTypes().Where(myType => myType.IsClass && !myType.IsAbstract && myType.IsSubclassOf(typeof(Building)));

            buildingsByName = new Dictionary<string, Type>();

            foreach(var type in buildingsByTypes)
            {
                var effect = Activator.CreateInstance(type) as Building;
                buildingsByName.Add(effect.Name, type);
            }
        }
        
        public static Building GetBuilding(string buildingType)
        {
            InitializeFactory();

            if(buildingsByName.ContainsKey(buildingType))
            {
                Type type = buildingsByName[buildingType];
                var building = Activator.CreateInstance(type) as Building;
                return building;
            }

            return null;
        }

        public static IEnumerable<string> GetBuildingNames()
        {
            UnityEngine.Debug.Log("test");
            InitializeFactory();
            return buildingsByName.Keys;
        }
    }

}
