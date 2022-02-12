using Material.Dialog;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Material.Dialog.Enums;

namespace Material.Demo.ViewModels
{
    public class DialogDemoViewModel : ViewModelBase
    {
        private string _dialog1Result;
        public string Dialog1Result { get => _dialog1Result; set { _dialog1Result = value; OnPropertyChanged(); } }

        private string _dialog2Result;
        public string Dialog2Result { get => _dialog2Result; set { _dialog2Result = value; OnPropertyChanged(); } }

        private string _dialog3Result;
        public string Dialog3Result { get => _dialog3Result; set { _dialog3Result = value; OnPropertyChanged(); } }
        
        private string _dialog4Result;
        public string Dialog4Result { get => _dialog4Result; set { _dialog4Result = value; OnPropertyChanged(); } }

        private string _loginDialogResult;
        public string LoginDialogResult { get => _loginDialogResult; set { _loginDialogResult = value; OnPropertyChanged(); } }

        private string _folderNameDialogResult;
        public string FolderNameDialogResult { get => _folderNameDialogResult; set { _folderNameDialogResult = value; OnPropertyChanged(); } }
        
        private string _timePickerDialogResult;
        public string TimePickerDialogResult { get => _timePickerDialogResult; set { _timePickerDialogResult = value; OnPropertyChanged(); } }
        
        private string _datePickerDialogResult;
        public string DatePickerDialogResult { get => _datePickerDialogResult; set { _datePickerDialogResult = value; OnPropertyChanged(); } }

        private TimeSpan _previousTimePickerResult;
        private DateTime _previousDatePickerResult = DateTime.Now;

        public async void Dialog1()
        {
            var dialog = DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
            {
                ContentHeader = "Welcome to use Material.Avalonia",
                SupportingText = "Enjoy Material Design in AvaloniaUI!",
                StartupLocation = WindowStartupLocation.CenterOwner, 
            });
            var result = await dialog.ShowDialog(Program.MainWindow);
            Dialog1Result = $"Result: {result.GetResult}";
        }

