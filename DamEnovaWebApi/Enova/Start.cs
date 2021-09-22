using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace DamEnovaWebApi.Enova
{
    public static class Start
    {
        private static Soneta.Start.Loader loader;

        static public void LoadLibraries()
        {
            // Wymuś katalog Enova, zastąp zmienną prywatną w klasie Loader przez referencję
            typeof(Soneta.Start.Loader).GetField("assemblyPath", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, @"C:\Program Files (x86)\Soneta\enova365 2012.3.4.0");

            // Uruchom loader
            loader = new Soneta.Start.Loader();
            loader.WithUI = false;
            loader.WithExtensions = false;
            loader.WithExtra = false;
            loader.WithNet = false;
            loader.Load();
            
            // Wymuś położenie plików konfiguracji
            Soneta.Tools.FileStorageProvider.InitLocalFolder(Soneta.Tools.FileStorageProvider.LocalFolderLocation.DomainDirectory);
        }
    }
}