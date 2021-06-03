using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using NETCore.Encrypt;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace Auth.GG.Winforms
{
    public partial class Mainform : Form
    {
        public Mainform()
        {
            InitializeComponent();
        }

        private string dataoneselected { get; set; }
        private string datatwoselected { get; set; }

        private string datatreeselected { get; set; }

        private void siticoneButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Loadapp()
        {
            string name = Utils.Constants.ApplicationName;
            string authkey = Utils.Constants.ApplicationAuthKey;
            try
            {
                string usercount = Utils.AuthAPI.Usercount(authkey);
                string licensecount = Utils.AuthAPI.Licensecount(authkey);
                label1.Text = $"Admin Panel: {Utils.Constants.ApplicationName}";
                label3.Text = String.Format("Name: {0}\nUsers: {1}\nLicenses: {2}", name, usercount, licensecount);
            } catch { }
            Getallusersdataview();
            Getalllicensesview();
            dataoneselected = "";
            datatwoselected = "";
            datatreeselected = "";
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            Loadapp();
            durationcombo.SelectedIndex = 0;
            formatcombo.SelectedIndex = 0;
            pclog.Text = System.Environment.MachineName;
        }

        private void siticoneButton1_Click(object sender, EventArgs e)
        {
            Form intiform = new Initialform();
            intiform.Show();
            this.Close();
        }

        private void siticoneButton3_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage1;
        }

        private void siticoneButton4_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage2;
        }

        private void siticoneButton5_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage3;
        }

        private void siticoneButton6_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage4;
        }

        private void siticoneButton7_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage5;
        }

        private void siticoneButton8_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage6;
        }

        private void siticoneButton9_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage7;
        }

        private void siticoneButton10_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = Utils.AuthAPI.AppInfoString(aidbox.Text, apibox.Text, secretbox.Text);
        }

        private Jsondata GetJsondata(string aid, string secret, string api)
        {
            var jsondata = new Jsondata
            {
                AID = aid,
                SecretKey = secret,
                APIKey = api
            };
            return jsondata;
        }

        public class Jsondata
        {
            public string AID { get; set; }
            public string SecretKey { get; set; }
            public string APIKey { get; set; }
        }

        private void siticoneButton11_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(aidbox.Text) || string.IsNullOrWhiteSpace(secretbox.Text) || string.IsNullOrWhiteSpace(apibox.Text))
            {
                MessageBox.Show("Please enter all fields.");
            }
            else
            {
                string path = Application.StartupPath;
                string apname = Utils.Constants.ApplicationName;
                string aid = EncryptProvider.Base64Encrypt(aidbox.Text);
                string secret = EncryptProvider.Base64Encrypt(secretbox.Text);
                string api = EncryptProvider.Base64Encrypt(apibox.Text);
                string filename = apname + "_api";
                string finalpath = String.Format("{0}\\{1}.json", path, filename);
                var jsonf = GetJsondata(aid, secret, api);
                var jsonToWrite = JsonConvert.SerializeObject(jsonf, Formatting.Indented);
                if (File.Exists(finalpath))
                {
                    MessageBox.Show("There is already a saved application with the same name.");
                }
                else
                {
                    try
                    {
                        Utils.AuthAPI.AppInformation(aidbox.Text, apibox.Text, secretbox.Text);
                        if (Utils.AuthAPI.Informationrequest)
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
                        }
                    }
                    catch
                    {
                        MessageBox.Show("Invalid Credentials.");
                    }
                }
            }
        }

        private void siticoneButton12_Click(object sender, EventArgs e)
        {
            try
            {
                string apname = Utils.Constants.ApplicationName;
                string filename = apname + "_api";
                string path = Application.StartupPath;
                string finalpath = String.Format("{0}\\{1}.json", path, filename);
                if (File.Exists(finalpath))
                {
                    string json = File.ReadAllText(finalpath);
                    dynamic jsonobj = JsonConvert.DeserializeObject<Jsondata>(json);
                    string aid = EncryptProvider.Base64Decrypt(jsonobj.AID);
                    string secret = EncryptProvider.Base64Decrypt(jsonobj.SecretKey);
                    string api = EncryptProvider.Base64Decrypt(jsonobj.APIKey);
                    aidbox.Text = aid;
                    secretbox.Text = secret;
                    apibox.Text = api;
                    MessageBox.Show("File loaded");
                }
                else
                {
                    MessageBox.Show($"I can't find {filename}");
                }
            }
            catch { }
        }

        public class Root
        {
            public string status { get; set; }
            public string value { get; set; }
        }

        private async void Getallusersdataview()
        {
            try
            {
                dataGridView1.Rows.Clear();
                var _client = new HttpClient();
                string userCount = await _client.GetStringAsync($"https://developers.auth.gg/USERS/?type=count&authorization={Utils.Constants.ApplicationAuthKey}");
                var objs = JsonConvert.DeserializeObject<Root>(userCount);
                _client.Dispose();
                userCount = objs.value.ToString();

                WebClient wc = new WebClient();
                string data = wc.DownloadString($"https://developers.auth.gg/USERS/?type=fetchall&authorization={Utils.Constants.ApplicationAuthKey}");
                var obj = JsonConvert.DeserializeObject<dynamic>(data);
                List<int> usersIds = new List<int>();

                for (int num = 0; num < (Convert.ToInt32(userCount) + 0); num++)
                    usersIds.Add(num);

                foreach (var id in usersIds)
                {
                    string userVar = obj[id.ToString()]["variable"];
                    if (userVar == "")
                        userVar = "N/A";
                    string[] gridData = new string[] { id.ToString(), obj[id.ToString()]["username"], obj[id.ToString()]["email"], obj[id.ToString()]["rank"], userVar, obj[id.ToString()]["lastlogin"], obj[id.ToString()]["lastip"], obj[id.ToString()]["expiry_date"] };
                    dataGridView1.Rows.Add(gridData);
                }
            }  catch { }
        }

        public async void Getalllicensesview()
        {
            try
            {
                dataGridView2.Rows.Clear();
                string licensesCount;
                WebClient wc = new WebClient();
                var __client = new HttpClient();
                string keys = await __client.GetStringAsync($"https://developers.auth.gg/LICENSES/?type=count&authorization={Utils.Constants.ApplicationAuthKey}");
                var objss = JsonConvert.DeserializeObject<dynamic>(keys);
                __client.Dispose();
                licensesCount = objss.value.ToString();
                string ssss = wc.DownloadString($"https://developers.auth.gg/LICENSES/?type=fetchall&authorization={Utils.Constants.ApplicationAuthKey}");
                var obj1 = JsonConvert.DeserializeObject<dynamic>(ssss);
                List<int> asasas = new List<int>();

                for (int num = 0; num < (Convert.ToInt32(licensesCount) + 0); num++)
                    asasas.Add(num);

                foreach (var id in asasas)
                {
                    string huqhjllscm = obj1[id.ToString()]["used"];
                    if (huqhjllscm == "1")
                        huqhjllscm = "True";
                    else
                    {
                        huqhjllscm = "False";
                    }
                    string[] gridData = new string[] { id.ToString(), obj1[id.ToString()]["token"], obj1[id.ToString()]["rank"], huqhjllscm, obj1[id.ToString()]["used_by"], obj1[id.ToString()]["days"] };
                    dataGridView2.Rows.Add(gridData);
                }
            }
            catch { }
        }

        private void siticoneButton13_Click(object sender, EventArgs e)
        {
            Getallusersdataview();
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataoneselected != "" || userbox.Text != String.Empty)
                {
                    siticoneButton4.Checked = false;
                    siticoneButton5.Checked = true;
                    bunifuPages1.SelectedTab = tabPage3;
                }
                else
                {
                    MessageBox.Show("First select a user");
                }
            }
            catch { }
        }

        private void resetHWIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataoneselected != "" || userbox.Text == String.Empty)
                {
                    Utils.AuthAPI.ResetHwid(Utils.Constants.ApplicationAuthKey, dataoneselected);
                } else
                {
                    MessageBox.Show("First select a user");
                }
            } catch { }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            dataoneselected = dataGridView1.Rows[e.RowIndex].Cells["Column2"].FormattedValue.ToString();
            userbox.Text = dataGridView1.Rows[e.RowIndex].Cells["Column2"].FormattedValue.ToString();
        }

        private void siticoneButton18_Click(object sender, EventArgs e)
        {
            Getalllicensesview();
            dataoneselected = "";
        }

        private void siticoneButton19_Click(object sender, EventArgs e)
        {
            Loadapp();
        }

        private void siticoneButton14_Click(object sender, EventArgs e)
        {
            try
            {
                richTextBox2.Text = Utils.AuthAPI.Userinfostring(userbox.Text, Utils.Constants.ApplicationAuthKey);
            } catch { }
        }

        private void siticoneButton15_Click(object sender, EventArgs e)
        {
            Utils.AuthAPI.Changepassword(Utils.Constants.ApplicationAuthKey, userbox.Text, newpassbox.Text);
        }

        private void siticoneButton16_Click(object sender, EventArgs e)
        {
            Utils.AuthAPI.Changevariable(Utils.Constants.ApplicationAuthKey, userbox.Text, varbox.Text);
        }

        private void siticoneButton20_Click(object sender, EventArgs e)
        {
            Utils.AuthAPI.Changerank(Utils.Constants.ApplicationAuthKey, userbox.Text, rankbox.Text);
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataoneselected != "" || userbox.Text != String.Empty)
                {
                    Utils.AuthAPI.Deleteuser(Utils.Constants.ApplicationAuthKey, dataoneselected);
                    Loadapp();
                    dataoneselected = "";
                }
                else
                {
                    MessageBox.Show("First select a user");
                }
            }
            catch { }
        }

        private void siticoneButton17_Click(object sender, EventArgs e)
        {
            bunifuPages1.SelectedTab = tabPage8;
        }

        private void licenseEditorToolStripMenuItem_Click(object sender, EventArgs e)
        {

            try
            {
                if (datatwoselected != "" || selectedlicensebox.Text != String.Empty)
                {
                    siticoneButton6.Checked = false;
                    siticoneButton7.Checked = true;
                    bunifuPages1.SelectedTab = tabPage5;
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void deleteLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (datatwoselected != "" || selectedlicensebox.Text != String.Empty)
                {
                    Utils.AuthAPI.Deletelicense(Utils.Constants.ApplicationAuthKey, datatwoselected);
                    Loadapp();
                    datatwoselected = "";
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void siticoneButton25_Click(object sender, EventArgs e)
        {
            //selectedlicensebox
            try
            {
                richTextBox3.Text = Utils.AuthAPI.Licenseinfostring(selectedlicensebox.Text, Utils.Constants.ApplicationAuthKey);
            }
            catch { }
        }

        private void siticoneButton24_Click(object sender, EventArgs e)
        {
            try
            {
                if (datatwoselected != "" || selectedlicensebox.Text != String.Empty)
                {
                    Utils.AuthAPI.Uselicense(Utils.Constants.ApplicationAuthKey, datatwoselected);
                    Loadapp();
                    datatwoselected = "";
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void siticoneButton23_Click(object sender, EventArgs e)
        {
            try
            {
                if (datatwoselected != "" || selectedlicensebox.Text != String.Empty)
                {
                    Utils.AuthAPI.Unuselicense(Utils.Constants.ApplicationAuthKey, datatwoselected);
                    Loadapp();
                    datatwoselected = "";
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void siticoneButton21_Click(object sender, EventArgs e)
        {
            try
            {
                if (datatwoselected != "" || selectedlicensebox.Text != String.Empty)
                {
                    Utils.AuthAPI.Deletelicense(Utils.Constants.ApplicationAuthKey, datatwoselected);
                    Loadapp();
                    datatwoselected = "";
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            datatwoselected = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].FormattedValue.ToString();
            selectedlicensebox.Text = dataGridView2.Rows[e.RowIndex].Cells["dataGridViewTextBoxColumn2"].FormattedValue.ToString();
        }

        private string days { get; set; }
        private static readonly Regex regex = new Regex(@"^\d+$");
        private void siticoneButton22_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(ammountbox.Text) || string.IsNullOrWhiteSpace(lengthbox.Text) || string.IsNullOrWhiteSpace(levelbox.Text) || string.IsNullOrWhiteSpace(prefixbox.Text) || string.IsNullOrWhiteSpace(customdaybox.Text))
                {
                    MessageBox.Show("Check that you have entered all the necessary information");
                    return;
                }
                if (!(regex.IsMatch(ammountbox.Text) || regex.IsMatch(lengthbox.Text) || regex.IsMatch(levelbox.Text) || regex.IsMatch(customdaybox.Text)))
                {
                    MessageBox.Show("Some options only accept numbers");
                    return;
                }
                if (regex.IsMatch(prefixbox.Text))
                {
                    MessageBox.Show("The prefix does not accept numbers");
                    return;
                }
                int lvllength = Int32.Parse(levelbox.Text);
                switch (durationcombo.SelectedIndex.ToString())
                {
                    case "0":
                        days = "1";
                        break;
                    case "1":
                        days = "3";
                        break;
                    case "2":
                        days = "7";
                        break;
                    case "3":
                        days = "21";
                        break;
                    case "4":
                        days = "30";
                        break;
                    case "5":
                        days = "90";
                        break;
                    case "6":
                        days = "9998";
                        break;
                    case "7":
                        days = customdaybox.Text;
                        break;
                }

                string ammount = ammountbox.Text;
                string length = lengthbox.Text;
                string level = levelbox.Text;
                string prefix = prefixbox.Text;
                string dayscount = days;
                string format = (Int32.Parse(formatcombo.SelectedIndex.ToString()) + 1).ToString();

                if (lvllength <= 10)
                {
                    var obje = Utils.AuthAPI.Generatelicense(ammount, length, prefix, level, dayscount, format, Utils.Constants.ApplicationAuthKey);
                    string strboj = obje.ToString();
                    dataGridView3.Rows.Clear();
                    var obj1 = JsonConvert.DeserializeObject<dynamic>(strboj);
                    List<int> asasas = new List<int>();

                    for (int num = 0; num < (Convert.ToInt32(ammount) + 0); num++)
                        asasas.Add(num);

                    foreach (var id in asasas)
                    {
                        string[] gridData = new string[] { id.ToString(), obj1[id.ToString()] };
                        dataGridView3.Rows.Add(gridData);
                    }
                    Loadapp();

                } else
                {
                    MessageBox.Show("Unable to generate licenses");
                }
            } catch
            {

            }
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (datatreeselected != "")
                { 
                    Clipboard.SetText(datatreeselected);
                    datatreeselected = "";
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            datatreeselected = dataGridView3.Rows[e.RowIndex].Cells["licensecreation"].FormattedValue.ToString();
        }

        private void siticoneButton26_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(aidbox.Text) || string.IsNullOrWhiteSpace(apibox.Text) || string.IsNullOrWhiteSpace(secretbox.Text) || string.IsNullOrWhiteSpace(loginusername.Text) || string.IsNullOrWhiteSpace(loginpass.Text))) {
                richTextBox4.Text = Utils.AuthAPI.Loginstring(loginusername.Text, loginpass.Text, aidbox.Text, apibox.Text, secretbox.Text);
            } else
            {
                MessageBox.Show("More information required (Home Tab)]\nor necessary information missing");
            }
        }

        private void siticoneButton27_Click(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(aidbox.Text) || string.IsNullOrWhiteSpace(apibox.Text) || string.IsNullOrWhiteSpace(secretbox.Text) || string.IsNullOrWhiteSpace(reguser.Text) || string.IsNullOrWhiteSpace(regpass.Text) || string.IsNullOrWhiteSpace(regmail.Text) || string.IsNullOrWhiteSpace(reglicen.Text)))
            {
                richTextBox4.Clear();
                Utils.AuthAPI.Registerauth(reguser.Text, regpass.Text, regmail.Text, reglicen.Text,aidbox.Text, apibox.Text, secretbox.Text);
            }
            else
            {
                MessageBox.Show("More information required (Home Tab)]\nor necessary information missing");
            }
        }

        private void siticoneButton28_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (datatwoselected != "")
                {
                    Clipboard.SetText(datatwoselected);
                    datatwoselected = "";
                }
                else
                {
                    MessageBox.Show("First select a license");
                }
            }
            catch { }
        }

        private void siticoneButton28_Click_1(object sender, EventArgs e)
        {
            if (!(string.IsNullOrWhiteSpace(aidbox.Text) || string.IsNullOrWhiteSpace(apibox.Text) || string.IsNullOrWhiteSpace(secretbox.Text) || string.IsNullOrWhiteSpace(userlog.Text) || string.IsNullOrWhiteSpace(pclog.Text) || string.IsNullOrWhiteSpace(richTextBox5.Text) || richTextBox5.Text == "Test"))
            {
                Utils.AuthAPI.Senlog(richTextBox5.Text, pclog.Text, userlog.Text, aidbox.Text, secretbox.Text, apibox.Text);
            }
            else
            {
                MessageBox.Show("More information required (Home Tab)]\nor necessary information missing");
            }
        }
    }
}
