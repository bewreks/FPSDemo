using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace FPSDemo
{
    public abstract class Command
    {
        [NonSerialized]
        private static Dictionary<Type, Queue<Command>> _commands = new Dictionary<Type, Queue<Command>>();

        public static T GetCommand<T>()
            where T : Command, new()
        {
            Command command = default(T);
            Queue<Command> commands;
            var type = typeof(T);
            _commands.TryGetValue(type, out commands);
            if (commands != null)
            {
                if (commands.Count > 0)
                {
                    command = commands.Dequeue();
                }
            }

            return (T) (command ?? new T());
        }

        protected static void Realize<T>(T command)
            where T : Command
        {
            Queue<Command> commands;
            var type = command.ChildType;
            _commands.TryGetValue(type, out commands);
            if (commands == null)
            {
                commands = new Queue<Command>();
                _commands.Add(type, commands);
            }
            commands.Enqueue(command);
        }


        [NonSerialized]
        public Type ChildType;

        [NonSerialized] protected bool _isEnabledInPause;

        protected Command()
        {
            InternalTypeSetter();
        }

        public void Execute()
        {
            if (!Main.Instance.PauseController.IsPaused || _isEnabledInPause)
            {
                try
                {
                    InternalExecute();
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }

            Realize(this);
        }

        protected abstract void InternalExecute();
        protected abstract void InternalTypeSetter();
    }
}