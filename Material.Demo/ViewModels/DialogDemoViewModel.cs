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
    }
}
