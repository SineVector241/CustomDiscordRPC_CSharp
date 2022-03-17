using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NetDiscordRpc.RPC;
using NetDiscordRpc;
using Newtonsoft.Json.Linq;

namespace DiscordCustomRPC
{
    public partial class MainWindow : Window
    {
        private static DiscordRPC rpc;
        private Config config = new Config();
        public MainWindow()
        {
            try
            {
                InitializeComponent();
                this.Title = "Custom Discord RPC";
                JObject obj = config.LoadSettings();
                ClientID.Text = obj["ClientID"].ToString();
                Details.Text = obj["Details"].ToString();
                State.Text = obj["State"].ToString();
                LargeImageKey.Text = obj["LIK"].ToString();
                LargeImageText.Text = obj["LIT"].ToString();
                SmallImageKey.Text = obj["SIK"].ToString();
                SmallImageText.Text = obj["SIT"].ToString();
                Button1Check.IsChecked = (bool)obj["BTN1"]["Enabled"];
                Button1Text.Text = obj["BTN1"]["Text"].ToString();
                Button1Link.Text = obj["BTN1"]["URL"].ToString();
                Button2Check.IsChecked = (bool)obj["BTN2"]["Enabled"];
                Button2Text.Text = obj["BTN2"]["Text"].ToString();
                Button2Link.Text = obj["BTN2"]["URL"].ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void InitializeRPC_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (InitializeRPC.Content.ToString() == "Start Rich Presence")
                {
                    if (string.IsNullOrWhiteSpace(ClientID.Text.ToString()))
                    {
                        MessageBox.Show( "Please fill in the custom ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    rpc = new DiscordRPC(ClientID.Text.ToString());
                    rpc.Initialize();
                    NetDiscordRpc.RPC.Button[] Buttons = null;
                    if ((bool)Button1Check.IsChecked && (bool)Button2Check.IsChecked)
                    {
                        Buttons = new NetDiscordRpc.RPC.Button[] { new() {
                            Label = Button1Text.Text.ToString(),
                            Url = Button1Link.Text.ToString()
                        },
                        new() {
                            Label = Button2Text.Text.ToString(),
                            Url = Button2Link.Text.ToString()
                        }};
                    }
                    if ((bool)Button1Check.IsChecked && !(bool)Button2Check.IsChecked)
                    {
                        Buttons = new NetDiscordRpc.RPC.Button[] {new()
                        {
                            Label = Button1Text.Text.ToString(),
                            Url = Button1Link.Text.ToString()
                        }};
                    }
                    if ((bool)Button2Check.IsChecked && !(bool)Button1Check.IsChecked)
                    {
                        Buttons = new NetDiscordRpc.RPC.Button[] {new()
                        {
                            Label = Button2Text.Text.ToString(),
                            Url = Button2Link.Text.ToString()
                        }};
                    }
                    rpc.SetPresence(new RichPresence()
                    {
                        Details = Details.Text.ToString(),
                        State = State.Text.ToString(),
                        Assets = new Assets()
                        {
                            LargeImageKey = LargeImageKey.Text.ToString(),
                            LargeImageText = LargeImageText.Text.ToString(),
                            SmallImageKey = SmallImageKey.Text.ToString(),
                            SmallImageText = SmallImageText.Text.ToString()
                        },
                        Buttons = Buttons
                    });
                    rpc.Invoke();
                    InitializeRPC.Content = "Update RPC";
                    InitializeRPC.Background = Brushes.Orange;
                }
                else
                {
                    NetDiscordRpc.RPC.Button[] Buttons = null;
                    if ((bool)Button1Check.IsChecked && (bool)Button2Check.IsChecked)
                    {
                        Buttons = new NetDiscordRpc.RPC.Button[] { new() {
                            Label = Button1Text.Text.ToString(),
                            Url = Button1Link.Text.ToString()
                        },
                        new() {
                            Label = Button2Text.Text.ToString(),
                            Url = Button2Link.Text.ToString()
                        }};
                    }
                    if ((bool)Button1Check.IsChecked && !(bool)Button2Check.IsChecked)
                    {
                        Buttons = new NetDiscordRpc.RPC.Button[] {new()
                        {
                            Label = Button1Text.Text.ToString(),
                            Url = Button1Link.Text.ToString()
                        }};
                    }
                    if ((bool)Button2Check.IsChecked && !(bool)Button1Check.IsChecked)
                    {
                        Buttons = new NetDiscordRpc.RPC.Button[] {new()
                        {
                            Label = Button2Text.Text.ToString(),
                            Url = Button2Link.Text.ToString()
                        }};
                    }

                    rpc.UpdateButtons(Buttons);
                    rpc.UpdateDetails(Details.Text.ToString());
                    rpc.UpdateState(State.Text.ToString());
                    rpc.UpdateLargeAsset(LargeImageKey.Text.ToString(),LargeImageText.Text.ToString());
                    rpc.UpdateSmallAsset(SmallImageKey.Text.ToString(), SmallImageText.Text.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveRPCSettings(object sender, RoutedEventArgs e)
        {
            try
            {
                JObject obj = new JObject();
                obj["ClientID"] = ClientID.Text;
                obj["Details"] = Details.Text;
                obj["State"] = State.Text;
                obj["LIK"] = LargeImageKey.Text;
                obj["LIT"] = LargeImageText.Text;
                obj["SIK"] = SmallImageKey.Text;
                obj["SIT"] = SmallImageText.Text;
                obj["BTN1"] = new JObject();
                obj["BTN1"]["Enabled"] = Button1Check.IsChecked;
                obj["BTN1"]["Text"] = Button1Text.Text;
                obj["BTN1"]["URL"] = Button1Link.Text;
                obj["BTN2"] = new JObject();
                obj["BTN2"]["Enabled"] = Button2Check.IsChecked;
                obj["BTN2"]["Text"] = Button2Text.Text;
                obj["BTN2"]["URL"] = Button2Link.Text;
                config.SaveSettings(obj);
                MessageBox.Show("Saved Your Settings", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}