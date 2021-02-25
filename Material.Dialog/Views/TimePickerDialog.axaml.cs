using System;
using System.Collections.Generic;
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
        private Carousel PART_PagesRoot;
        private Grid CallerPanel1;
        private Grid CallerPanel2;
        private Stack<IPointer> Pointers;
        private bool HoldingPointer => Pointers.Count >= 1;
        
        private void CreateCallerCells1() => CreateCallerCellsToPanel(CallerPanel1, 12, firstText: 12, decreaseShows: false);
        private void CreateCallerCells2() => CreateCallerCellsToPanel(CallerPanel2, 60,padNumbers:true);
        
        private void CreateCallerCellsToPanel(Grid panel, int counts, float radius = 109, int firstText = 0, bool padNumbers = false, bool decreaseShows = true)
        {
            var offset = (Math.PI * 2) * 0.25;
            var target = 0;
            
            for (var i = 0; i < counts; i++)
            {
                if (decreaseShows)
                {
                    if (target == i)
                    {
                        target += 5;
                    }
                    else 
                        continue;
                }
                
                var r = (float) i / (float) counts * (Math.PI * 2) - offset;
                var x = radius * Math.Cos(r);
                var y = radius * Math.Sin(r);

                var v = (i == 0 ? firstText : i);
                var text = padNumbers ? v.ToString("D2") : v.ToString();
                var cell = CreateCallerCell(x, y, text);
                panel.Children.Add(cell);
            }
        }

        private Control CreateCallerCell(double x, double y, string text)
        {
            var root = new Border()
            {
                Classes = Classes.Parse("CallerCell"),
                RenderTransform = new TranslateTransform(x, y),
                Background = SolidColorBrush.Parse("Transparent"),
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
            return root;
        }

        public DateTimePickerDialogResult Result { get; set; }
        
        public TimePickerDialog()
        {
            Result = new DateTimePickerDialogResult();
            Pointers = new Stack<IPointer>();
            
            InitializeComponent();
            // Create decorations
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
            
            PART_PagesRoot = this.Get<Carousel>("PART_PagesRoot");
            
            CreateCallerCells1();
            CreateCallerCells2();
        }

        public DateTimePickerDialogResult GetResult() => Result;
        
        public void SetNegativeResult(DialogResult result) => Result.Result = result.GetResult;
        
        private void InitializeComponent() => AvaloniaXamlLoader.Load(this);

        private void CallerPanel_OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            Pointers.Push(e.Pointer);
            var panel = sender as Control;
            var pointer = e.GetPosition(panel);
            CallerPanel_OnPointerPressOrMove(panel, pointer);
        }

        private void CallerPanel_OnPointerMoved(object sender, PointerEventArgs e)
        {
            var panel = sender as Control;
            var pointer = e.GetPosition(panel);
            CallerPanel_OnPointerPressOrMove(panel, pointer);
        }

        private void CallerPanel_OnPointerPressOrMove(Control panel, Point p)
        {
            if (HoldingPointer)
            {
                var radius = panel.Bounds.Width / 2;
                
                var radians = Math.Atan2(p.X - radius, p.Y - radius);
                var degree = 360 - ((radians * 180 / Math.PI) + 180);
                ProcessPick(degree);
            }
        }

        private void ProcessPick(double deg)
        {
            var mul = PART_PagesRoot.SelectedIndex == 1 ? 60 : 12;

            var v = (int)Math.Round(deg / 360 * mul);

            if (v == mul)
                v = 0;
            
            if(PART_PagesRoot.SelectedIndex == 1)
                viewModel.SecondField = (ushort)v;
            else
                viewModel.FirstField = (ushort)v;
        }
        
        private void CallerPanel_OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            Pointers.Pop();
            
            if(!HoldingPointer)
                PART_PagesRoot.Next();
        }
    }
}
