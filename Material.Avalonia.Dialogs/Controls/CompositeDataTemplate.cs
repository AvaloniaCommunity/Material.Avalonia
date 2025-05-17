using System;
using System.Diagnostics;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Data;

namespace Material.Dialog.Controls;

public class CompositeDataTemplate : AvaloniaList<IDataTemplate>, ITreeDataTemplate, IDataTemplate
{
    public InstancedBinding? ItemsSelector(object item)
    {
        var paramType = item?.GetType();
        
        foreach (var t in this)
        {
            switch (t)
            {
                case ITreeDataTemplate firstStage:
                    if (firstStage is not ITypedDataTemplate dataTemplate)
                        throw new ArrayTypeMismatchException($"{item?.GetType() } doesn't have {nameof(ITypedDataTemplate)} interface implementation, which required for recognise the data type and select templates.");
                    
                    if(!dataTemplate.DataType?.Equals(paramType) ?? throw new ArgumentNullException())
                        continue;

                    return firstStage.ItemsSelector(item!);
            }
        }

        return null;
    }

    public Control? Build(object? param)
    {
        var paramType = param?.GetType();
        
        foreach (var item in this)
        {
            switch (item)
            {
                case ITypedDataTemplate dataTemplate:

                    var templateType = dataTemplate.DataType ?? throw new ArgumentNullException();
                    
                    var isSameType = templateType == paramType;
                    var isInheritType = paramType?.IsSubclassOf(templateType) ?? false;
                    
                    if(!isSameType && !isInheritType)
                        continue;

                    return dataTemplate.Build(param);
                
                default:
                    throw new NotSupportedException($"{item?.GetType()} is a unsupported type");
            }
        }

        Trace.TraceError($"CompositeDataTemplate: No satisfied data template entry for {paramType}.");
        return null;
    }

    public bool Match(object? data)
    {
        return true;
    }
}