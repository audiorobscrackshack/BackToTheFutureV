﻿using BackToTheFutureV;
using FusionLibrary;
using FusionLibrary.Extensions;
using GTA;
using LemonUI.Menus;
using System;
using System.ComponentModel;
using static BackToTheFutureV.InternalEnums;
using static FusionLibrary.FusionEnums;

namespace BackToTheFutureV
{
    internal class MainMenu : BTTFVMenu
    {
        private readonly NativeListItem<string> spawnBTTF;
        private readonly NativeSubmenuItem presetsMenu;
        private readonly NativeItem convertIntoTimeMachine;
        private readonly NativeSubmenuItem customMenu;

        private readonly NativeSubmenuItem rcMenu;
        private readonly NativeSubmenuItem outatimeMenu;

        private readonly NativeItem deleteCurrent;
        private readonly NativeItem deleteOthers;
        private readonly NativeItem deleteAll;

        public MainMenu() : base("Main")
        {
            Subtitle = TextHandler.Me.GetLocalizedText("SelectOption");

            spawnBTTF = NewListItem("Spawn", TextHandler.Me.GetLocalizedText("DMC12", "BTTF1", "BTTF1H", "BTTF2", "BTTF3", "BTTF3RR"));
            spawnBTTF.ItemChanged += SpawnBTTF_ItemChanged;
            spawnBTTF.Description = GetItemValueDescription("Spawn", "DMC12");

            presetsMenu = NewSubmenu(MenuHandler.PresetsMenu, "Presets");

            convertIntoTimeMachine = NewItem("Convert");

            customMenu = NewSubmenu(MenuHandler.CustomMenuMain, "Custom");

            rcMenu = NewSubmenu(MenuHandler.RCMenu, "RC");
            outatimeMenu = NewSubmenu(MenuHandler.OutatimeMenu, "Outatime");

            deleteCurrent = NewItem("Remove");
            deleteOthers = NewItem("RemoveOther");
            deleteAll = NewItem("RemoveAll");

            NewSubmenu(MenuHandler.SettingsMenu, "Settings");
        }

        private void SpawnBTTF_ItemChanged(object sender, ItemChangedEventArgs<string> e)
        {
            switch (e.Index)
            {
                case 0:
                    spawnBTTF.Description = GetItemValueDescription(sender, "DMC12");
                    break;
                case 1:
                case 2:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF1");
                    break;
                case 3:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF2");
                    break;
                case 4:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF3");
                    break;
                case 5:
                    spawnBTTF.Description = GetItemValueDescription(sender, "BTTF3RR");
                    break;
            }

            Recalculate();
        }

        public override void Tick()
        {
            convertIntoTimeMachine.Enabled = FusionUtils.PlayerVehicle.IsFunctioning() && !FusionUtils.PlayerVehicle.IsTimeMachine();

            outatimeMenu.Enabled = RemoteTimeMachineHandler.RemoteTimeMachineCount > 0;

            rcMenu.Enabled = FusionUtils.PlayerVehicle == null && TimeMachineHandler.TimeMachineCount > 0;
        }

        public override void Menu_OnItemActivated(NativeItem sender, EventArgs e)
        {
            TimeMachine timeMachine;

            if (sender == spawnBTTF)
            {
                if (spawnBTTF.SelectedIndex == 0)
                {
                    FusionUtils.PlayerPed.Task.WarpIntoVehicle(DMC12Handler.CreateDMC12(FusionUtils.PlayerPed.Position, FusionUtils.PlayerPed.Heading), VehicleSeat.Driver);
                    Visible = false;
                    return;
                }

                WormholeType wormholeType = WormholeType.BTTF1;

                switch (spawnBTTF.SelectedIndex)
                {
                    case 3:
                        wormholeType = WormholeType.BTTF2;
                        break;
                    case 4:
                    case 5:
                        wormholeType = WormholeType.BTTF3;
                        break;
                }

                if (ModSettings.CinematicSpawn)
                {
                    timeMachine = TimeMachineHandler.Create(SpawnFlags.ForceReentry | SpawnFlags.New, wormholeType);
                }
                else
                {
                    timeMachine = TimeMachineHandler.Create(SpawnFlags.WarpPlayer | SpawnFlags.New, wormholeType);
                }

                if (spawnBTTF.SelectedIndex == 2)
                {
                    timeMachine.Mods.Hook = HookState.OnDoor;
                    timeMachine.Mods.Plate = PlateType.Empty;

                    timeMachine.Properties.ReactorCharge = 0;
                }

                if (spawnBTTF.SelectedIndex == 5)
                {
                    timeMachine.Mods.Wheel = WheelType.RailroadInvisible;
                }
            }

            if (sender == convertIntoTimeMachine)
            {
                FusionUtils.PlayerVehicle.TransformIntoTimeMachine();
            }

            if (sender == deleteCurrent)
            {
                timeMachine = TimeMachineHandler.GetTimeMachineFromVehicle(FusionUtils.PlayerVehicle);

                if (timeMachine == null)
                {
                    TextHandler.Me.ShowNotification("NotSeated");
                    return;
                }

                TimeMachineHandler.RemoveTimeMachine(timeMachine);

                ExternalHUD.SetOff();
            }

            if (sender == deleteOthers)
            {
                TimeMachineHandler.RemoveAllTimeMachines(true);
                RemoteTimeMachineHandler.DeleteAll();
                TextHandler.Me.ShowNotification("RemovedOtherTimeMachines");
            }

            if (sender == deleteAll)
            {
                TimeMachineHandler.RemoveAllTimeMachines();
                RemoteTimeMachineHandler.DeleteAll();
                TextHandler.Me.ShowNotification("RemovedAllTimeMachines");

                ExternalHUD.SetOff();
            }

            Visible = false;
        }

        public override void Menu_OnItemCheckboxChanged(NativeCheckboxItem sender, EventArgs e, bool Checked)
        {

        }

        public override void Menu_OnItemSelected(NativeItem sender, SelectedEventArgs e)
        {

        }

        public override void Menu_Closing(object sender, CancelEventArgs e)
        {

        }

        public override void Menu_Shown(object sender, EventArgs e)
        {
            if (!MenuHandler.UnlockSpawnMenu)
            {
                Remove(spawnBTTF);
                Remove(presetsMenu);
                Remove(customMenu);
                Remove(convertIntoTimeMachine);
            }
            else if (!Items.Contains(spawnBTTF))
            {
                Add(0, spawnBTTF);
                Add(1, presetsMenu);
                Add(2, customMenu);
                Add(3, convertIntoTimeMachine);
            }
        }

        public override void Menu_OnItemValueChanged(NativeSliderItem sender, EventArgs e)
        {

        }
    }
}
