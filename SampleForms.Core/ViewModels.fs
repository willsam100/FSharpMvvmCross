namespace SampleForms.Core.ViewModels

open System
open MvvmCross.Core.ViewModels
open System.Windows.Input
open System.Diagnostics
open SampleForms.Core.NameOf

type AboutViewModel() = 
    inherit MvxViewModel()

type FirstViewModel() = 
    inherit MvxViewModel()

    let mutable _yourNickname = "";

    member this.YourNickname 
    
        with get() =  _yourNickname; 
        and set(value) = 
            if (_yourNickname <> value) then
                _yourNickname <- value
                this.RaisePropertyChanged()
                this.RaisePropertyChanged(nameOf <@ this.Hello @>);

    member this.Hello
        with get() =  "Hello " + this.YourNickname; 
       
    
    member this.ShowAboutPageCommand
        with get() = 
            new MvxCommand(Action(this.Show))
        
    member this.Show() = this.ShowViewModel<AboutViewModel>() |> ignore
    


