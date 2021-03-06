﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace FileSystemWebApiAngular.Models
{
    public class FileInfoDataModel
    {
        public List<FileInfoData> findAll()
        {
            List<FileInfoData> li = new List<FileInfoData>();
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            foreach (DriveInfo d in allDrives)
            {
                if (d.DriveType == DriveType.Fixed)
                {
                    li.Add(new FileInfoData { DirectoryName = d.Name, Puth = "" });
                }
            }
            return li;
        }

        public List<FileInfoData> findAll(string id)
        {
            List<FileInfoData> li = new List<FileInfoData>();
            string sDir = id;
            List<string> dirs = new List<string>(Directory.EnumerateDirectories(sDir));
            if (dirs.Count > 0)
            {
                foreach (var dir in dirs)
                {
                    li.Add(new FileInfoData { Puth = sDir + @"\", DirectoryName = dir.Substring(dir.LastIndexOf("\\") + 1), BackStap = ".." });
                }
            }
            else
            {
                li.Add(new FileInfoData { Puth = sDir + @"\", BackStap = ".." });
            }
            DirectoryInfo directory = new DirectoryInfo(id);
            try
            {
                foreach (FileInfo file in directory.GetFiles())
                {
                    li.Add(new FileInfoData { FileName = file.Name, Puth = file.DirectoryName, BackStap = ".." });
                }
            }
            catch (UnauthorizedAccessException) { li.Add(new FileInfoData { Error = "Доступ к этой папке закрыт!Для просмотра этой папки нужны права администратора!" }); }
            //подсчет файлов
            var filesTop = Enumerable.Empty<FileInfo>();
            var filesAll = Enumerable.Empty<FileInfo>();
            try
            {
                var root = new DirectoryInfo(sDir);
                if (sDir.Count() > 3)
                {
                    filesTop = root.EnumerateFiles(".", SearchOption.AllDirectories);
                    var filesSmall = filesTop.Where(fi => fi.Length <= 10485760).Count();
                    var filesBig = filesTop.Where(fi => fi.Length > 104857600).Count();
                    var filesMiddle = filesTop.Where(fi => fi.Length > 10485760 && fi.Length < 52428800).Count();
                    li.Add(new FileInfoData { SizeSmall = filesSmall, SizeBig = filesBig, SizeMiddle = filesMiddle });
                }
                else
                {
                    filesTop = root.EnumerateFiles(".", SearchOption.TopDirectoryOnly);
                    var filesSmall = filesTop.Where(fi => fi.Length <= 10485760).Count();
                    var filesBig = filesTop.Where(fi => fi.Length > 104857600).Count();
                    var filesMiddle = filesTop.Where(fi => fi.Length > 10485760 && fi.Length < 52428800).Count();
                    li.Add(new FileInfoData { SizeSmall = filesSmall, SizeBig = filesBig, SizeMiddle = filesMiddle });
                }
            }
            catch (UnauthorizedAccessException) { li.Add(new FileInfoData { Error = "Доступ к этой папке закрыт!Для просмотра этой папки нужны права администратора!" }); }
            return li;
        }

        public List<FileInfoData> findAllBack(string id)
        {
            string direc = id;
            List<FileInfoData> li = new List<FileInfoData>();
            if (direc.Count() > 4)
            {
                if (direc.EndsWith(@"\"))
                {
                    direc = direc.Remove((direc.Length - 1));
                }
                string sDir = Path.GetDirectoryName(direc);
                List<string> dirs = new List<string>(Directory.EnumerateDirectories(sDir));
                foreach (var dir in dirs)
                {
                    li.Add(new FileInfoData { Puth = sDir + @"\", DirectoryName = dir.Substring(dir.LastIndexOf("\\") + 1), BackStap = ".." });
                }
                DirectoryInfo directory = new DirectoryInfo(sDir);
                try
                {
                    foreach (FileInfo file in directory.GetFiles())
                    {
                        li.Add(new FileInfoData { FileName = file.Name, Puth = file.DirectoryName });
                    }
                }
                catch (UnauthorizedAccessException) { li.Add(new FileInfoData { Error = "Доступ к этой папке закрыт!Для просмотра этой папки нужны права администратора!" }); }
                //подсчет файлов
                var filesTop = Enumerable.Empty<FileInfo>();
                var filesAll = Enumerable.Empty<FileInfo>();
                try
                {
                    var root = new DirectoryInfo(sDir);
                    if (sDir.Count() > 3)
                    {
                        filesTop = root.EnumerateFiles(".", SearchOption.AllDirectories);
                        var filesSmall = filesTop.Where(fi => fi.Length <= 10485760).Count();
                        var filesBig = filesTop.Where(fi => fi.Length > 104857600).Count();
                        var filesMiddle = filesTop.Where(fi => fi.Length > 10485760 && fi.Length < 52428800).Count();
                        li.Add(new FileInfoData { SizeSmall = filesSmall, SizeBig = filesBig, SizeMiddle = filesMiddle });
                    }
                    else
                    {
                        filesTop = root.EnumerateFiles(".", SearchOption.TopDirectoryOnly);
                        var filesSmall = filesTop.Where(fi => fi.Length <= 10485760).Count();
                        var filesBig = filesTop.Where(fi => fi.Length > 104857600).Count();
                        var filesMiddle = filesTop.Where(fi => fi.Length > 10485760 && fi.Length < 52428800).Count();
                        li.Add(new FileInfoData { SizeSmall = filesSmall, SizeBig = filesBig, SizeMiddle = filesMiddle });
                    }
                }
                catch (UnauthorizedAccessException) { li.Add(new FileInfoData { Error = "Доступ к этой папке закрыт!Для просмотра этой папки нужны права администратора!" }); }
            }
            else
            {
                DriveInfo[] allDrives = DriveInfo.GetDrives();
                foreach (DriveInfo d in allDrives)
                {
                    if (d.DriveType == DriveType.Fixed)
                    {
                        li.Add(new FileInfoData { DirectoryName = d.Name, Puth = "", BackStap = "" });
                    }
                }
            }
            return li;
        }
    }
}