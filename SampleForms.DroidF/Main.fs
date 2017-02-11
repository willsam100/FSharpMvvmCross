namespace SampleForms.DroidF

open System
open System.Diagnostics
open System.Collections.Generic
open System.Linq
open System.Text
open Android.App
open Android.Content
open Android.OS
open Android.Runtime
open Android.Views
open Android.Widget
open Android.Content.PM
open Xamarin.Forms.Platform.Android
open Xamarin.Forms
open MvvmCross.Forms.Presenter.Core
open MvvmCross.Platform
open MvvmCross.Core.Views
open MvvmCross.Forms.Presenter.Droid
open MvvmCross.Core.ViewModels
open MvvmCross.Platform.Platform
open MvvmCross.Droid.Platform
open MvvmCross.Droid.Views

type DebugTrace() as this = 
    interface IMvxTrace with
        member this.Trace(level: MvxTraceLevel , tag: string , message: Func<string>) = 
            Debug.WriteLine <| sprintf "%s:%A:%s" tag level (message.Invoke())
        
        member this.Trace(level: MvxTraceLevel , tag: string , message: string) =
            Debug.WriteLine <| sprintf "%s:%A:%s" tag level message
        

        member this.Trace(level: MvxTraceLevel , tag: string , message: string , [<ParamArray>] args: Object[]) = 
            let rec trace level tag message (args: Object[]) = 
                try
                    Debug.WriteLine <| sprintf "%s:%A:%s, %A" tag level message args
                with 
                | e as FormatException -> 
                    trace MvxTraceLevel.Error tag "Exception during trace of 0 1" [|level; message|]
            trace level tag message args
    
type Setup(applicationContext: Context) =
    inherit MvxAndroidSetup(applicationContext)

    override this.CreateApp() : IMvxApplication = new Core.App()

    override this.CreateDebugTrace(): IMvxTrace = new DebugTrace() :> IMvxTrace

    override this. CreateViewPresenter() : IMvxAndroidViewPresenter = 
        let presenter = new MvxFormsDroidPagePresenter()
        Mvx.RegisterSingleton<IMvxViewPresenter>(presenter)
        presenter :> IMvxAndroidViewPresenter


type Resources = SampleForms.DroidF.Resource

[<Activity (Label = "SampleForms", MainLauncher = true, Icon = "@mipmap/icon")>]
    type MvxFormsApplicationActivity() = 
        inherit FormsApplicationActivity()
    
        override this.OnCreate(bundle: Bundle) = 
            base.OnCreate(bundle)

            Forms.Init(this, bundle)
            let mvxFormsApp = new MvxFormsApp()
            this.LoadApplication(mvxFormsApp)

            let presenter = Mvx.Resolve<IMvxViewPresenter>() :?> MvxFormsDroidPagePresenter
            presenter.MvxFormsApp <- mvxFormsApp

            Mvx.Resolve<IMvxAppStart>().Start()

