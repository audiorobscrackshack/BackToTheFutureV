﻿using LemonUI.Menus;
using System;
using System.ComponentModel;
using static BackToTheFutureV.InternalEnums;

namespace BackToTheFutureV
{
    internal class TimeMachineMenu : BTTFVMenu
    {
        public NativeCheckboxItem TimeCircuitsOn { get; }
        public NativeCheckboxItem CutsceneMode { get; }
        public NativeCheckboxItem FlyMode { get; }
        public NativeCheckboxItem AltitudeHold { get; }
        public NativeCheckboxItem RemoteControl { get; }
        //public NativeCheckboxItem EscapeMission { get; }

        //public NativeSubmenuItem CustomMenu { get; }
        public NativeSubmenuItem PhotoMenu { get; }
        public NativeSubmenuItem BackToMain { get; }

        public TimeMachineMenu() : base("TimeMachine")
        {
            TimeCircuitsOn = NewCheckboxItem("TC");
            CutsceneMode = NewCheckboxItem("Cutscene");
            FlyMode = NewCheckboxItem("Hover");
            AltitudeHold = NewCheckboxItem("Altitude");
            RemoteControl = NewCheckboxItem("RC");
            //Add(EscapeMission = new NativeCheckboxItem("Escape Mission"));

            //CustomMenu = NewSubmenu(MenuHandler.CustomMenu, "Custom");

            PhotoMenu = NewSubmenu(MenuHandler.PhotoMenu, "Photo");

            BackToMain = NewSubmenu(MenuHandler.MainMenu, "GoToMain");
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {

        }

        public override void Menu_Shown(object sender, EventArgs e)
        {
            if (TimeMachineHandler.CurrentTimeMachine == null)
            {
                Visible = false;
                return;
            }

            FlyMode.Enabled = TimeMachineHandler.CurrentTimeMachine.Mods.HoverUnderbody == ModState.On && !TimeMachineHandler.CurrentTimeMachine.Properties.AreFlyingCircuitsBroken;
            AltitudeHold.Enabled = FlyMode.Enabled;
            RemoteControl.Enabled = TimeMachineHandler.CurrentTimeMachine.Properties.IsRemoteControlled;
            //CustomMenu.Enabled = !TimeMachineHandler.CurrentTimeMachine.Properties.IsRemoteControlled;
            TimeCircuitsOn.Enabled = !RemoteControl.Enabled;
            CutsceneMode.Enabled = !RemoteControl.Enabled;

            if (!MenuHandler.UnlockPhotoMenu)
                Remove(PhotoMenu);
            else if (!Items.Contains(PhotoMenu))
                Add(5, PhotoMenu);
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {
            if (sender == TimeCircuitsOn)
            {
                TimeMachineHandler.CurrentTimeMachine.Events.SetTimeCircuits?.Invoke(Checked);
            }
            else if (sender == CutsceneMode)
            {
                TimeMachineHandler.CurrentTimeMachine.Events.SetCutsceneMode?.Invoke(Checked);
            }
            else if (sender == RemoteControl && !Checked && TimeMachineHandler.CurrentTimeMachine.Properties.IsRemoteControlled)
            {
                RemoteTimeMachineHandler.StopRemoteControl();
            }
            else if (sender == FlyMode)
            {
                TimeMachineHandler.CurrentTimeMachine.Events.SetFlyMode?.Invoke(Checked);
            }
            else if (sender == AltitudeHold)
            {
                TimeMachineHandler.CurrentTimeMachine.Events.SetAltitudeHold?.Invoke(Checked);
            }
            //else if (sender == EscapeMission)
            //{
            //    if (Checked)
            //        MissionHandler.Escape.Start();
            //    else
            //        MissionHandler.Escape.End();

            //    Close();
            //}
        }

        public override void Tick()
        {
            if (TimeMachineHandler.CurrentTimeMachine == null)
            {
                Visible = false;
                return;
            }

            TimeCircuitsOn.Checked = TimeMachineHandler.CurrentTimeMachine.Properties.AreTimeCircuitsOn;
            CutsceneMode.Checked = TimeMachineHandler.CurrentTimeMachine.Properties.CutsceneMode;
            FlyMode.Checked = TimeMachineHandler.CurrentTimeMachine.Properties.IsFlying;
            AltitudeHold.Checked = TimeMachineHandler.CurrentTimeMachine.Properties.IsAltitudeHolding;
            RemoteControl.Checked = TimeMachineHandler.CurrentTimeMachine.Properties.IsRemoteControlled;

            //CustomMenu.Enabled = !TimeMachineHandler.CurrentTimeMachine.Constants.FullDamaged && !TimeMachineHandler.CurrentTimeMachine.Properties.IsRemoteControlled;

            if (MenuHandler.UnlockPhotoMenu)
                PhotoMenu.Enabled = !TimeMachineHandler.CurrentTimeMachine.Constants.FullDamaged && !TimeMachineHandler.CurrentTimeMachine.Properties.IsRemoteControlled;

            //EscapeMission.Enabled = !TimeMachineHandler.CurrentTimeMachine.Properties.IsFlying;
            //EscapeMission.Checked = MissionHandler.Escape.IsPlaying;
        }

        public override void Menu_OnItemValueChanged(NativeSliderItem sender, EventArgs e)
        {

        }

        public override void Menu_OnItemSelected(NativeItem sender, SelectedEventArgs e)
        {

        }

        public override void Menu_Closing(object sender, CancelEventArgs e)
        {

        }
    }
}
