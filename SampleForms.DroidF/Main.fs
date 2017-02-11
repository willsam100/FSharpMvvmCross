namespace SampleForms.DroidF

open System
open System.Collections.Generic
open System.Linq
open System.Text
open
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

