using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.XR;
using Valve.VR;

namespace TextUITemplate.Libraries
{
    public class ControllerInputs
    {
        public static bool leftStick()
        {
            bool output;
            if (GameObject.Find("[SteamVR]") != null)
                output = SteamVR_Actions.gorillaTag_LeftJoystickClick.GetState(SteamVR_Input_Sources.LeftHand);
            else
                InputDevices.GetDeviceAtXRNode(XRNode.LeftHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out output);
            return output;
        }

        public static bool leftGrip()
        {
            return ControllerInputPoller.instance.leftGrab;
        }

        public static bool leftTrigger()
        {
            return ControllerInputPoller.instance.leftControllerIndexFloat > 0.5f;
        }

        public static bool leftPrimary()
        {
            return ControllerInputPoller.instance.leftControllerPrimaryButton;
        }

        public static bool leftSecondary()
        {
            return ControllerInputPoller.instance.leftControllerSecondaryButton;
        }

        public static bool rightStick()
        {
            bool output;
            if (GameObject.Find("[SteamVR]") != null)
                output = SteamVR_Actions.gorillaTag_RightJoystickClick.GetState(SteamVR_Input_Sources.RightHand);
            else
                InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(CommonUsages.primary2DAxisClick, out output);
            return output;
        }

        public static bool rightGrip()
        {
            return ControllerInputPoller.instance.rightGrab;
        }

        public static bool rightTrigger()
        {
            return ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f;
        }

        public static bool rightPrimary()
        {
            return ControllerInputPoller.instance.rightControllerPrimaryButton;
        }

        public static bool rightSecondary()
        {
            return ControllerInputPoller.instance.rightControllerSecondaryButton;
        }
    }
}
