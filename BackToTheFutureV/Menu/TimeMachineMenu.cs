﻿using FusionLibrary;
using FusionLibrary.Extensions;
using GTA;
using LemonUI.Menus;
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
        public NativeSubmenuItem PhotoMenu { get; }
        public NativeSubmenuItem DebugMenu { get; }

        public TimeMachineMenu() : base("TimeMachine")
        {
            TimeCircuitsOn = NewCheckboxItem("TC");
            CutsceneMode = NewCheckboxItem("Cutscene");
            FlyMode = NewCheckboxItem("Hover");
            AltitudeHold = NewCheckboxItem("Altitude");
            RemoteControl = NewCheckboxItem("RC");

            PhotoMenu = NewSubmenu(MenuHandler.PhotoMenu);

            DebugMenu = NewSubmenu(MenuHandler.DebugMenu);

            NewSubmenu(MenuHandler.MainMenu);
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {

        }

        public override void Menu_Shown(object sender, EventArgs e)
        {
            if (CurrentTimeMachine == null || !FusionUtils.PlayerPed.IsFullyInVehicle())
            {
                Visible = false;
                return;
            }

            FlyMode.Enabled = CurrentTimeMachine.Mods.HoverUnderbody == ModState.On && !CurrentTimeMachine.Properties.AreFlyingCircuitsBroken;
            AltitudeHold.Enabled = FlyMode.Enabled;
            RemoteControl.Enabled = CurrentTimeMachine.Properties.IsRemoteControlled;
            TimeCircuitsOn.Enabled = !RemoteControl.Enabled && !Game.IsMissionActive;
            CutsceneMode.Enabled = !RemoteControl.Enabled;

            if (!MenuHandler.UnlockPhotoMenu)
            {
                Remove(PhotoMenu);
            }
            else if (!Items.Contains(PhotoMenu))
            {
                Add(5, PhotoMenu);
<<<<<<< Updated upstream
            }
=======
            if (!MenuHandler.UnlockDebugMenu)
                Remove(DebugMenu);
            else if (!Items.Contains(DebugMenu))
                Add(6, DebugMenu);
>>>>>>> Stashed changes
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {
            switch (sender)
            {
                case NativeCheckboxItem item when item == TimeCircuitsOn:
                    CurrentTimeMachine.Events.SetTimeCircuits?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == CutsceneMode:
                    CurrentTimeMachine.Events.SetCutsceneMode?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == FlyMode:
                    CurrentTimeMachine.Events.SetFlyMode?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == AltitudeHold:
                    CurrentTimeMachine.Events.SetAltitudeHold?.Invoke(Checked);
                    break;
                case NativeCheckboxItem item when item == RemoteControl && !Checked && CurrentTimeMachine.Properties.IsRemoteControlled:
                    RemoteTimeMachineHandler.StopRemoteControl();
                    break;
            }
        }

        public override void Tick()
        {
            if (CurrentTimeMachine == null)
            {
                Visible = false;
                return;
            }

            TimeCircuitsOn.Checked = CurrentTimeMachine.Properties.AreTimeCircuitsOn;
            CutsceneMode.Checked = CurrentTimeMachine.Properties.CutsceneMode;
            FlyMode.Checked = CurrentTimeMachine.Properties.IsFlying;
            AltitudeHold.Checked = CurrentTimeMachine.Properties.IsAltitudeHolding;
            RemoteControl.Checked = CurrentTimeMachine.Properties.IsRemoteControlled;

            if (MenuHandler.UnlockPhotoMenu && !Game.IsMissionActive)
            {
                PhotoMenu.Enabled = !CurrentTimeMachine.Constants.FullDamaged && !CurrentTimeMachine.Properties.IsRemoteControlled;
<<<<<<< Updated upstream
            }
=======

            if (MenuHandler.UnlockDebugMenu)
                DebugMenu.Enabled = !CurrentTimeMachine.Constants.FullDamaged && !CurrentTimeMachine.Properties.IsRemoteControlled;

            //EscapeMission.Enabled = !CurrentTimeMachine.Properties.IsFlying;
            //EscapeMission.Checked = MissionHandler.Escape.IsPlaying;
>>>>>>> Stashed changes
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
