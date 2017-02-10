namespace SampleForms.Core

open System
open Xamarin.Forms
open MvvmCross.Platform.IoC

type App() =
    inherit MvvmCross.Core.ViewModels.MvxApplication()

    override this.Initialize() = 

        this.CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton()

        this.RegisterAppStart<ViewModels.FirstViewModel>();