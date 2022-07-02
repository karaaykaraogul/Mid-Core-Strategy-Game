using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace BuildingFactoryStatic
{
    public abstract class Building
    {
        public abstract string Name {get; }
        public abstract string PrefabName{get; }
        public abstract int Width{get; }
        public abstract int Height{get; }
        public abstract void Process();
        public abstract void CreateBuilding();
    }

    public class BarracksBuilding : Building
    {
        public override string Name => "barracks";
        public override string PrefabName => "BarracksPrefab";
        public override int Width => 4;
        public override int Height => 4;

        public override void Process()
        {
            CreateBuilding();
            Debug.Log("barracks");
        }

        public override void CreateBuilding()
        {
            var buildingGameObject = Resources.Load(PrefabName) as GameObject;
            BuildingClient.instance.InitializeBuilding(buildingGameObject, this);
        }
    }

    public class PowerPlantBuilding : Building
    {
        public override string Name => "power_plant";
        public override string PrefabName => "PowerPlantPrefab";
        public override int Width => 2;
        public override int Height => 3;

        public override void Process()
        {
            CreateBuilding();
            Debug.Log("power_plant");
        }
        public override void CreateBuilding()
        {
            var buildingGameObject = Resources.Load(PrefabName) as GameObject;
            BuildingClient.instance.InitializeBuilding(buildingGameObject, this);
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
