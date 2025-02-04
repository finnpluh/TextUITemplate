using BepInEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TextUITemplate.Libraries;
using TextUITemplate.Mods;
using TMPro;
using UnityEngine;

namespace TextUITemplate.Management
{
    public class Menu
    {
        private static GameObject parent = null;
        private static TextMeshPro text = null;

        private static bool toggled = true;
        public static int page_index = 0;
        public static int index = 0;
        private static float cooldown;

        public static List<Button[]> pages = new List<Button[]>();

        public static void Start()
        {
            pages.Add(new Button[]
            {
                new Button { title = "Example Page", tooltip = "An example page", isToggleable = false, action = () => UpdateCurrentPage(1) },
                new Button { title = "Ping Counter", tooltip = "Displays your ping on a seperate interface", toggled = true, isToggleable = true, action = () => PingCounter.Load(), disableAction = () => PingCounter.Cleanup() },
                new Button { title = "Tool Tips", tooltip = "Displays information for the module you have selected", toggled = true, isToggleable = true, action = () => ToolTips.Load(), disableAction = () => ToolTips.Cleanup() },
            });

            pages.Add(new Button[]
            {
                new Button { title = "Example Toggle", tooltip = "An example toggle", toggled = false, isToggleable = true, },
                new Button { title = "Example Button", tooltip = "An example button", toggled = false, isToggleable = false, },
                new Button { title = "Back", tooltip = "Returns to the home page", isToggleable = false, action = () => UpdateCurrentPage(0) },
            });
        }

        public static void Load()
        {
            if (GorillaTagger.hasInstance)
            {
                if (parent == null)
                    Interfaces.Create("Menu", ref parent, ref text, TextAlignmentOptions.TopRight);
                else
                {
                    foreach (Button[] modules in pages)
                        foreach (Button module in modules)
                            if (module.isToggleable)
                                if (module.toggled)
                                    if (module.action != null)
                                        module.action();

                    if (text.renderer.material.shader != Shader.Find("GUI/Text Shader"))
                        text.renderer.material.shader = Shader.Find("GUI/Text Shader");

                    Button[] buttons = pages[page_index];
                    if (toggled)
                    {
                        if (!parent.activeSelf)
                            parent.SetActive(true);
                        else
                        {
                            if (ControllerInputs.leftStick() && ControllerInputs.rightTrigger() || UnityInput.Current.GetKey(KeyCode.UpArrow))
                            {
                                if (Time.time >= cooldown)
                                {
                                    if (index > 0)
                                        index--;
                                    else
                                        index = buttons.Length - 1;

                                    cooldown = Time.time + 0.25f;
                                }
                            }

                            if (ControllerInputs.leftStick() && ControllerInputs.rightGrip() || UnityInput.Current.GetKey(KeyCode.DownArrow))
                            {
                                if (Time.time >= cooldown)
                                {
                                    if (index + 1 != buttons.Length)
                                        index++;
                                    else
                                        index = 0;

                                    cooldown = Time.time + 0.25f;
                                }
                            }

                            if (ControllerInputs.leftStick() && ControllerInputs.rightPrimary() || UnityInput.Current.GetKey(KeyCode.RightArrow))
                            {
                                if (Time.time >= cooldown)
                                {
                                    if (buttons[index].isToggleable)
                                    {
                                        buttons[index].toggled = !buttons[index].toggled;

                                        if (!buttons[index].toggled)
                                            if (buttons[index].disableAction != null)
                                                buttons[index].disableAction();
                                    }
                                    else
                                        if (buttons[index].action != null)
                                        buttons[index].action();

                                    cooldown = Time.time + 0.25f;
                                }
                            }
                        }
                    }
                    else
                        if (parent.activeSelf)
                        parent.SetActive(false);

                    if (ControllerInputs.leftStick() && ControllerInputs.rightStick() || UnityInput.Current.GetKey(KeyCode.Tab))
                    {
                        if (Time.time >= cooldown)
                        {
                            toggled = !toggled;
                            cooldown = Time.time + 0.25f;
                        }
                    }

                    string display = $"<size={text.fontSize * 1.5f}><color={Color32ToHTML(Settings.theme)}>{Settings.title}</color></size>\n";
                    for (int i = 0; i < buttons.Length; i++)
                    {
                        display += $"{((i == index) ? "-> " : string.Empty)}{buttons[i].title} ";
                        if (buttons[i].isToggleable)
                            display += buttons[i].toggled ? $"<color={Color32ToHTML(Settings.theme)}>[ON]</color>" : "<color=red>[OFF]</color>";
                        display += "\n";
                    }
                    text.text = display;

                    parent.transform.position = GorillaTagger.Instance.headCollider.transform.position + GorillaTagger.Instance.headCollider.transform.forward * 2.75f;
                    parent.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
                }
            }
        }

        private static void UpdateCurrentPage(int page)
        {
            if (page >= 0)
            {
                if (page < pages.Count)
                {
                    page_index = page;

                    if (index != 0)
                        index = 0;
                }
            }
        }

        public static string Color32ToHTML(Color32 color)
        {
            return $"#{color.r:X2}{color.g:X2}{color.b:X2}";
        }
    }
}
