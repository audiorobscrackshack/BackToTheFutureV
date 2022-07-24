﻿using FusionLibrary;
using GTA;
using GTA.Native;
using KlangRageAudioLibrary;
using System;
using System.Windows.Forms;
using Screen = GTA.UI.Screen;

namespace BackToTheFutureV
{
    internal class Main : Script
    {
        public static Version Version => System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;

        public static AudioEngine CommonAudioEngine { get; set; } = new AudioEngine() { BaseSoundFolder = "BackToTheFutureV\\Sounds" };

        public static bool FirstTick { get; private set; } = true;

        public static DateTime ResetDate { get; private set; }

        public static PedReplica ResetPed { get; private set; }

        public static VehicleReplica ResetVehicle { get; private set; }

        public static int SwitchedPed { get; private set; }

        public static CustomStopwatch CustomStopwatch { get; } = new CustomStopwatch();

        public Main()
        {
            DateTime buildDate = new DateTime(2000, 1, 1).AddDays(Version.Build).AddSeconds(Version.Revision * 2);

            System.IO.File.AppendAllText($"./ScriptHookVDotNet.log", $"BackToTheFutureV - {Version} ({buildDate})" + Environment.NewLine);

            ModSettings.LoadSettings();

            Tick += Main_Tick;
            KeyDown += Main_KeyDown;
            Aborted += Main_Aborted;
        }

        private void Main_Aborted(object sender, EventArgs e)
        {
            World.RenderingCamera = null;

            Screen.FadeIn(1000);

            if (RemoteTimeMachineHandler.IsRemoteOn)
            {
                RemoteTimeMachineHandler.StopRemoteControl(true);
            }

            if (ModSettings.PersistenceSystem)
            {
                TimeMachineHandler.Save();
            }

            GarageHandler.Abort();
            MissionHandler.Abort();
            StoryTimeMachineHandler.Abort();
            RemoteTimeMachineHandler.Abort();
            TimeMachineHandler.Abort();
            FireTrailsHandler.Abort();
            CustomTrainHandler.Abort();
            DMC12Handler.Abort();
        }

        private unsafe void Main_KeyDown(object sender, KeyEventArgs e)
        {
            TimeMachineHandler.KeyDown(e);
            MissionHandler.KeyDown(e);
            MenuHandler.KeyDown(e);
        }

        private unsafe void Main_Tick(object sender, EventArgs e)
        {
            if (Game.Version >= GameVersion.v1_0_2372_0_NoSteam || Game.Version >= GameVersion.v1_0_2372_0_Steam)
            {
                if (Game.IsLoading || FusionUtils.FirstTick)
                {
                    return;
                }

                if (FirstTick)
                {
                    Screen.ShowHelpText("BackToTheFutureV loading...", 3000);

                    ResetPed = new PedReplica(FusionUtils.PlayerPed);
                    SwitchedPed = Function.Call<int>(Hash.PLAYER_PED_ID);
                    if (FusionUtils.PlayerPed.IsInVehicle())
                    {
                        ResetVehicle = new VehicleReplica(FusionUtils.PlayerVehicle);
                    }
                    RemoteTimeMachineHandler.MAX_REMOTE_TIMEMACHINES = ModSettings.MaxRecordedMachines;

                    ModelHandler.RequestModels();

                    //Disable fake shake of the cars.
                    Function.Call(Hash._​SET_​CAR_​HIGH_​SPEED_​BUMP_​SEVERITY_​MULTIPLIER, 0);

                    if (ModSettings.PersistenceSystem)
                    {
                        TimeMachineHandler.Load();
                        RemoteTimeMachineHandler.Load();
                    }

                    FusionUtils.RandomTrains = ModSettings.RandomTrains;
                    TimeHandler.RealTime = ModSettings.RealTime;
                    TimeHandler.TrafficVolumeYearBased = ModSettings.YearTraffic;

                    DecoratorsHandler.Register();
                    WeatherHandler.Register();
                }

                if (PlayerSwitch.IsInProgress && FusionUtils.PlayerPed.Model == ResetPed.Model)
                {
                    SwitchedPed = Function.Call<int>(Hash.PLAYER_PED_ID);
                }

                if (!FirstTick)
                {
                    WaybackSystem.Tick();
                }

                CustomTrainHandler.Tick();
                DMC12Handler.Tick();
                TimeMachineHandler.Tick();
                RemoteTimeMachineHandler.Tick();
                FireTrailsHandler.Tick();
                TcdEditer.Tick();
                RCGUIEditer.Tick();
                MissionHandler.Tick();
                StoryTimeMachineHandler.Tick();
                MenuHandler.Tick();
                TrashHandler.Tick();
                GarageHandler.Tick();
                WeatherHandler.Tick();

                if (FirstTick)
                {
                    ResetDate = FusionUtils.CurrentTime;
                    WaybackSystem.Tick();

                    TrafficHandler.ModelSwaps.Add(new ModelSwap
                    {
                        Enabled = true,
                        Model = ModelHandler.DMC12,
                        VehicleType = VehicleType.Automobile,
                        VehicleClass = VehicleClass.Sports,
                        DateBased = true,
                        StartProductionDate = new DateTime(1981, 1, 21, 0, 0, 0),
                        EndProductionDate = new DateTime(1982, 12, 24, 23, 59, 59),
                        MaxInWorld = 25,
                        MaxSpawned = 3,
                        WaitBetweenSpawns = 10000
                    });

                    Screen.ShowHelpText("BackToTheFutureV loaded correctly.", 3000);
                    FirstTick = false;
                }
            }
            else
            {
                Screen.ShowHelpText("~r~ERROR:~s~ ~y~BackToTheFutureV~s~ is ~o~not~s~ compatible with the currently installed version of the GTAV.~n~Please ~b~update~s~ your game to version ~g~2372~s~ or newer.", 100);
            }
        }
    }
}
