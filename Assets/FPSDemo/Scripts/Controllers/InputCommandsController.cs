using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace FPSDemo
{
    public class InputCommandsController : MonoBehaviour
    {
        public static UnityAction OnCommandsUpdate;

        public static bool IsOff;

        public static List<Command> Commands => _commands;
        
        private static List<Command> _commands = new List<Command>();

        public static int counter = 0;
        
        private void Update()
        {
            if (IsOff)
            {
                return;
            }

            if (Input.GetButtonDown("SwitchFlashlight"))
            {
                var command = Command.GetCommand<FlashlightSwitchCommand>();
                _commands.Add(command);
            }
            
            if (Input.GetButton("Fire1"))
            {
                _commands.Add(Command.GetCommand<CurrentWeaponFireCommand>());
            }

            CurrentWeaponAlternativeFireCommand alternativeFireCommand;
            if (Input.GetButtonDown("Fire2"))
            {
                alternativeFireCommand = Command.GetCommand<CurrentWeaponAlternativeFireCommand>();
                alternativeFireCommand.IdDown = true;
                _commands.Add(alternativeFireCommand);
            }
            if (Input.GetButtonUp("Fire2"))
            {
                alternativeFireCommand = Command.GetCommand<CurrentWeaponAlternativeFireCommand>();
                alternativeFireCommand.IdDown = false;
                _commands.Add(alternativeFireCommand);
            }

            if (Input.GetButtonDown("Reload"))
            {
                _commands.Add(Command.GetCommand<CurrentWeaponReloadCommand>());
            }

            if (Input.GetButtonUp("Cancel"))
            {
                _commands.Add(Command.GetCommand<PauseCommand>());
            }

            var axis = Input.GetAxis("Mouse ScrollWheel");
            if (axis != 0)
            {
                var switchWeaponCommand = Command.GetCommand<SwitchWeaponCommand>();
                switchWeaponCommand.Type(axis);
                _commands.Add(switchWeaponCommand);
            }

            var moveCommand = Command.GetCommand<MoveCommand>();
            moveCommand.Initialize(Input.GetAxis("Horizontal"),
                                   Input.GetAxis("Vertical"));
            _commands.Add(moveCommand);

            var lookCommand = Command.GetCommand<LookCommand>();
            lookCommand.Initialize(Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"));
            _commands.Add(lookCommand);
            
            if (Input.GetButtonDown("M8NextPosition"))
            {
                _commands.Add(Command.GetCommand<TeammateNextPositionCommand>());
            }

            if (Input.GetButtonDown("CallM8"))
            {
                _commands.Add(Command.GetCommand<TeammateCallCommand>());
            }

            if (Input.GetKeyUp(KeyCode.F5))
            {
                _commands.Add(Command.GetCommand<SaveGameCommand>());
            }
            if (Input.GetKeyUp(KeyCode.F9))
            {
                _commands.Add(Command.GetCommand<LoadGameCommand>());
            }

            counter += _commands.Count;
            OnCommandsUpdate?.Invoke();
            _commands.Clear();
        }

        private void OnGUI()
        {
            
            if (Event.current.type == EventType.KeyUp && Event.current.keyCode == KeyCode.SysReq)
            {
                _commands.Add(Command.GetCommand<TakeScreenshotCommand>());
            }
        }
    }
}