using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TheGameAgain
{
    [ExecuteInEditMode]
    public class CustomInputEditorInterface : MonoBehaviour
    {
        [Header("Editor only script")]
        [SerializeField]
        private CustomInputManager inputManager;

        [SerializeField]
        CustomInputManager.Axis axis = new CustomInputManager.Axis();

        [Header("Buttons")]
        public Boolean addAxis;
        public Boolean clear;
        public Boolean defaultInputs;
        public Boolean minimumInputs;
        

        [Header("File")]
        public String fileName = "InputManager.asset";
        public Boolean loadInputs;
        public Boolean saveInputs;

        public void Update()
        {
            if (addAxis) { AddAxis(); }
            if (clear) { ClearAxis(); }
            if (defaultInputs) { DefaultInputs(); }
            if (minimumInputs) { MinimumInputs(); }
            if (saveInputs) { SaveInputs(); }
            if (loadInputs) { LoadInputs(); }
        }

        private void AddAxis()
        {
            inputManager.axes.Add(axis);
            axis = new CustomInputManager.Axis();
            addAxis = false;
        }

        private void ClearAxis()
        {
            axis = new CustomInputManager.Axis();
            clear = false;
        }

        private void DefaultInputs()
        {
            //Clear axes
            inputManager.axes.Clear();

            //Key inputs
            CustomInputManager.Axis axis = inputManager.AddAxis("Horizontal");
            axis.button.positive = "right";
            axis.button.negative = "left";
            axis.altButton.positive = "d";
            axis.altButton.negative = "a";
            axis.snap = true;
            axis.gravity = 3f;
            axis.sensitivity = 3f;

            axis = inputManager.AddAxis("Vertical");
            axis.button.positive = "up";
            axis.button.negative = "down";
            axis.altButton.positive = "w";
            axis.altButton.negative = "s";
            axis.snap = true;
            axis.gravity = 3f;
            axis.sensitivity = 3f;

            axis = inputManager.AddAxis("Fire1");
            axis.button.positive = "left ctrl";
            axis.altButton.positive = "mouse 0";

            axis = inputManager.AddAxis("Fire2");
            axis.button.positive = "left alt";
            axis.altButton.positive = "mouse 1";

            axis = inputManager.AddAxis("Fire3");
            axis.button.positive = "left shift";
            axis.altButton.positive = "mouse 2";

            axis = inputManager.AddAxis("Jump");
            axis.button.positive = "space";

            //Mouse inputs
            axis = inputManager.AddAxis("Mouse X");
            axis.gravity = 0f;
            axis.deadzone = 0f;
            axis.sensitivity = 0.1f;
            axis.type = InputType.Mouse_Movement;
            axis.axis = InputAxis.X_axis;

            axis = inputManager.AddAxis("Mouse Y");
            axis.gravity = 0f;
            axis.deadzone = 0f;
            axis.sensitivity = 0.1f;
            axis.type = InputType.Mouse_Movement;
            axis.axis = InputAxis.Y_axis;

            axis = inputManager.AddAxis("Mouse ScrollWheel");
            axis.gravity = 0f;
            axis.deadzone = 0f;
            axis.sensitivity = 0.1f;
            axis.type = InputType.Mouse_Movement;
            axis.axis = InputAxis.Z_axis;

            //Joystick inputs
            axis = inputManager.AddAxis("Horizontal");
            axis.gravity = 0f;
            axis.deadzone = 0.19f;
            axis.sensitivity = 1f;
            axis.type = InputType.Joystick_Axis;
            axis.axis = InputAxis.X_axis;
            axis.joystick = 0;

            axis = inputManager.AddAxis("Vertical");
            axis.gravity = 0f;
            axis.deadzone = 0.19f;
            axis.sensitivity = 1f;
            axis.invert = true;
            axis.type = InputType.Joystick_Axis;
            axis.axis = InputAxis.Y_axis;
            axis.joystick = 0;

            axis = inputManager.AddAxis("Fire1");
            axis.button.positive = "joystick button 0";

            axis = inputManager.AddAxis("Fire2");
            axis.button.positive = "joystick button 1";

            axis = inputManager.AddAxis("Fire3");
            axis.button.positive = "joystick button 2";

            axis = inputManager.AddAxis("Jump");
            axis.button.positive = "joystick button 3";

            axis = inputManager.AddAxis("Submit");
            axis.button.positive = "return";
            axis.altButton.positive = "joystick button 0";

            axis = inputManager.AddAxis("Submit");
            axis.button.positive = "enter";
            axis.altButton.positive = "space";

            axis = inputManager.AddAxis("Cancel");
            axis.button.positive = "escape";
            axis.altButton.positive = "joystick button 1";

            defaultInputs = false;
        }

        private void MinimumInputs()
        {
            minimumInputs = false;
        }

        private void LoadInputs()
        {
            inputManager.Load(fileName);
            loadInputs = false;
        }

        private void SaveInputs()
        {
            inputManager.Save(fileName);
            saveInputs = false;
        }
    }
}