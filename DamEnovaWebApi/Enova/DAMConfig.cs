using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DamEnovaWebApi.Enova
{
    public class DAMConfig
    {
        public DAMConfig()
        {
            try
            {
                // Config/Global
                //System.Xml.XPath.XPathDocument doc = new System.Xml.XPath.XPathDocument(@"config.xml");

                //foreach (System.Xml.XPath.XPathNavigator child in doc.CreateNavigator().Select("Config/Global"))
                //{
                //    xml_global_DBName = child.SelectSingleNode("DBName").Value;
                //    xml_global_DBuser = child.SelectSingleNode("DBUser").Value;
                //    xml_global_DBpassword = child.SelectSingleNode("DBPassword").Value;
                //    xml_global_DBserver = child.SelectSingleNode("DBServer").Value.ToString().Replace(@"\\", @"\");
                //    xml_global_EnovaFirma = child.SelectSingleNode("EnovaFirma").Value;
                //    xml_global_EnovaUser = child.SelectSingleNode("EnovaUser").Value;
                //    xml_global_EnovaUserPwd = child.SelectSingleNode("EnovaUserPwd").Value;
                //}


                //xml_global_DBName = "Demo";
                //xml_global_DBuser = "sa";
                //xml_global_DBpassword = "Symfonia1";
                //xml_global_DBserver = @"DESKTOP-2CVI6HT\SQLEXPRESS";
                ////xml_global_DBserver = @".\ENOVA";                

                //xml_global_EnovaFirma = "TestDanych";
                //xml_global_EnovaUser = "Damal1";
                //xml_global_EnovaUserPwd = "Lamad1#";

                //xml_global_EnovaFirma = "BametPBabrajKH";
                //xml_global_EnovaUser = "Administrator";
                //xml_global_EnovaUserPwd = "";

                //BAMET:
                xml_global_DBName = "Bamet_to_SOP3";
                xml_global_DBuser = "sa";
                xml_global_DBpassword = "Symfonia1$";
                xml_global_DBserver = @"BAMETSERVER\ENOVA";

                xml_global_EnovaFirma = "Bamet_to_SOP3";
                xml_global_EnovaUser = "SOP3";
                xml_global_EnovaUserPwd = "Symfonia1#";
            }
            catch (Exception)
            {
                //MessageBox.Show(e.Message);
                //messa("Exception: {0}", e.Message);
                //Environment.Exit(1);
            }
            //Environment.Exit(0);
        }

        private string xml_global_DBName;  // the name field
        public string DbName    // the Name property
        {
            get
            {
                return xml_global_DBName;
            }
        }


        private string xml_global_DBuser;  // the name field
        public string DbUser    // the Name property
        {
            get
            {
                return xml_global_DBuser;
            }
        }

        private string xml_global_DBpassword;  // the name field
        public string DbPassword    // the Name property
        {
            get
            {
                return xml_global_DBpassword;
            }
        }

        private string xml_global_DBserver;  // the name field
        public string DbServer    // the Name property
        {
            get
            {
                return xml_global_DBserver;
            }
        }

        private string xml_global_EnovaFirma;  // the name field
        public string EnovaFirma    // the Name property
        {
            get
            {
                return xml_global_EnovaFirma;
            }
        }

        private string xml_global_EnovaUser;  // the name field
        public string EnovaUser    // the Name property
        {
            get
            {
                return xml_global_EnovaUser;
            }
        }

        private string xml_global_EnovaUserPwd;  // the name field
        public string EnovaUserPwd    // the Name property
        {
            get
            {
                return xml_global_EnovaUserPwd;
            }
        }
    }
}