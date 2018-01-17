using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

namespace TheGameAgain
{
    public enum InputType
    {
        Key_or_Mouse_Button, Mouse_Movement, Joystick_Axis
    }
    //Has 28 different axis support... Z axis is mousewheel or 3rd 
    public enum InputAxis
    {
        X_axis, Y_axis, Z_axis, Axis_4, Axis_5, Axis_6, Axis_7, Axis_8, Axis_9, Axis_10, Axis_11, Axis_12, Axis_13, Axis_14, Axis_15, Axis_16, Axis_17, Axis_18, Axis_19, Axis_20, Axis_21, Axis_22, Axis_23, Axis_24, Axis_25, Axis_26, Axis_27, Axis_28
    }
    public class CustomInputManager : MonoBehaviour
    {
        [Serializable]
        public struct YinYang
        {
            public string positive, negative;

            public void defaults()
            {
                positive = "";
                negative = "";
            }
        }
        [Serializable]
        public class Axis
        {
            public string name;
            public YinYang description, button, altButton;
            public float gravity, deadzone, sensitivity;
            public bool snap, invert;
            public InputType type;
            public InputAxis axis;
            public int joystick;

            public Axis()
            {
                name = "";
                description = new YinYang();
                description.defaults();
                button = new YinYang();
                button.defaults();
                altButton = new YinYang();
                altButton.defaults();
                gravity = 1000f;
                deadzone = 0.001f;
                sensitivity = 1000f;
                snap = false;
                invert = false;
                type = InputType.Key_or_Mouse_Button;
                axis = InputAxis.X_axis;
                joystick = 0;
            }
        }

        [SerializeField]
        public List<Axis> axes = new List<Axis>();

        // Use this for initialization
        void Start()
        {
            Load(Application.dataPath + "/../ProjectSettings/InputManager.asset");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public Axis AddAxis(string name)
        {
            axes.Add(new Axis());
            Axis x = axes[axes.Count - 1];
            x.name = name;
            return x;
        }

        public void Save(string filename)
        {
            string target = Application.dataPath;
            if (filename == "InputManager.asset")
            {
                target += "/../ProjectSettings/InputManager.asset";
            }
            else
            {
                target += "/" + Path.GetFileNameWithoutExtension(filename) + ".asset";
            }

            string start = "%YAML 1.1\n" +
                "% TAG !u!tag:unity3d.com,2011:\n" +
                "---!u!13 & 1\n" +
                "InputManager:\n" +
                "  m_ObjectHideFlags: 0\n" +
                "  serializedVersion: 2\n" +
                "  m_Axes:\n";
            string content = start;
            axes.ForEach(delegate (Axis axis) {
                content += "  - serializedVersion: 3\n" +
                "    m_Name: " + axis.name + "\n" +
                "    descriptiveName: " + axis.description.positive + "\n" +
                "    descriptiveNegativeName: " + axis.description.negative + "\n" +
                "    negativeButton: " + axis.button.positive + "\n" +
                "    positiveButton: " + axis.button.negative + "\n" +
                "    altNegativeButton: " + axis.altButton.positive + "\n" +
                "    altPositiveButton: " + axis.altButton.negative + "\n" +
                "    gravity: " + axis.gravity.ToString() + "\n" +
                "    dead: " + axis.deadzone.ToString() + "\n" +
                "    sensitivity: " + axis.sensitivity.ToString() + "\n" +
                "    snap: " + (axis.snap ? 1 : 0) + "\n" +
                "    invert: " + (axis.invert ? 1 : 0) + "\n" +
                "    type: " + (int)axis.type + "\n" +
                "    axis: " + (int)axis.axis + "\n" +
                "    joyNum: " + axis.joystick + "\n";
            });

            Write(target, content);
        }

        private void Write(string target, string content)
        {
            Debug.Log("Write method is not implemented yet");
            File.WriteAllText(target, content);
        }

        public void Load(string filename)
        {
            string target = Application.dataPath;
            if (filename == "InputManager.asset")
            {
                target += "/../ProjectSettings/InputManager.asset";
            }
            else
            {
                target += "/" + Path.GetFileNameWithoutExtension(filename) + ".asset";
                if (!File.Exists(target))
                {
                    //If file doesn't exist, leave.
                    Debug.Log("File doesn't exist");
                    return;
                }
            }

            string content = File.ReadAllText(target);
            //TODO read data to axes list
            string[] rows = content.Split('\n');
            axes.Clear();
            Axis x = null;
            for(int i = 7, j = 0; (i + j) < rows.Length; j++)
            {
                string[] data = rows[i + j].Split(':');
                if (j % 16 == 0)
                {
                    //If these happens, stop reading more
                    if (data.Length == 0) return; //Nothing to split
                    if (!data[0].Contains("serializedVersion"))
                    {
                        //There's something, but it's not right.
                        Debug.Log("Wrong line or something... data: " + data.ToString());
                        return;
                    }
                    continue;
                }
                if (j % 16 == 1) { x = AddAxis(data[1].Trim()); continue; }
                if (j % 16 == 2) { x.description.positive = data[1].Trim(); continue; }
                if (j % 16 == 3) { x.description.negative = data[1].Trim(); continue; }
                if (j % 16 == 4) { x.button.negative = data[1].Trim(); continue; }
                if (j % 16 == 5) { x.button.positive = data[1].Trim(); continue; }
                if (j % 16 == 6) { x.altButton.negative = data[1].Trim(); continue; }
                if (j % 16 == 7) { x.altButton.positive = data[1].Trim(); continue; }
                if (j % 16 == 8) { x.gravity = float.Parse(data[1].Trim()); continue; }
                if (j % 16 == 9) { x.deadzone = float.Parse(data[1].Trim()); continue; }
                if (j % 16 == 10) { x.sensitivity = float.Parse(data[1].Trim()); continue; }
                if (j % 16 == 11) { x.snap = int.Parse(data[1].Trim()).Equals(1); continue; }
                if (j % 16 == 12) { x.invert = int.Parse(data[1].Trim()).Equals(1); continue; }
                if (j % 16 == 13) { x.type = (InputType)int.Parse(data[1].Trim()); continue; }
                if (j % 16 == 14) { x.axis = (InputAxis)int.Parse(data[1].Trim()); continue; }
                if (j % 16 == 15) { x.joystick = int.Parse(data[1].Trim()); continue; }
            }
        }
    }
}