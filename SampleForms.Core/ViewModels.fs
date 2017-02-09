namespace SampleForms.Core.ViewModels

open MvvmCross.Core.ViewModels
open System.Windows.Input

type AboutViewModel() = 
    inherit MvxViewModel()

type FirstViewModel() = 
    inherit MvxViewModel()

    let mutable _yourNickname = "";

    member this.YourNickname 
    
        with get() =  _yourNickname; 
        and set(value) = 
            if (SetProperty(ref _yourNickname, value)) then
                this.RaisePropertyChanged(fun () -> Hello);

    member this.Hello
        with get() =  "Hello " + this.YourNickname; 
    
    member this.ShowAboutPageCommand
        with get() : ICommand = MvxCommand(fun () -> ShowViewModel<AboutViewModel>()); 
        
    


