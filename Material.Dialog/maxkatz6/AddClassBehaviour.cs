using Avalonia;
using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace Material.Dialog.maxkatz6
{
    public class AddClassBehavior : AvaloniaObject
    {
        public IAvaloniaObject AssociatedObject { get; private set; }

        public string Class
        {
            get => GetValue(ClassProperty);
            set => SetValue(ClassProperty, value);
        }

        public static readonly StyledProperty<string> ClassProperty = AvaloniaProperty.Register<AddClassBehavior, string>(nameof(Class), null);

        public bool IsEnabled
        {
            get => GetValue(IsEnabledProperty);
            set => SetValue(IsEnabledProperty, value);
        }

        public static readonly StyledProperty<bool> IsEnabledProperty = AvaloniaProperty.Register<AddClassBehavior, bool>(nameof(IsEnabled), false);

        public void Attach(IAvaloniaObject associatedObject)
        {
            if (!(associatedObject is IStyledElement styledElement))
            {
                throw new ArgumentException($"{nameof(AddClassBehavior)} supports only IStyledElement");
            }

            AssociatedObject = associatedObject;

            if (Class is string className)
            {
                styledElement.Classes.Set(className, IsEnabled);
            }
        }

        public void Detach()
        {
            IsEnabled = false;
            AssociatedObject = null;
        }

        protected override void OnPropertyChanged<T>(AvaloniaPropertyChangedEventArgs<T> e)
        {
            base.OnPropertyChanged(e);

            if (!(AssociatedObject is IStyledElement styledElement))
            {
                return;
            }

            if (e.Property == ClassProperty)
            {
                if (e.OldValue.GetValueOrDefault<string>() is string oldClassName)
                {
                    styledElement.Classes.Set(oldClassName, false);
                }
                if (e.NewValue.GetValueOrDefault<string>() is string newClassName)
                {
                    styledElement.Classes.Set(newClassName, IsEnabled);
                }
            }
            else if (e.Property == IsEnabledProperty)
            {
                if (Class is string className)
                {
                    styledElement.Classes.Set(className, IsEnabled);
                }
            }
        }
    }
}
