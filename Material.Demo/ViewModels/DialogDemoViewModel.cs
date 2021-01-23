using Material.Dialog;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;
using Material.Dialog.Enums;

namespace Material.Demo.ViewModels
{
    public class DialogDemoViewModel : ViewModelBase
    {
        private string m_Dialog1Result;
        public string Dialog1Result { get => m_Dialog1Result; set { m_Dialog1Result = value; OnPropertyChanged(); } }

        private string m_Dialog2Result;
        public string Dialog2Result { get => m_Dialog2Result; set { m_Dialog2Result = value; OnPropertyChanged(); } }

        private string m_Dialog3Result;
        public string Dialog3Result { get => m_Dialog3Result; set { m_Dialog3Result = value; OnPropertyChanged(); } }

        private string m_LoginDialogResult;
        public string LoginDialogResult { get => m_LoginDialogResult; set { m_LoginDialogResult = value; OnPropertyChanged(); } }

        private string m_FolderNameDialogResult;
        public string FolderNameDialogResult { get => m_FolderNameDialogResult; set { m_FolderNameDialogResult = value; OnPropertyChanged(); } }


        public async void Dialog1()
        {
            var dialog = DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
            {
                ContentHeader = "Welcome to use Material.Avalonia",
                SupportingText = "Enjoy with Material Design in AvaloniaUI!",
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
                        Content = "DELETE",
                        Result = "delete"
                    },
                    new DialogResultButton
                    {
                        Content = "CANCEL",
                        Result = "cancel"
                    }
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
                StartupLocation = WindowStartupLocation.CenterOwner,
                NegativeResult = new DialogResult("cancel"),
                Borderless = true,
                DialogHeaderIcon = Dialog.Icons.DialogIconKind.Help,
                DialogButtons = new DialogResultButton[]
                {
                    new DialogResultButton
                    {
                        Content = "DELETE",
                        Result = "delete"
                    },
                    new DialogResultButton
                    {
                        Content = "CANCEL",
                        Result = "cancel"
                    }
                },
            }).ShowDialog(Program.MainWindow);
            Dialog3Result = $"Result: {result.GetResult}";
            if(result.GetResult == "delete")
            {
                await DialogHelper.CreateAlertDialog(new AlertDialogBuilderParams()
                {
                    ContentHeader = "Result",
                    SupportingText = "20 files has deleted.",
                    StartupLocation = WindowStartupLocation.CenterOwner,
                    NegativeResult = new DialogResult(DialogHelper.DIALOG_RESULT_OK),
                    Borderless = true,
                    DialogHeaderIcon = Dialog.Icons.DialogIconKind.Success,
                    DialogButtons = DialogHelper.CreateSimpleDialogButtons(DialogButtonsEnum.Ok)
                }).ShowDialog(Program.MainWindow);
            }
        }

        public async void LoginDialog()
        {
            var result = await DialogHelper.CreateTextFieldDialog(new TextFieldDialogBuilderParams()
            {
                ContentHeader = "Authentication required.",
                SupportingText = "Please login before any action (this is a joke).",
                StartupLocation = WindowStartupLocation.CenterOwner,
                NegativeResult = new DialogResult("cancel"),
                Borderless = true,
                Width = 400,
                MaxWidth = 400,
                DialogHeaderIcon = Dialog.Icons.DialogIconKind.Blocked,
                TextFields = new TextFieldBuilderParams[]
                {
                    new TextFieldBuilderParams
                    {
                        Label = "Account",
                        MaxCountChars = 24,
                        Validater = ValidateAccount, 
                    },
                    new TextFieldBuilderParams
                    {
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
                NegativeButton = new DialogResultButton
                {
                    Content = "CANCEL",
                    Result = "cancel"
                }
            }).ShowDialog(Program.MainWindow);
            LoginDialogResult = $"Result: {result.GetResult}";
            if (result.GetResult == "login")
            {
                LoginDialogResult = $"Result: {result.GetResult}\nAccount: {result.GetFieldsResult()[0].Text}\nPassword: {result.GetFieldsResult()[1].Text}";
            }
        }

        private bool ValidateAccount(string text) => text?.Length > 5;
        private bool ValidatePassword(string text) => text?.Length >= 1;


        public async void FolderNameDialog()
        {
            var result = await DialogHelper.CreateTextFieldDialog(new TextFieldDialogBuilderParams()
            {
                ContentHeader = "Rename folder",
                StartupLocation = WindowStartupLocation.CenterOwner,
                NegativeResult = new DialogResult("cancel"),
                Borderless = true,
                MaxWidth = 400,
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
                NegativeButton = new DialogResultButton
                {
                    Content = "CANCEL",
                    Result = "cancel"
                }
            }).ShowDialog(Program.MainWindow);
            FolderNameDialogResult = $"Result: {result.GetResult}";
            if (result.GetResult == "rename")
            {
                FolderNameDialogResult = $"Result: {result.GetResult}\nFolder name: {result.GetFieldsResult()[0].Text}";
            }
        }
    }
}
