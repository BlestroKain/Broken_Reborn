﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Intersect.Editor.Content;
using Intersect.Editor.Forms;
using Intersect.Editor.General;
using Intersect.Editor.Localization;
using Intersect.Editor.Maps;
using Intersect.Time;

namespace Intersect.Editor.Core
{

    public static partial class Main
    {

        private static long sAnimationTimer = Timing.Global.Milliseconds;

        private static int sFps;

        private static int sFpsCount;

        private static long sFpsTime;

        private static Thread sMapThread;

        private static FrmMain sMyForm;

        private static FrmProgress sProgressForm;

        private static long sWaterfallTimer = Timing.Global.Milliseconds;

        public static void StartLoop()
        {
            AppDomain.CurrentDomain.UnhandledException += Program.CurrentDomain_UnhandledException;
            Globals.MainForm.Visible = true;
            Globals.MainForm.EnterMap(Globals.CurrentMap == null ? Guid.Empty : Globals.CurrentMap.Id);
            sMyForm = Globals.MainForm;

            if (sMapThread == null)

            {
                sMapThread = new Thread(UpdateMaps);
                sMapThread.Start();

                // drawing loop
                while (sMyForm.Visible) // loop while the window is open
                {
                    RunFrame();
                }
            }
        }

        public static void DrawFrame()
        {
            //Check Editors
            if (Globals.ResourceEditor != null && Globals.ResourceEditor.IsDisposed == false)
            {
                Globals.ResourceEditor.Render();
            }

            if (Globals.MapGrid == null)
            {
                return;
            }

            lock (Globals.MapGrid.GetMapGridLock())
            {
                Graphics.Render();
            }
        }

        public static void RunFrame()
        {
            //Shooting for 30fps
            var startTime = Timing.Global.Milliseconds;
            sMyForm.Update();

            if (sWaterfallTimer < Timing.Global.Milliseconds)
            {
                Globals.WaterfallFrame++;
                if (Globals.WaterfallFrame == 3)
                {
                    Globals.WaterfallFrame = 0;
                }

                sWaterfallTimer = Timing.Global.Milliseconds + 500;
            }

            if (sAnimationTimer < Timing.Global.Milliseconds)
            {
                Globals.AutotileFrame++;
                if (Globals.AutotileFrame == 3)
                {
                    Globals.AutotileFrame = 0;
                }

                sAnimationTimer = Timing.Global.Milliseconds + 600;
            }

            DrawFrame();

            GameContentManager.Update();
            Networking.Network.Update();
            Application.DoEvents(); // handle form events

            sFpsCount++;
            if (sFpsTime < Timing.Global.Milliseconds)
            {
                sFps = sFpsCount;
                sMyForm.toolStripLabelFPS.Text = Strings.MainForm.fps.ToString(sFps);
                sFpsCount = 0;
                sFpsTime = Timing.Global.Milliseconds + 1000;
            }

            Thread.Sleep(Math.Max(1, (int) (1000 / 60f - (Timing.Global.Milliseconds - startTime))));
        }

        private static void UpdateMaps()
        {
            while (!Globals.ClosingEditor)
            {
                if (Globals.MapsToScreenshot.Count > 0 &&
                    Globals.FetchingMapPreviews == false &&
                    sMyForm.InvokeRequired)
                {
                    if (sProgressForm == null || sProgressForm.IsDisposed || sProgressForm.Visible == false)
                    {
                        sProgressForm = new FrmProgress();

                        sProgressForm.SetTitle(Strings.MapCacheProgress.title);
                        new Task(() => Globals.MainForm.ShowDialogForm(sProgressForm)).Start();
                        while (Globals.MapsToScreenshot.Count > 0)
                        {
                            try
                            {
                                var maps = MapInstance.Lookup.ValueList.ToArray();
                                foreach (MapInstance map in maps)
                                {
                                    if (!sMyForm.Disposing && sProgressForm.IsHandleCreated)
                                    {
                                        sProgressForm.BeginInvoke(
                                            (Action) (() => sProgressForm.SetProgress(
                                                Strings.MapCacheProgress.remaining.ToString(
                                                    Globals.MapsToScreenshot.Count
                                                ), -1, false
                                            ))
                                        );
                                    }

                                    if (map != null)
                                    {
                                        map.Update();
                                    }

                                    Networking.Network.Update();
                                    Application.DoEvents();
                                }
                            }
                            catch (Exception ex)
                            {
                                Logging.Log.Error(
                                    ex, "JC's Solution for UpdateMaps collection was modified bug did not work!"
                                );
                            }

                            Thread.Sleep(50);
                        }

                        Globals.MapGrid.ResetForm();
                        while (!sProgressForm.IsHandleCreated)
                        {
                            Thread.Sleep(50);
                        }

                        sProgressForm.BeginInvoke(new Action(() => sProgressForm.Close()));
                    }
                }

                Thread.Sleep(100);
            }
        }

    }

}
