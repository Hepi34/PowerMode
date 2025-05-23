using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Diagnostics;

namespace PowerModeWinUI.Tools
{
    internal class UpdateCheck
    {
        private const string CurrentVersion = "1.5";
        private const string VersionUrl = "https://raw.githubusercontent.com/Hepi34/PowerMode/main/version.txt";

        public static async Task CheckForUpdates(Window parentWindow)
        {
            object u = Windows.Storage.ApplicationData.Current.LocalSettings.Values["UpdateState"];

            if (u != null && u is bool UonValue && UonValue)
            {
                Debug.WriteLine("Update check code reached.");
                try
                {
                    using HttpClient client = new HttpClient { Timeout = TimeSpan.FromSeconds(5) };
                    string latestVersion = await client.GetStringAsync(VersionUrl);
                    latestVersion = latestVersion.Trim();

                    if (!string.Equals(CurrentVersion, latestVersion, StringComparison.OrdinalIgnoreCase))
                    {
                        var updateWindow = new Microsoft.UI.Xaml.Window();

                        var rootGrid = new Grid
                        {
                            Width = 300,
                            Height = 200,
                            Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White)
                        };

                        var stackPanel = new StackPanel
                        {
                            Margin = new Thickness(20)
                        };

                        stackPanel.Children.Add(new TextBlock
                        {
                            Text = $"A new version is available: {latestVersion}\nYou are using: {CurrentVersion}\nPlease go to GitHub and download the latest version.",
                            TextWrapping = TextWrapping.Wrap
                        });

                        var closeButton = new Button
                        {
                            Content = "Close",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(0, 20, 0, 0)
                        };

                        closeButton.Click += (s, e) => updateWindow.Close();
                        stackPanel.Children.Add(closeButton);

                        rootGrid.Children.Add(stackPanel);
                        updateWindow.Content = rootGrid;
                        updateWindow.Activate();
                    }
                }
                catch (TaskCanceledException)
                {
                    ShowErrorWindow("Update Check Failed", "Failed to check for updates. No internet connection or request timed out.");
                }
                catch (Exception ex)
                {
                    ShowErrorWindow("Error Checking for Updates", $"Unexpected error: {ex.Message}");
                }
            }
        }

        private static void ShowErrorWindow(string title, string message)
        {
            var errorWindow = new Microsoft.UI.Xaml.Window();

            var rootGrid = new Grid
            {
                Width = 300,
                Height = 150,
                Background = new Microsoft.UI.Xaml.Media.SolidColorBrush(Microsoft.UI.Colors.White)
            };

            var stackPanel = new StackPanel
            {
                Margin = new Thickness(20)
            };

            stackPanel.Children.Add(new TextBlock
            {
                Text = message,
                TextWrapping = TextWrapping.Wrap
            });

            var closeButton = new Button
            {
                Content = "Close",
                HorizontalAlignment = HorizontalAlignment.Center,
                Margin = new Thickness(0, 20, 0, 0)
            };

            closeButton.Click += (s, e) => errorWindow.Close();
            stackPanel.Children.Add(closeButton);

            rootGrid.Children.Add(stackPanel);
            errorWindow.Content = rootGrid;
            errorWindow.Activate();
        }



    }
}
     