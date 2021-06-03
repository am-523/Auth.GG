using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NETCore.Encrypt;
using Newtonsoft.Json;

namespace Auth.GG.Winforms
{
    public partial class Initialform : Form
    {
        public Initialform()
        {
            InitializeComponent();
        }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage1;
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage2;
        }

        private Jsondata GetJsondata(string appname, string authkey)
        {
            var jsondata = new Jsondata
            {
                Appname = appname,
                Authkey = authkey,
            };
            return jsondata;
        }

        public class Jsondata
        {
            public string Appname { get; set; }
            public string Authkey { get; set; }
        }

        private void Writejsonapp(string Aplicationame, string authentikey)
        {
            WebClient client = new WebClient();
            if (string.IsNullOrWhiteSpace(Aplicationame) || string.IsNullOrWhiteSpace(authentikey))
            {
                MessageBox.Show("Please enter all fields.");
            }
            else
            {
                string path = Application.StartupPath;
                string apname = Aplicationame;
                string autkey = EncryptProvider.Base64Encrypt(authentikey);
                string finalpath = String.Format("{0}\\{1}.json", path, apname);
                var jsonf = GetJsondata(EncryptProvider.Base64Encrypt(apname), autkey);
                var jsonToWrite = JsonConvert.SerializeObject(jsonf, Formatting.Indented);

                if (File.Exists(finalpath))
                {
                    MessageBox.Show("There is already a saved application with the same name.");
                }
                else
                {
                    if (client.DownloadString($"https://developers.auth.gg/USERS/?type=count&authorization={authentikey}").Contains("success"))
                    {
                        try
                        {
                            File.WriteAllText(finalpath, jsonToWrite);
                            MessageBox.Show("Successfully saved.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    } else
                    {
                        MessageBox.Show("Invalid Authorization Key");
                    }
                }
            }
        }


        private void siticoneButton4_Click(object sender, EventArgs e)
        {
           Writejsonapp(siticoneMaterialTextBox1.Text, siticoneMaterialTextBox2.Text);
        }

        private string appnameread { get; set; }
        private string Localizepath(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.InitialDirectory = Application.StartupPath;
                    fileDialog.Filter = "json files (*.json)|*.json";
                    fileDialog.RestoreDirectory = true;
                    var fldlg = fileDialog.ShowDialog();
                    string fnalpath = fileDialog.FileName;
                    //string namefile = (fileDialog.SafeFileName).Replace(".json", "");
                    if (fldlg.ToString() == "OK")
                    {
                        return fnalpath;
                    }
                    else if (fldlg.ToString() == "Cancel")
                    {
                        return "Invalid";
                    }
                    else
                    {
                        return "Invalid";
                    }
                }
                else
                {
                    string path = Application.StartupPath;
                    string apname = name;
                    string finalpath = String.Format("{0}\\{1}.json", path, apname);
                    if (File.Exists(finalpath))
                    {
                        return finalpath;
                    }
                    else
                    {
                        MessageBox.Show("File could not be found.");
                        return "Invalid";
                    }
                }
            } catch { return "Invalid"; }
        }

        private void Readjson(string path)
        {
            if (path != "Invalid")
            {
                try
                {
                    string comppath = path;
                    string json = File.ReadAllText(comppath);
                    dynamic jsonobj = JsonConvert.DeserializeObject<Jsondata>(json);
                    string applicationname = EncryptProvider.Base64Decrypt(jsonobj.Appname);
                    string applicationauth = EncryptProvider.Base64Decrypt(jsonobj.Authkey);
                    WebClient wb = new WebClient();
                    if (wb.DownloadString($"https://developers.auth.gg/USERS/?type=count&authorization={applicationauth}").Contains("success"))
                    {
                        Utils.Constants.ApplicationName = applicationname;
                        Utils.Constants.ApplicationAuthKey = applicationauth;
                        Form mainform = new Mainform();
                        this.Hide();
                        mainform.Show();
                    } else
                    {
                        MessageBox.Show("Invalid Authorization Key");
                    }
                } catch { }
            }
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            Readjson(Localizepath(siticoneMaterialTextBox4.Text));
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/DXgamerwar890");
        }
    }
}
