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
            let trace level tag message (args: Object[]) = 
                try
                    Debug.WriteLine <| sprintf "%s:%A:%s, %A" tag level message args
                with 
                | e as FormatException -> ()
//                    trace MvxTraceLevel.Error tag "Exception during trace of 0 1" [|level message|]
            trace level tag message args
    
type Setup(applicationContext: Context) =
    inherit MvxAndroidSetup(applicationContext)

    override this.CreateApp() : IMvxApplication = new SampleForms.Core.App() :> IMvxApplication

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

            let thing = Mvx.TryResolve<IMvxViewPresenter>()

            let presenter = Mvx.Resolve<IMvxViewPresenter>() :?> MvxFormsDroidPagePresenter
            presenter.MvxFormsApp <- mvxFormsApp

            Mvx.Resolve<IMvxAppStart>().Start()

[<Activity(
        Label = "SampleForms.DroidF"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        //, Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)>]
    type SplashScreen() = 
        inherit MvxSplashScreenActivity(Resources.Layout.SplashScreen)

        let _ = new LinkerPleaseInclude()

        let mutable isInitializationComplete = false
        override this.InitializationComplete() =
            if (not isInitializationComplete) then
                isInitializationComplete <- true
                this.StartActivity(typeof<MvxFormsApplicationActivity>)
        
        override this.OnCreate(bundle: Android.OS.Bundle) =
            Forms.Init(this, bundle)
            EventHandler<ViewInitializedEventArgs>(fun s e -> if (System.String.IsNullOrEmpty(e.View.StyleId) |> not) then
                                                                e.NativeView.ContentDescription <- e.View.StyleId )
            |> Forms.ViewInitialized.AddHandler
            base.OnCreate(bundle)
        
    

