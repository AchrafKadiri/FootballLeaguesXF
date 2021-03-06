﻿using FootballLeaguesXF.Plugins;
using FootballShared.Plugin.FileService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(FileServiceImplementation))]
namespace FootballShared.Plugin.FileService
{
    /// <summary>
    /// Implementation for Feature
    /// </summary>
    public class FileServiceImplementation : FileServiceBase, IFileService
    {

        /// <summary>
        /// Return the root folder where the plugin will save files
        /// </summary>
        /// <returns></returns>
        protected override string EnvironmentGetFolderPath()
        {
            string ret;
#if WINDOWS_UWP
            
            ret = Windows.Storage.ApplicationData.Current.LocalCacheFolder.Path;
#else
            ret = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            //return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#endif
            return ret;
        }

        /// <summary>
        /// Directory.Exists
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        protected override bool DirectoryExists(string folder)
        {
            return Directory.Exists(folder);
        }

        /// <summary>
        /// Directory.CreateDirectory
        /// </summary>
        /// <param name="folder"></param>
        protected override void DirectoryCreateDirectory(string folder)
        {
            Directory.CreateDirectory(folder);
        }

        /// <summary>
        /// File.Exists
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected override bool FileExists(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }

        /// <summary>
        /// File.Delete
        /// </summary>
        /// <param name="file"></param>
        protected override void FileDelete(string file)
        {
            System.IO.File.Delete(file);
        }

        /// <summary>
        /// File.WriteAllText
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="result"></param>
        protected override void FileWriteAllText(string filePath, string result)
        {
            System.IO.File.WriteAllText(filePath, result);
        }

        /// <summary>
        /// File.ReadAllText
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected override string FileReadAllText(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }

        /// <summary>
        /// Directory.GetFiles
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected override string[] DirectoryGetFiles(string filePath)
        {
            return Directory.GetFiles(filePath);
        }

        /// <summary>
        /// File.SetAttributesNormal
        /// </summary>
        /// <param name="file"></param>
        protected override void FileSetAttributesNormal(string file)
        {
            System.IO.File.SetAttributes(file, FileAttributes.Normal);
        }

        /// <summary>
        /// Directory.Delete
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="bRecursive"></param>
        protected override void DirectoryDelete(string filePath, bool bRecursive)
        {
            Directory.Delete(filePath, bRecursive);
        }

        /// <summary>
        /// File.GetLastWriteTime
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        protected override DateTime FileGetLastWriteTime(string filePath)
        {
            //return System.IO.File.GetCreationTime(filePath);
            return System.IO.File.GetLastWriteTime(filePath);
        }

        /// <summary>
        /// File.SetLastWriteTime
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="dateTime"></param>
        protected override void FileSetLastWriteTime(string filePath, DateTime dateTime)
        {
            //System.IO.File.SetCreationTime(filePath, dateTime);
            System.IO.File.SetLastWriteTime(filePath, dateTime);
        }

        /// <summary>
        /// File.WriteAllBytes
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="byteArray"></param>
        protected override void FileWriteAllBytes(string filename, byte[] byteArray)
        {
            System.IO.File.WriteAllBytes(filename, byteArray);
        }

        /// <summary>
        /// Path.GetTempPath
        /// </summary>
        /// <returns></returns>
        protected override string PathGetTempPath()
        {
            return Path.GetTempPath();
        }

        /// <summary>
        /// File.ReadAllBytes
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        protected override byte[] FileReadAllBytes(string filename)
        {
            return System.IO.File.ReadAllBytes(filename);
        }

        /// <summary>
        /// Directory.EnumerateFiles
        /// </summary>
        /// <param name="documentsPath"></param>
        /// <returns></returns>
        protected override IEnumerable<string> DirectoryEnumerateFiles(string documentsPath)
        {
            return Directory.EnumerateFiles(documentsPath);
        }

        /// <summary>
        /// Encoding.UTF8.GetString
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        protected override string EncodingUTF8GetString(byte[] buffer)
        {
            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// File.OpenText
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected override StreamReader FileOpenText(string path)
        {
            return System.IO.File.OpenText(path);
        }
    }
}
