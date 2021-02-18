using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Material.Dialog.Interfaces;
using Material.Dialog.ViewModels;

namespace Material.Dialog.Views
{
    public class TimePickerDialog : Window, IDialogWindowResult<DateTimePickerDialogResult>
    {
        //private bool PointerHoldingCell;
        private TimePickerDialogViewModel viewModel;
        private Grid CallerPanel1;
        private Grid CallerPanel2;
        
        private void CreateCallerCells1() => CreateCallerCellsToPanel(CallerPanel1, 12, firstText: 12, onPointerPressing: v => viewModel.SetFirstField(v));
        private void CreateCallerCells2() => CreateCallerCellsToPanel(CallerPanel2, 60, onPointerPressing: v => viewModel.SetSecondField(v));
        
        private void CreateCallerCellsToPanel(Grid panel, int counts, float radius = 109, int firstText = 0, Action<int> onPointerPressing = null)
        {
            var offset = (Math.PI * 2) * 0.25; 
            for (var i = 0; i < counts; i++)
            {
                var r = (float) i / (float) counts * (Math.PI * 2) - offset;
                var x = radius * Math.Cos(r);
                var y = radius * Math.Sin(r);

                var v = i;
                var text = i == 0 ? firstText.ToString() : v.ToString();
                var cell = CreateCallerCell(x, y, text, v, i1 => onPointerPressing?.Invoke(i1));
                panel.Children.Add(cell);
            }
        }

        private Control CreateCallerCell(double x, double y, string text, int value, Action<int> onPointerPressing)
        {
            var root = new Border()
            {
                Classes = Classes.Parse("CallerCell"),
                RenderTransform = new TranslateTransform(x, y),
                Background = SolidColorBrush.Parse("Transparent"),
                Tag = value,
                Child = new Grid()
                {
                    Children = {
                        new Border()
                        {
                            Name = "PointerEnterFeedback",
                        },
                        new TextBlock()
                        {
                            Text = text
                        }
                    }
                }
            }; 
            
            root.PointerPressed += (sender, args) =>
            {
                //PointerHoldingCell = true;
                onPointerPressing((int) root.Tag);
            };
            root.PointerReleased += (sender, args) =>
            {
                var pager = this.Get<Carousel>("PART_PagesRoot");
                pager.Next();
            };
            return root;
        }
        
    
        public DateTimePickerDialogResult Result { get; set; }
        
        public TimePickerDialog()
        {
            Result = new DateTimePickerDialogResult();
            
            InitializeComponent();
            CallerPanel1 = this.Get<Grid>(nameof(CallerPanel1));
            CallerPanel2 = this.Get<Grid>(nameof(CallerPanel2));
        }

        public void AttachViewModel(TimePickerDialogViewModel vm)
        {
            this.DataContext = vm;
            viewModel = vm;
        }

        protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
        {
            base.OnApplyTemplate(e);
            
            CreateCallerCells1();
            CreateCallerCells2();
        }

        public DateTimePickerDialogResult GetResult() => Result;
        
        public void SetNegativeResult(DialogResult result) => Result._result = result.GetResult;
        
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);
    }
}
