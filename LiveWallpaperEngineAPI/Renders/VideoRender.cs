﻿using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Giantapp.LiveWallpaper.Engine.Renders
{
    public class VideoRender : ExternalProcessRender
    {
        //每次升级就修改这个文件名
        public static string PlayerFolderName { get; } = "video0";
        public VideoRender() : base(WallpaperType.Video, new List<string>() { ".mp4", ".flv", ".blv", ".avi", ".mov", ".gif", }, false)
        {

        }

        protected override ProcessStartInfo GetRenderExeInfo(WallpaperModel model)
        {
            string path = model.RunningData.AbsolutePath;
            //文档：https://mpv.io/manual/stable/
            string playerPath = Path.Combine(WallpaperApi.Options.ExternalPlayerFolder, $@"{PlayerFolderName}\mpv.exe");
            if (!File.Exists(playerPath))
                return null;

            FileInfo info = new FileInfo(playerPath);
            if (info.Length == 0)
                return null;

            string args = $"\"{path}\" --hwdec=auto --panscan=1.0 --loop-file=inf --fs --geometry=-10000:-10000";
            if (!model.Option.HardwareDecoding)
            {
                args = $"\"{path}\" --hwdec=no --panscan=1.0 --loop-file=inf --fs --geometry=-10000:-10000";
            }

            ProcessStartInfo r = new ProcessStartInfo(playerPath)
            {
                Arguments = args,
                UseShellExecute = false
            };
            return r;
        }
    }
}