        public async void Dialog2()
        {
            var result = await DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
            {
                ContentHeader = "Confirm action",
                SupportingText = "Are you sure to DELETE 20 FILES?",
                StartupLocation = WindowStartupLocation.CenterOwner,
                NegativeResult = new DialogResult("cancel"),
                DialogHeaderIcon = Dialog.Icons.DialogIconKind.Help,
                DialogButtons = new DialogResultButton[] 
                {
                    new DialogResultButton
                    {
                        Content = "CANCEL",
                        Result = "cancel"
                    },
                    new DialogResultButton
                    {
                        Content = "DELETE",
                        Result = "delete"
                    },
                },
            }).ShowDialog(Program.MainWindow); 
            Dialog2Result = $"Result: {result.GetResult}";
        }

        public async void Dialog3()
        {
            var result = await DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
            {
                ContentHeader = "Confirm action",
                SupportingText = "Are you sure to DELETE 20 FILES?",
                DialogHeaderIcon = Dialog.Icons.DialogIconKind.Help,
                StartupLocation = WindowStartupLocation.CenterOwner,
                NegativeResult = new DialogResult("cancel"),
                Borderless = true,
                DialogButtons = new DialogResultButton[]
                {
                    new DialogResultButton
                    {
                        Content = "CANCEL",
                        Result = "cancel"
                    },
                    new DialogResultButton
                    {
                        Content = "DELETE",
                        Result = "delete"
                    }
                }
            }).ShowDialog(Program.MainWindow);
            Dialog3Result = $"Result: {result.GetResult}";
            if(result.GetResult == "delete")
            {
                await DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
                {
                    ContentHeader = "Result",
                    SupportingText = "20 files has deleted.",
                    StartupLocation = WindowStartupLocation.CenterOwner,
                    DialogHeaderIcon = Dialog.Icons.DialogIconKind.Success,
                    Borderless = true,
                }).ShowDialog(Program.MainWindow);
            }
        }
        
        public async void Dialog4()
        {
            // Get AssetLoader service
            var assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
            
            // Open asset stream using assets.Open method.
            using (var icon = assets.Open(new Uri("avares://Material.Demo/Assets/avalonia-logo.png")))
            {
                var dialog = DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
                {
                    ContentHeader = "Welcome to use Material.Avalonia",
                    SupportingText = "Enjoy Material Design in AvaloniaUI!",
                    StartupLocation = WindowStartupLocation.CenterOwner, 
                    Borderless = true,
                    // Create Image control
                    DialogIcon = new Bitmap(icon),
                    NeutralDialogButtons = new []
                    {
                        new DialogResultButton
                        {
                            Content = "READ MORE...",
                            Result = "read_more"
                        }
                    }
                });
                var result = await dialog.ShowDialog(Program.MainWindow);
                Dialog4Result = $"Result: {result.GetResult}";
            }
        }

        public async void LoginDialog()
        {
            var result = await DialogHelper.CreateTextFieldDialog(new TextFieldDialogBuilderParams()
            {
                ContentHeader = "Authentication required.",
                SupportingText = "Please login before any action.",
                StartupLocation = WindowStartupLocation.CenterOwner,
                DialogHeaderIcon = Dialog.Icons.DialogIconKind.Blocked,
                Borderless = true,
                Width = 400,
                TextFields = new TextFieldBuilderParams[]
                {
                    new TextFieldBuilderParams
                    {
                        HelperText = "* Required",
                        Classes = "Outline",
                        Label = "Account",
                        MaxCountChars = 24,
                        Validater = ValidateAccount, 
                    },
                    new TextFieldBuilderParams
                    {
                        HelperText = "* Required",
                        Classes = "Outline",
                        Label = "Password",
                        MaxCountChars = 64,
                        FieldKind = TextFieldKind.Masked,
                        Validater = ValidatePassword,
                    }
                },
                PositiveButton = new DialogResultButton
                {
                    Content = "LOGIN",
                    Result = "login"
                },
            }).ShowDialog(Program.MainWindow);
            LoginDialogResult = $"Result: {result.GetResult}";
            if (result.GetResult == "login")
            {
                LoginDialogResult = $"Result: {result.GetResult}\nAccount: {result.GetFieldsResult()[0].Text}\nPassword: {result.GetFieldsResult()[1].Text}";
            }
        }

        private Tuple<bool,string> ValidateAccount(string text)
        {
            var result = text?.Length > 5;
            return new Tuple<bool, string>(result, result ? "" : "Too few account name.");
        }
        private Tuple<bool, string> ValidatePassword(string text) 
        {
            var result = text?.Length >= 1;
            return new Tuple<bool, string>(result, result ? "" : "Field should be filled.");
        }


        public async void FolderNameDialog()
        {
            var result = await DialogHelper.CreateTextFieldDialog(new TextFieldDialogBuilderParams()
            {
                ContentHeader = "Rename folder",
                StartupLocation = WindowStartupLocation.CenterOwner,
                Borderless = true,
                Width = 400,
                TextFields = new TextFieldBuilderParams[]
                {
                    new TextFieldBuilderParams
                    {
                        Label = "Folder name",
                        MaxCountChars = 256,
                        Validater = ValidatePassword,
                        DefaultText = "Folder1"
                    },
                },
                PositiveButton = new DialogResultButton
                {
                    Content = "RENAME",
                    Result = "rename"
                },
            }).ShowDialog(Program.MainWindow);
            FolderNameDialogResult = $"Result: {result.GetResult}";
            if (result.GetResult == "rename")
            {
                FolderNameDialogResult = $"Result: {result.GetResult}\nFolder name: {result.GetFieldsResult()[0].Text}";
            }
        }

        public async void TimePickerDialog()
        {
            var result = await DialogHelper.CreateTimePicker(new TimePickerDialogBuilderParams()
            {
                Borderless = true,
                StartupLocation = WindowStartupLocation.CenterOwner,
                ImplicitValue = _previousTimePickerResult,
                PositiveButton = new DialogResultButton
                {
                    Content = "CONFIRM",
                    Result = "confirm"
                },
            }).ShowDialog(Program.MainWindow);
            TimePickerDialogResult = $"Result: {result.GetResult}";
            if (result.GetResult == "confirm")
            {
                var r = result.GetTimeSpan();
                TimePickerDialogResult = $"Result: {result.GetResult}\nTimeSpan: {r}";
                _previousTimePickerResult = r;
            }
        }
        
        public async void DatePickerDialog()
        {
            var result = await DialogHelper.CreateDatePicker(new DatePickerDialogBuilderParams()
            {
                Borderless = true,
                StartupLocation = WindowStartupLocation.CenterOwner,
                ImplicitValue = _previousDatePickerResult,
                PositiveButton = new DialogResultButton
                {
                    Content = "CONFIRM",
                    Result = "confirm"
                },
            }).ShowDialog(Program.MainWindow);
            DatePickerDialogResult = $"Result: {result.GetResult}";
            if (result.GetResult == "confirm")
            {
                var r = result.GetDate();
                DatePickerDialogResult = $"Result: {result.GetResult}\nDate: {r.ToString("d")}";
                _previousDatePickerResult = r;
            }
        }
    }
}
