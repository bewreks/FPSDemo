using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FPSDemo;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class AutoTestMainScript : MonoBehaviour
{
    public enum Behav
    {
        SAVE,
        LOAD
    }

    private List<SaveCommandJson> _commandList = new List<SaveCommandJson>();
    private List<LoadCommandJson> _loaded = new List<LoadCommandJson>();

    public Behav Beh;

    private int carret = 0;

    private void Awake()
    {
        if (Beh == Behav.SAVE)
        {
            InputCommandsController.counter = 0;
            InputCommandsController.OnCommandsUpdate += CacheCommands;
        }
        else
        {
            InputCommandsController.IsOff = true;
            Load();
        }
    }

    private void Load()
    {
        var jsons = JsonConvert.DeserializeObject<List<SaveCommandJson>>(
            File.ReadAllText(Path.Combine(Application.dataPath, "moveset.json")));
        _loaded.AddRange(
            from obj in jsons
//            orderby obj.time
            select LoadCommandJson.From(obj)
        );
    }

    private void CacheCommands()
    {
        InputCommandsController.Commands.ForEach(command =>
        {
            _commandList.Add(new SaveCommandJson
            {
                time = Time.time,
                command = JsonConvert.SerializeObject(command),
                typeName = command.ChildType.FullName
            });
        });
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            InputCommandsController.OnCommandsUpdate -= CacheCommands;
            Debug.Log($"{InputCommandsController.counter} {_commandList.Count}");
            var serializeObject = JsonConvert.SerializeObject(_commandList);
            File.WriteAllText(Path.Combine(Application.dataPath, "moveset.json"), serializeObject);
            //AssetDatabase.Refresh();
            Command.GetCommand<ExitCommand>().Execute();
        }

        if (Beh == Behav.LOAD)
        {
            while (true)
            {
                if (_loaded.Count <= carret)
                {
                    break;
                }

                if (_loaded[carret].time > Time.time)
                {
                    break;
                }
                
                _loaded[carret].command.Execute();
                carret++;

            }
        }
    }
    
    

    struct SaveCommandJson
    {
        public float time;
        public string typeName;
        public string command;
    }

    struct LoadCommandJson
    {
        public float time;
        public Command command;

        public static LoadCommandJson From(SaveCommandJson saveCommandJson)
        {
            var loadCommandJson = new LoadCommandJson();
            loadCommandJson.time = saveCommandJson.time;
            var type = Type.GetType(saveCommandJson.typeName);
            if (CreateCommand<CurrentWeaponAlternativeFireCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<CurrentWeaponFireCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<CurrentWeaponReloadCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<ExitCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<FlashlightSwitchCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<LoadGameCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<LookCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<MoveCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<SaveGameCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<SwitchWeaponCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<TakeScreenshotCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<TeammateNextPositionCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            if (CreateCommand<TeammateCallCommand>(type, saveCommandJson.command, ref loadCommandJson.command))
            {
                return loadCommandJson;
            }
            
            Debug.Log("SMTing is wrong");

            return loadCommandJson;
        }

        private static bool CreateCommand<T>(Type type, string json, ref Command command)
            where T: Command
        {
            if (type == typeof(T))
            {
                command = JsonConvert.DeserializeObject<T>(json);
                return true;
            }

            return false;
        } 
    }
}