using System.Reflection;

namespace DamEnovaWebApi.Enova
{
    public static class Start
    {
        private static Soneta.Start.Loader loader;

        static public void LoadLibraries()
        {
            // Wymuś katalog Enova, zastąp zmienną prywatną w klasie Loader przez referencję
            typeof(Soneta.Start.Loader).GetField("assemblyPath", BindingFlags.Static | BindingFlags.NonPublic).SetValue(null, @"C:\Program Files (x86)\Soneta\enova365 2012.4.9.0");
            
            // Uruchom loader
            loader = new Soneta.Start.Loader();
            //loader.WithUI = false;
            //loader.WithExtensions = true;
            //loader.WithExtra = false;
            //loader.WithNet = true;
            loader.Load();
            
            // Wymuś położenie plików konfiguracji
            Soneta.Tools.FileStorageProvider.InitLocalFolder(Soneta.Tools.FileStorageProvider.LocalFolderLocation.DomainDirectory);
        }
    }
}