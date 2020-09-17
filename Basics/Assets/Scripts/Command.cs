using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace CommandSpace
{
    [System.Flags]
    public enum WindDirection
    {
        North   = 1 << 0,
        East    = 1 << 1,   
        South   = 1 << 2,
        West    = 1 << 3
    }

    public class Command : MonoBehaviour
    {
        public WindDirection windDirection;

        [SerializeField] private GameObject _enemyPrefab;

        private List<ICommand> commands = new List<ICommand>();
        private int commandIndex = 0;

        public void Setup()
        {
            //1110 NORTH WEST
            //1000 EAST
            //&
            //1000

            bool containsEast = (windDirection & WindDirection.East) != 0;
            switch (windDirection)
            {
                case WindDirection.East:

                    break;
                case WindDirection.North:

                    break;

            }

            InputHandlerScalable inputHandlerScalable = new InputHandlerScalable();
            var fireCommand = new FireGunCommand();
            inputHandlerScalable.BindInputToCommand(KeyCode.X, fireCommand);
            //Alternative Fire Button
            inputHandlerScalable.BindInputToCommand(KeyCode.R, fireCommand);

            InputHandler inputHandler = new InputHandler(new JumpCommand(), new WeaponSwitchCommand());
            IGameObjectCommand command = inputHandler.HandleInput();
            command.Execute(gameObject);


            //Dictionary<string, int> dictionary = new Dictionary<string, int>();
            //dictionary.Add("Hoi", 3);
            //dictionary["Hoi"]
            //dictionary.ContainsKey("Hoi")
            //dictionary.TryGetValue("Hoi", out int result);
            //foreach (KeyValuePair<string, int> kv in dictionary)
            //{
            //    kv.Key
            //    kv.Value
            //}

        }

        public void Update()
        {
            //Create a new command
            if (Input.GetMouseButtonDown(0))
            {
                //We need to clear all dangling commands
                for (int i = commands.Count - 1; i >= commandIndex; i--)
                {
                    commands[i].ClearCommand();
                    commands.RemoveAt(i);
                }
                //Execute a new command
                Vector3 hitPos;
                if (RayCastPosition(out hitPos))
                {
                    var createCommand = new CreateObjectCommand(_enemyPrefab, hitPos, Quaternion.identity);
                    createCommand.Execute();
                    commands.Add(createCommand);
                    commandIndex++;
                    Debug.Log("Created An Object");
                }
            }

            //Undo the last command
            if (Input.GetMouseButtonDown(1))
            {
                commandIndex = Mathf.Max(0, commandIndex - 1);
                commands[commandIndex]?.Undo();
                Debug.Log("Did an Undo Operation");
            }

            //Redo a command
            if (Input.GetMouseButtonDown(2))
            {
                if (commandIndex < commands.Count)
                {
                    commands[commandIndex]?.Execute();
                    commandIndex = Mathf.Min(commands.Count, commandIndex + 1);
                }
                Debug.Log("Did a Redo Operation");
            }

        }

        public bool RayCastPosition(out Vector3 pos)
        {
            pos = Vector3.zero;
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit))
            {
                pos = hit.point;
                return true;
            }
            return false;
        }

    }

    public interface ICommand
    {
        void Execute();
        void Undo();
        void ClearCommand();
    }

    public class FireGunCommand : ICommand
    {
        public void Execute()
        {
            FireGun();
        }

        public void Undo() { }
        public void ClearCommand() { }

        public void FireGun()
        {
            Debug.Log("Piew piew");
        }
    }

    public class JumpCommand : IGameObjectCommand
    {
        public void Execute(GameObject actor)
        {
            //Do the Jump!
        }
    }
    public class WeaponSwitchCommand : IGameObjectCommand
    {
        public void Execute(GameObject actor)
        {
            //Do the WeaponSwitch!
        }
    }
    public class CreateObjectCommand : ICommand
    {
        public GameObject Prefab { get; private set; }
        public Vector3 Position { get; private set; }
        public Quaternion Rotation { get; private set; }
        public GameObject GameObjectInstance { get; private set; }

        public CreateObjectCommand(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            Prefab = prefab;
            Position = position;
            Rotation = rotation;
        }

        public void Execute()
        {
            //Build the Object
            GameObjectInstance = Object.Instantiate(Prefab, Position, Rotation);
        }

        public void Undo()
        {
            //Destroy the gameObject
            Object.Destroy(GameObjectInstance);
        }

        public void ClearCommand()
        {
            //If the Command is removed, we also remove the GameObject Instance
            Object.Destroy(GameObjectInstance);
        }
    }


    public interface IGameObjectCommand
    {
        void Execute(GameObject actor);
    }

    public class InputHandler
    {
        private IGameObjectCommand XButtonPressed;
        private IGameObjectCommand YButtonPressed;

        public InputHandler(IGameObjectCommand XButtonCommand, IGameObjectCommand YButtonCommand)
        {
            this.XButtonPressed = XButtonCommand;
            this.YButtonPressed = YButtonCommand;
        }

        public IGameObjectCommand HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.X)) { return XButtonPressed; }
            if (Input.GetKeyDown(KeyCode.Y)) { return YButtonPressed; }
            return null;
        }
    }

    public class InputHandlerScalable
    {
        private List<KeyCommand> keyCommands = new List<KeyCommand>();

        public void HandleInput()
        {
            foreach (var keyCommand in keyCommands)
            {
                if (Input.GetKeyDown(keyCommand.key))
                {
                    keyCommand.command.Execute();
                }
            }
        }

        public void BindInputToCommand(KeyCode keyCode, ICommand command)
        {
            keyCommands.Add(new KeyCommand()
            {
                key = keyCode,
                command = command
            });
        }

        public void UnBindInput(KeyCode keyCode)
        {
            var items = keyCommands.FindAll(x => x.key == keyCode);
            items.ForEach(x => keyCommands.Remove(x));
        }

        public class KeyCommand
        {
            public KeyCode key;
            public ICommand command;
        }
    }


}

/*
namespace CommandExample
{
    public interface ICommand
    {
        void Execute();
    }

    public abstract class Command : ICommand
    {
        public abstract void Execute();
    }

    public class JumpCommand : Command
    {
        public float JumpHeight { get; set; }
        public override void Execute()
        {
            //Do the jump
        }
    }

    public class InputHandler
    {
        public ICommand XButtonCommand;

        public InputHandler(ICommand XButtonCommand)
        {
            this.XButtonCommand = XButtonCommand;
        }

        public ICommand HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.X)) { return XButtonCommand; }
            return null;
        }

    }

    public class GameController
    {
        InputHandler inputHandler;

        public void Awake()
        {
            ICommand xCommand = new JumpCommand();
            inputHandler = new InputHandler(xCommand);
        }

        public void Update()
        {
            ICommand result = inputHandler.HandleInput();
            if (result != null)
            {
                result.Execute();
            }
        }

    }
}
*/