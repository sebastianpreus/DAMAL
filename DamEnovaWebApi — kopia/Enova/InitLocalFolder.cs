using Soneta.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Enova
{
    public class InitLocalFolder
    {
        public static void Init()

        {

            // Wymuś położenie plików konfiguracji

            FileStorageProvider.InitLocalFolder(FileStorageProvider.LocalFolderLocation.DomainDirectory);

        }
    }
}