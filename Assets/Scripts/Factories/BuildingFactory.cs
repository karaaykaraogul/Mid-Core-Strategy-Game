using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using UnityEngine;

namespace BuildingFactoryStatic
{
    public abstract class Building
    {
        public abstract string Name {get; }
        public abstract void Process();
    }

    public class BarracksBuilding : Building
    {
        [SerializeField] Sprite barracks;
        [SerializeField] Tilemap tilemap;
        public override string Name => "barracks";

        public override void Process()
        {
            Debug.Log("barracks");
        }
    }

    public class PowerPlantBuilding : Building
    {
        public override string Name => "power_plant";

        public override void Process()
        {
            Debug.Log("power_plant");
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
