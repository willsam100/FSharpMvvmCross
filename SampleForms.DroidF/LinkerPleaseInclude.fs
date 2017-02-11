namespace SampleForms.DroidF

open System
open System.Collections.Specialized
open System.Windows.Input
open Android.App
open Android.Views
open Android.Widget

//#pragma warning disable 219


// This class is never actually executed, but when Xamarin linking is enabled it does how to ensure types and properties
// are preserved in the deployed app
type LinkerPleaseInclude() = 

    member this.Include(button: Button) = 
        button.Click.AddHandler <| EventHandler(fun s e -> button.Text <- button.Text + "")
    
    member this.Include(checkBox: CheckBox) = 
        EventHandler<CompoundButton.CheckedChangeEventArgs>(fun sender args -> checkBox.Checked <- not checkBox.Checked) 
        |> checkBox.CheckedChange.AddHandler
    
    member this.Include(``switch``: Switch) = 
        EventHandler<CompoundButton.CheckedChangeEventArgs>(fun sender args -> ``switch``.Checked <- not ``switch``.Checked)
        |> ``switch``.CheckedChange.AddHandler
    
    member this.Include(view: View) = 
        EventHandler(fun s e  -> view.ContentDescription <- view.ContentDescription + "")
        |> view.Click.AddHandler
    
    member this.Include(text: TextView) = 
    
        text.TextChanged += (sender, args) => text.Text = "" + text.Text
    //    text.Hint = "" + text.Hint
    

    //member this.Include(CheckedTextView text)
    
    //    text.TextChanged += (sender, args) => text.Text = "" + text.Text
    //    text.Hint = "" + text.Hint
    

    //member this.Include(CompoundButton cb)
    
    //    cb.CheckedChange += (sender, args) => cb.Checked = !cb.Checked
    

    //member this.Include(SeekBar sb)
    
    //    sb.ProgressChanged += (sender, args) => sb.Progress = sb.Progress + 1
    

    //member this.Include(Activity act)
    
    //    act.Title = act.Title + ""
    

    //member this.Include(INotifyCollectionChanged changed)
    
    //    changed.CollectionChanged += (s, e) =>  var test = string.Format("01234", e.Action, e.NewItems, e.NewStartingIndex, e.OldItems, e.OldStartingIndex) 
    

    //member this.Include(ICommand command)
    
    //    command.CanExecuteChanged += (s, e) =>  if (command.CanExecute(null)) command.Execute(null) 
    

    //member this.Include(MvvmCross.Platform.IoC.MvxPropertyInjector injector)
    
    //    injector = new MvvmCross.Platform.IoC.MvxPropertyInjector()
    

    //member this.Include(System.ComponentModel.INotifyPropertyChanged changed)
    
    //    changed.PropertyChanged += (sender, e) =>
        
    //        var test = e.PropertyName
        
        
    


