using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using LearnAva.MusicStore.Library.ViewModels;
using Splat;

namespace LearnAva.MusicStore;

public class ViewLocator : IDataTemplate, IEnableLogger
{
    public bool SupportsRecycling => false;

    public IControl Build(object data)
    {
        var name = data.GetType().FullName!
            .Replace(".Library", string.Empty)
            .Replace("ViewModel", "View");
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control) Activator.CreateInstance(type)!;
        }
        else
        {
            this.Log().Error($"Not Found: {name}");
            return new TextBlock {Text = "Not Found: " + name};
        }
    }

    public bool Match(object data)
    {
        return data is ViewModelBase;
    }
}